using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.AI.Navigation;

public class GameBoardManager : MonoBehaviour
{

    public static GameBoardManager singleton;

    [HideInInspector]
    public List<BoardEntity> boardPlayers = new List<BoardEntity>(); // For turns shuffle.

    public Recipe objectiveRecipe;

    private Dictionary<BoardEntity, Recipe> recipeStates = new Dictionary<BoardEntity, Recipe>();

    public List<SceneAsset> minigameScenes = new List<SceneAsset>();

    public bool randomRecipe;
    public List<Recipe> recipesList = new List<Recipe>();
    public List<Flavor> recipeFlavors = new List<Flavor>();
    public List<Ingredient> recipeIngredients = new List<Ingredient>();

    [HideInInspector]
    public int roundIndex = 0;

    private int turnIndex = 0;

    #region Save/Load

    private GameObject persistentBoardObjects;

    public void SaveGameState()
    {
        if(persistentBoardObjects == null)
        {
            persistentBoardObjects = new GameObject("Persistent On Board");
            DontDestroyOnLoad(persistentBoardObjects);
            persistentBoardObjects.SetActive(false);
        }

        foreach(Coaster c in FindObjectsOfType<Coaster>())
        {
            c.transform.parent = persistentBoardObjects.transform;
        }
    }

    public void LoadGameState()
    {
        foreach(Coaster c in persistentBoardObjects.GetComponentsInChildren<Coaster>())
        {
            Debug.Log("yes");
            c.gameObject.transform.parent = null;
            SceneManager.MoveGameObjectToScene(c.gameObject, SceneManager.GetActiveScene());
        }
    }

    #endregion

    #region Awake/Start/Update
    private void Awake()
    {
        #region Singleton
        if(singleton != null)
        {
            enabled = false;
            return;
        }
        singleton = this;
        //persistentBoardObjects = null; // Not necessary tho (?).
        #endregion
        DontDestroyOnLoad(this);
        InitializeGame();
    }

    private void Start()
    {
        //Time.timeScale = 15f;
    }

    /*
    private void Update()
    {
        
    }
    */

    #endregion

    private void InitializeGame()
    {
        InitializeCoasters();
        //InitializePaths();
        InitializePlayers();
        RandomizeTurns();
        GameStart();
    }

    private void InitializeCoasters()
    {
        List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

        List<Coaster> coasters = new List<Coaster>();

        CoasterSpawner[] coasterSpawners = FindObjectsOfType<CoasterSpawner>();

        foreach (CoasterSpawner cS in coasterSpawners)
        {
            Coaster c = cS.SpawnCoaster();
            c.Initialize();
            surfaces.Add(c.GetComponent<NavMeshSurface>());
            coasters.Add(c);
        }

        for(int j = 0; j < coasterSpawners.Length; j++)
        {
            foreach(CoasterSpawner next in coasterSpawners[j].next)
            {
                coasters[j].next.Add(next.coaster);
            }
        }

        // Don't destroy CoasterSpawner because can be used again to change the coaster type (?)

        #region CreateLinks
        for (int i = 0; i < coasters.Count; i++)
        {
            GameObject gO = new GameObject("NavMeshLink");
            gO.transform.position = coasters[i].transform.position + (coasters[i].next[0].transform.position - coasters[i].transform.position) / 2;
            NavMeshLink nml = gO.AddComponent<NavMeshLink>();
            nml.startPoint = coasters[i].transform.position;
            //nml.startPoint = gO.transform.position + (coasters[i].transform.position - gO.transform.position);
            nml.endPoint = coasters[i].next[0].transform.position;
            //.endPoint = gO.transform.position + (coasters[i].next[0].transform.position - gO.transform.position);
        }
        #endregion

        BuildNavMesh(surfaces);
    }

    private void InitializePaths()
    {
        /*
        List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("BoardPath"))
        {
            NavMeshSurface nms = gO.AddComponent<NavMeshSurface>();
            nms.collectObjects = CollectObjects.Children;
            surfaces.Add(nms);
        }
        BuildNavMesh(surfaces);
        */
    }

    private void BuildNavMesh(List<NavMeshSurface> surfaces)
    {
        foreach (NavMeshSurface nms in surfaces)
        {
            if(nms != null)
            {
                nms.BuildNavMesh();
            }
        }
    }

    private void InitializePlayers()
    {
        List<PlayerCharacter> players = new List<PlayerCharacter>();
        PlayerCharacter player = Instantiate(CharacterManager.selectedCharacter);
        BoardPlayer bP = player.gameObject.AddComponent<BoardPlayer>();
        bP.Initialize();
        players.Add(player);

        foreach (PlayerCharacter ai in CharacterManager.aiCharacters)
        {
            PlayerCharacter aiPlayer = Instantiate(ai);
            BoardAI aiP = aiPlayer.gameObject.AddComponent<BoardAI>();
            aiP.Initialize();
            players.Add(aiPlayer);
        }

        foreach (PlayerCharacter p in players)
        {
            BoardEntity boardPlayer = p.GetComponent<BoardEntity>();
            List<Vector3> waitZones = Coaster.initialCoaster.GetAvailableWaitZones();
            if (waitZones.Count > 0)
            {
                boardPlayer.TeleportTo(waitZones[0]);
                Coaster.initialCoaster.SetWaitZoneState(waitZones[0], boardPlayer);
            } else
            {
                boardPlayer.TeleportTo(Coaster.initialCoaster.transform.position + Vector3.up);
                Coaster.initialCoaster.SetWaitZoneState(Coaster.initialCoaster.transform.position + Vector3.up, boardPlayer);
            }
            boardPlayer.currentCoaster = Coaster.initialCoaster;
            boardPlayers.Add(boardPlayer);
        }
    }

    private SceneAsset GetRandomMinigame()
    {
        if(minigameScenes != null && minigameScenes.Count > 0)
        {
            return minigameScenes[UnityEngine.Random.Range(0, minigameScenes.Count)];
        }
        return null;
    }

    public void RandomizeTurns()
    {
        List<BoardEntity> aux = boardPlayers;
        boardPlayers = new List<BoardEntity>();
        int count = aux.Count;
        for(int i = 0; i < count; i++)
        {
            BoardEntity rndE = aux[UnityEngine.Random.Range(0, aux.Count)];
            aux.Remove(rndE);
            boardPlayers.Add(rndE);
        }
    }

    #region Events
    public event Action onGameStart;
    public void GameStart()
    {
        TurnStart(boardPlayers[turnIndex]);
        onGameStart?.Invoke();
    }

    public event Action onGameEnd;
    public void GameEnd()
    {
        onGameEnd?.Invoke();
    }

    public event Action<BoardEntity> onTurnStart;
    public void TurnStart(BoardEntity entity)
    {
        // Do stuff. (?)
        entity.hasTurn = true;


        // ------
        onTurnStart?.Invoke(entity);
    }

    public event Action<BoardEntity> onTurnEnd;
    public void TurnEnd(BoardEntity entity)
    {
        Debug.Log("a");
        entity.hasTurn = false;
        turnIndex++;
        if (turnIndex >= boardPlayers.Count)
        {
            Debug.Log("ab");
            turnIndex = 0;
            SaveGameState();
            // Start random event. (Minigame, general boost, etc.)
            SceneAsset nextMinigame = GetRandomMinigame();
            if(nextMinigame != null)
            {
                SceneManager.LoadScene(nextMinigame.name);
            }
            //SceneManager.LoadScene("MainMenu");
        }
        onTurnEnd?.Invoke(entity);
        Debug.Log("b");
        TurnStart(boardPlayers[turnIndex]);
    }
    #endregion
}