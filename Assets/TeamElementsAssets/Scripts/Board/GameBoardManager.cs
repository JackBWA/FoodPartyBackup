using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.AI.Navigation;
using SplineMesh;
using System.Collections;
using UnityEngine.UI;

public class GameBoardManager : MonoBehaviour
{

    public string boardSceneName;

    public static GameBoardManager singleton;

    [HideInInspector]
    public List<BoardEntity> boardPlayers = new List<BoardEntity>(); // For turns shuffle.

    public Recipe recipe;

    public Dictionary<BoardEntity, Recipe> recipeStates = new Dictionary<BoardEntity, Recipe>();

    public List<string> minigameScenes = new List<string>();

    private GameObject boardInteractablesParent;

    /*
    public bool randomRecipe;
    public List<Recipe> recipesList = new List<Recipe>();
    */

    private List<Flavor> recipeFlavors = new List<Flavor>();
    private List<Ingredient> recipeIngredients = new List<Ingredient>();

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

        foreach(BoardEntity bE in boardPlayers)
        {
            bE.transform.parent = persistentBoardObjects.transform;
        }
    }

    public IEnumerator LoadGameState()
    {

        SceneManager.LoadScene(boardSceneName);
        while (!SceneManager.GetActiveScene().name.Equals(boardSceneName))
        {
            yield return null;
        }

        // From here scene is loaded.

        CameraBoardManager.UpdateCinemachineBrain();

        boardInteractablesParent = GameObject.FindGameObjectWithTag("BoardInteractables");

        foreach (Coaster c in persistentBoardObjects.GetComponentsInChildren<Coaster>())
        {
            c.gameObject.transform.parent = null;
            SceneManager.MoveGameObjectToScene(c.gameObject, SceneManager.GetActiveScene());
            c.gameObject.transform.parent = boardInteractablesParent.transform;
        }

        foreach(BoardEntity bE in persistentBoardObjects.GetComponentsInChildren<BoardEntity>())
        {
            bE.gameObject.transform.parent = null;
            SceneManager.MoveGameObjectToScene(bE.gameObject, SceneManager.GetActiveScene());
            bE.gameObject.transform.parent = boardInteractablesParent.transform;
        }

        RandomizeTurns();
        RoundStart();

        yield return null;
    }

    #endregion

    #region Awake/Start/Update
    private void Awake()
    {
        #region Singleton
        if(singleton != null)
        {
            Destroy(gameObject);
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
        GameStart();
        //Time.timeScale = 3f;
    }

    /*
    private void Update()
    {
        
    }
    */

    #endregion

    private void InitializeGame()
    {
        boardInteractablesParent = GameObject.FindGameObjectWithTag("BoardInteractables");
        Scene aux = SceneManager.GetActiveScene();
        boardSceneName = aux.name;
        InitializeBoard();
        InitializePlayers();
        InitializeRecipe();
        RandomizeTurns();
    }

    private void InitializeRecipe()
    {
        Recipe[] recipes = Resources.LoadAll<Recipe>("Recipes");
        recipe = recipes[UnityEngine.Random.Range(0, recipes.Length)];
        recipe.Initialize();
        
        foreach(BoardEntity player in boardPlayers)
        {
            Recipe recipeCopy = ScriptableObject.CreateInstance<Recipe>();
            recipeCopy.CopyFrom(recipe);

            /*
            // TEST
            foreach (KeyValuePair<Ingredient, int> kV in recipeCopy.requiredIngredients)
            {
                recipeCopy.SetCurrentIngredient(kV.Key, UnityEngine.Random.Range(0, kV.Value));
            }
            */

            recipeStates.Add(player, recipeCopy);
        }

        /* // Test of recipe completion. (Works)
        Debug.Log(recipe.isCompleted);
        Debug.Log(recipe);
        recipe.Complete();
        Debug.Log(recipe.isCompleted);
        Debug.Log(recipe);
        */
    }

    private void InitializeBoard()
    {
        #region Coasters
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

        for (int j = 0; j < coasterSpawners.Length; j++)
        {
            foreach (CoasterSpawner next in coasterSpawners[j].next)
            {
                coasters[j].next.Add(next.coaster);
            }
            coasters[j].transform.parent = boardInteractablesParent.transform;
        }
        #endregion

        // Don't destroy CoasterSpawner because can be used again to change the coaster type (?)

        #region Paths // Nope
        /*
        for(int i = 0; i < coasters.Count; i++)
        {
            GameObject gO = new GameObject("Path");
            Coaster cC = coasters[i];
            int j = i + 1;
            if (j >= coasters.Count) j = 0;
            Coaster nC = coasters[j];
            Debug.Log(cC.transform.localScale);
            gO.transform.position = cC.transform.position + cC.transform.forward * (cC.transform.localScale.x / 1.5f);
            Spline spline = gO.AddComponent<Spline>();
            spline.AddNode(new SplineNode(nC.transform.position + -nC.transform.forward * (nC.transform.localScale.x / 1.5f), Vector3.zero));
            SplineMeshTiling splineMesh = gO.AddComponent<SplineMeshTiling>();
            splineMesh.mesh = pathMesh;
            splineMesh.mode = MeshBender.FillingMode.Repeat;
        }
        */
        #endregion

        BuildNavMesh(surfaces);

        #region CreateLinks
        /*
        for (int i = 0; i < coasters.Count; i++)
        {


             // Doesn't work.
            GameObject gO = new GameObject("NavMeshLink");
            gO.transform.position = coasters[i].transform.position + (coasters[i].next[0].transform.position - coasters[i].transform.position) / 2;
            NavMeshLink nml = gO.AddComponent<NavMeshLink>();
            nml.startPoint = coasters[i].transform.localPosition;
            //nml.startPoint = gO.transform.position + (coasters[i].transform.position - gO.transform.position);
            nml.endPoint = coasters[i].next[0].transform.localPosition;
            //.endPoint = gO.transform.position + (coasters[i].next[0].transform.position - gO.transform.position);
            
        }
        */
        #endregion
    }

    private void InitializePaths() // Nop
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
            boardPlayer.transform.parent = boardInteractablesParent.transform;
            boardPlayers.Add(boardPlayer);
        }
    }

    private string GetRandomMinigame()
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
        RoundStart();
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
        entity.hasTurn = false;
        turnIndex++;
        if (turnIndex >= boardPlayers.Count)
        {
            RoundEnd();
            return; // Return is needed? I think so.
        }
        onTurnEnd?.Invoke(entity);
        TurnStart(boardPlayers[turnIndex]);
    }

    public event Action onRoundStart;
    public void RoundStart()
    {
        TurnStart(boardPlayers[turnIndex]);
        onRoundStart?.Invoke();
    }

    public event Action onRoundEnd;
    public void RoundEnd()
    {
        turnIndex = 0;
        roundIndex++;
        SaveGameState();
        // Start random event. (Minigame, general boost, etc.)
        string nextMinigame = GetRandomMinigame();
        if (nextMinigame != null)
        {
            SceneManager.LoadScene(nextMinigame);
        }
        onRoundEnd?.Invoke();
    }

    public void EventEnd()
    {
        Debug.Log("Event Ended.");
        StartCoroutine(LoadGameState());
    }
    #endregion
}