using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    public string minigameName;
    [TextArea]
    public string minigameDescription;

    public List<Vector3> spawnZones = new List<Vector3>();

    public List<PlayerCharacter> players = new List<PlayerCharacter>();

    public static MiniGame singleton;

    #region Events
    public event Action onMinigameStart;
    public void MiniGameStart()
    {
        onMinigameStart?.Invoke();
    }

    public event Action onMinigameFinish;
    public void MinigameFinish()
    {
        onMinigameFinish?.Invoke();
    }

    public event Action onMinigameExit;
    public void MinigameExit()
    {
        onMinigameExit?.Invoke();
    }
    #endregion

    private void OnEnable()
    {
        onMinigameExit += GameBoardManager.singleton.EventEnd;
    }

    private void OnDisable()
    {
        onMinigameExit -= GameBoardManager.singleton.EventEnd;
    }

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        #region Singleton
        singleton = this;
        #endregion

        #region Players
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("SpawnZone"))
        {
            spawnZones.Add(gO.transform.position);
        }
        InitializePlayers();
        SpawnPlayers();
        #endregion

        #region Countdown

        #endregion
    }

    protected virtual void Start()
    {
        Time.timeScale = 1f; // Temporal xd.
    }

    protected virtual void Update()
    {

    }
    #endregion

    protected virtual void InitializePlayers()
    {
        PlayerCharacter playerChar = Instantiate(CharacterManager.selectedCharacter);
        DontGetBurntPlayerController playerController = playerChar.gameObject.AddComponent<DontGetBurntPlayerController>();
        playerController.Initialize();
        players.Add(playerChar);
        for (int i = 0; i < CharacterManager.aiCharacters.Count; i++)
        {
            PlayerCharacter aiChar = Instantiate(CharacterManager.aiCharacters[i]);
            DontGetBurntAIController aiController = aiChar.gameObject.AddComponent<DontGetBurntAIController>();
            // Initialize controller.
            aiController.Initialize();
            players.Add(aiChar);
        }
    }

    protected virtual void SpawnPlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnZones[Mathf.Clamp(i, 0, spawnZones.Count - 1)];
        }
    }
}