using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameBoardManager : MonoBehaviour
{

    public static GameBoardManager singleton;

    public List<BoardEntity> boardPlayers = new List<BoardEntity>(); // For turns shuffle.

    public int roundIndex = 0;

    private int turnIndex = 0;

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
        #endregion
        DontDestroyOnLoad(this);
        InitializeGame();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    #endregion

    private void InitializeGame()
    {
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
        }
        #endregion
        RandomizeTurns();
        roundIndex = 0;
        turnIndex = 0;
        TurnStart(boardPlayers[turnIndex]);
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
            // Cache board game state.

            // Start random event. (Minigame, general boost, etc.)
            SceneManager.LoadScene("MainMenu");
        }
        onTurnEnd?.Invoke(entity);

        TurnStart(boardPlayers[turnIndex]);
    }
    #endregion
}