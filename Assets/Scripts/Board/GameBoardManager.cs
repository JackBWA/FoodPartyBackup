using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 15f;
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
        #region Old xd
        /*
        #region Variables Setup
        if (randomRecipe)
        {
            objectiveRecipe = Recipe.CreateRandomRecipe(2, 4, recipeFlavors, recipeIngredients); // Hardcoded for now.
        } else
        {
            objectiveRecipe = recipesList[UnityEngine.Random.Range(0, recipesList.Count)];
        }
        roundIndex = 0;
        turnIndex = 0;
        #endregion
        #region Initialize Coasters
        Coaster[] coasters = FindObjectsOfType<Coaster>();
        foreach (Coaster c in coasters)
        {
            c.Initialize(GameManager.maxPlayers);
        }
        #endregion
        #region Initialize Players
        // Instantiate players
        List<PlayerCharacter> players = new List<PlayerCharacter>();
        PlayerCharacter player = Instantiate(CharacterManager.selectedCharacter);
        BoardPlayer bP = player.gameObject.AddComponent<BoardPlayer>();
        bP.Initialize();
        players.Add(player);

        foreach(PlayerCharacter ai in CharacterManager.aiCharacters)
        {
            PlayerCharacter aiPlayer = Instantiate(ai);
            BoardAI aiP = aiPlayer.gameObject.AddComponent<BoardAI>();
            aiP.Initialize();
            players.Add(aiPlayer);
        }

        // Teleport them to the coaster's waiting zones.
        foreach (PlayerCharacter p in players)
        {
            BoardEntity boardPlayer = p.GetComponent<BoardEntity>();
            boardPlayer.TeleportTo(Coaster.initialCoaster);
            boardPlayers.Add(boardPlayer);
            recipeStates.Add(boardPlayer, new Recipe());
        }
        #endregion
        RandomizeTurns();
        GameStart();
        */
        #endregion
    }

    private void InitializeCoasters()
    {
        foreach (CoasterSpawner cS in FindObjectsOfType<CoasterSpawner>())
        {
            cS.SpawnCoaster().Initialize(GameManager.maxPlayers);
        }

        // PLAYERS WIP (MOVE & SPAWN WITH CHARACTERCONTROLLER)
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

        // Teleport them to the initial coaster.
        foreach (PlayerCharacter p in players)
        {
            BoardEntity boardPlayer = p.GetComponent<BoardEntity>();
            boardPlayer.TeleportTo(Coaster.initialCoaster.transform.position + Vector3.up);
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
        entity.hasTurn = false;
        turnIndex++;
        if (turnIndex >= boardPlayers.Count)
        {
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

        TurnStart(boardPlayers[turnIndex]);
    }
    #endregion
}