using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager : MonoBehaviour
{

    public static GameBoardManager singleton;

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
        /*switch (player.characterType)
        {
            case PlayerCharacter.CharacterType.AI:
                player.gameObject.AddComponent<BoardAI>();
                //player.gameObject.AddComponent<BoardAI>().Initialize();
                break;

            case PlayerCharacter.CharacterType.Player:
                player.gameObject.AddComponent<BoardPlayer>();
                //player.gameObject.AddComponent<BoardPlayer>().Initialize();
                break;
        }
        */
        BoardPlayer bP = player.gameObject.AddComponent<BoardPlayer>();
        bP.Initialize();
        players.Add(player);

        foreach(PlayerCharacter ai in CharacterManager.aiCharacters)
        {
            PlayerCharacter aiPlayer = Instantiate(ai);
            /*switch (aiPlayer.characterType)
            {
                case PlayerCharacter.CharacterType.AI:
                    aiPlayer.gameObject.AddComponent<BoardAI>();
                    break;

                case PlayerCharacter.CharacterType.Player:
                    aiPlayer.gameObject.AddComponent<BoardPlayer>();
                    break;
            }
            */
            BoardAI aiP = aiPlayer.gameObject.AddComponent<BoardAI>();
            aiP.Initialize();
            players.Add(aiPlayer);
        }

        // Teleport them to the coaster's waiting zones.
        foreach (PlayerCharacter p in players)
        {
            p.GetComponent<BoardEntity>().TeleportTo(Coaster.initialCoaster, Coaster.initialCoaster.GetAvailableWaitZones()[0]);
        }
        #endregion
        RandomizeTurns();
        // Start
    }

    public void RandomizeTurns()
    {

    }

    #region Events

    #endregion
}