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

    public static GameBoardManager singleton;

    [HideInInspector]
    public string boardSceneName;

    [HideInInspector]
    public List<BoardEntity> boardPlayers = new List<BoardEntity>(); // For turns shuffle.

    [HideInInspector]
    public BoardEntity winner;

    [HideInInspector]
    public Recipe recipe;

    public int playerInitialCoins = 50;

    public Dictionary<BoardEntity, Recipe> recipeStates = new Dictionary<BoardEntity, Recipe>();

    public Canvas gameBoardCanvasPrefab;
    [HideInInspector]
    public Canvas gameBoardCanvasInstance;

    public List<string> minigameScenes = new List<string>();

    #region Parents
    public GameObject systemsParent;
    public GameObject uiParent;
    public GameObject navMeshParent;
    public GameObject mapParent;
    public GameObject saveLoadGameObjectsParent;
    public GameObject otherParent;
    #endregion

    /*
    public bool randomRecipe;
    public List<Recipe> recipesList = new List<Recipe>();
    */

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
        }

        saveLoadGameObjectsParent.gameObject.SetActive(false);

        saveLoadGameObjectsParent.transform.parent = persistentBoardObjects.transform;

        /*
        foreach(Transform gO in saveLoadGameObjectsParent.transform)
        {
            gO.parent = persistentBoardObjects.transform;
        }
        */

        persistentBoardObjects.SetActive(false);

        /*
        foreach(Coaster c in FindObjectsOfType<Coaster>())
        {
            c.transform.parent = persistentBoardObjects.transform;
        }

        foreach(BoardEntity bE in boardPlayers)
        {
            bE.transform.parent = persistentBoardObjects.transform;
        }
        */
    }

    public IEnumerator LoadGameState()
    {
        SceneManager.LoadScene(boardSceneName);
        while (!SceneManager.GetActiveScene().name.Equals(boardSceneName))
        {
            yield return null;
        }

        // From here scene is loaded.

        // Trash code.
        foreach (GameObject gO in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (gO.CompareTag("PersistentObjects"))
            {
                Destroy(gO);
                break;
            }
        }

        SetParents();

        CameraBoardManager.UpdateCinemachineBrain();

        // No.
        //saveLoadBoardObjectsParent = GameObject.FindGameObjectWithTag("BoardInteractables");

        saveLoadGameObjectsParent.transform.parent = null;
        SceneManager.MoveGameObjectToScene(saveLoadGameObjectsParent, SceneManager.GetActiveScene());

        saveLoadGameObjectsParent.SetActive(true);

        #region old code
        /*
        foreach(Transform persistentChild in persistentBoardObjects.transform)
        {
            persistentChild.parent = null;
            SceneManager.MoveGameObjectToScene(persistentChild.gameObject, SceneManager.GetActiveScene());
            persistentChild.parent = saveLoadGameObjectsParent.transform;
        }
        */

        /*
        foreach (Coaster c in persistentBoardObjects.GetComponentsInChildren<Coaster>())
        {
            c.gameObject.transform.parent = null;
            SceneManager.MoveGameObjectToScene(c.gameObject, SceneManager.GetActiveScene());
            c.gameObject.transform.parent = saveLoadBoardObjectsParent.transform;
        }

        foreach(BoardEntity bE in persistentBoardObjects.GetComponentsInChildren<BoardEntity>())
        {
            bE.gameObject.transform.parent = null;
            SceneManager.MoveGameObjectToScene(bE.gameObject, SceneManager.GetActiveScene());
            bE.gameObject.transform.parent = saveLoadBoardObjectsParent.transform;
        }
        */
        #endregion

        RandomizeTurns();
        InitializeGameCanvas();

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
        transform.parent = null;
        DontDestroyOnLoad(this);
        InitializeGame();
    }

    private void Start()
    {
        GameStart();
    }

    #endregion

    private void InitializeGame()
    {
        SetParents();
        //saveLoadGameObjectsParent = GameObject.FindGameObjectWithTag("BoardInteractables");
        Scene aux = SceneManager.GetActiveScene();
        boardSceneName = aux.name;
        winner = null;
        CreateBoard();
        CreatePlayers();
        CreateRecipe();

        RandomizeTurns();
        InitializeGameCanvas();

        InitializePlayers();
    }

    public void SetParents()
    {
        #region Parents Setting

        if(systemsParent == null) systemsParent = GameObject.FindGameObjectWithTag("Systems");
        if (uiParent == null) uiParent = GameObject.FindGameObjectWithTag("UI");
        if (navMeshParent == null) navMeshParent = GameObject.FindGameObjectWithTag("NavMesh");
        if (mapParent == null) mapParent = GameObject.FindGameObjectWithTag("MapStatic");
        if (saveLoadGameObjectsParent == null) saveLoadGameObjectsParent = GameObject.FindGameObjectWithTag("PersistentObjects");
        if (otherParent == null) otherParent = GameObject.FindGameObjectWithTag("Other");

        /*
        public GameObject systemsParent;
        public GameObject uiParent;
        public GameObject navMeshParent;
        public GameObject mapParent;
        public GameObject saveLoadGameObjectsParent;
        public GameObject otherParent;
        */
        #endregion
    }

    private void InitializeGameCanvas()
    {
        gameBoardCanvasInstance = Instantiate(gameBoardCanvasPrefab);
        gameBoardCanvasInstance.transform.SetParent(uiParent.transform);
    }

    private void CreateRecipe()
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

    private void CreateBoard()
    {
        #region Coasters
        //List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

        List<Coaster> coasters = new List<Coaster>();

        CoasterSpawner[] coasterSpawners = FindObjectsOfType<CoasterSpawner>();

        foreach (CoasterSpawner cS in coasterSpawners)
        {
            Coaster c = cS.SpawnCoaster();
            c.Initialize();
            //surfaces.Add(c.GetComponent<NavMeshSurface>()); // Prebaked.
            coasters.Add(c);
        }

        for (int j = 0; j < coasterSpawners.Length; j++)
        {
            foreach (CoasterSpawner next in coasterSpawners[j].next)
            {
                coasters[j].next.Add(next.coaster);
            }

            coasters[j].transform.parent = saveLoadGameObjectsParent.transform;
        }
        #endregion
        // Don't destroy CoasterSpawner because can be used again to change the coaster type (?)
    }

    private void CreatePlayers()
    {
        //List<PlayerCharacter> players = new List<PlayerCharacter>();
        PlayerCharacter player = Instantiate(CharacterManager.selectedCharacter);
        boardPlayers.Add(player.gameObject.AddComponent<BoardPlayer>());
        /*BoardPlayer bP = player.gameObject.AddComponent<BoardPlayer>();
        bP.Initialize();
        players.Add(player);*/

        foreach (PlayerCharacter ai in CharacterManager.aiCharacters)
        {
            PlayerCharacter aiPlayer = Instantiate(ai);
            boardPlayers.Add(aiPlayer.gameObject.AddComponent<BoardAI>());
            /*BoardAI aiP = aiPlayer.gameObject.AddComponent<BoardAI>();
            aiP.Initialize();
            players.Add(aiPlayer);*/
        }
    }

    private void InitializePlayers()
    {
        foreach (BoardEntity bE in boardPlayers)
        {
            //BoardEntity bE = bE.GetComponent<BoardEntity>();
            bE.Initialize();
            List<Vector3> waitZones = Coaster.initialCoaster.GetAvailableWaitZones();
            if (waitZones.Count > 0)
            {
                bE.TeleportTo(waitZones[0]);
                Coaster.initialCoaster.SetWaitZoneState(waitZones[0], bE);
            }
            else
            {
                bE.TeleportTo(Coaster.initialCoaster.transform.position + Vector3.up);
                Coaster.initialCoaster.SetWaitZoneState(Coaster.initialCoaster.transform.position + Vector3.up, bE);
            }

            bE.currentCoaster = Coaster.initialCoaster;
            bE.transform.parent = saveLoadGameObjectsParent.transform;
            //boardPlayers.Add(bE);
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
        recipeStates[boardPlayers[0]].Complete();
        RoundStart();
        onGameStart?.Invoke();
    }

    public event Action onGameEnd;
    public void GameEnd()
    {
        // Do stuff when game ends.
        Debug.Log("Game ended!");
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

        if (winner != null)
        {
            GameEnd();
            return;
        }

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
        Time.timeScale = 1f;
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
        onRoundEnd?.Invoke();
        if (nextMinigame != null)
        {
            SceneManager.LoadScene(nextMinigame);
        }
    }

    public void EventEnd()
    {
        Debug.Log("Event Ended.");
        StartCoroutine(LoadGameState());
    }
    #endregion
}