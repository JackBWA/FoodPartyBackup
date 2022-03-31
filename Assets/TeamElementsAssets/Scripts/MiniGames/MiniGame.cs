using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{

    public static MiniGame singleton;

    public string minigameName = "Awesome Minigame";
    [TextArea]
    public string minigameDescription = "Use this box to describe the minigame objective.";

    public bool hasTimeLimit = true;
    private float timeLeft;
    public float timeLimit = 60f;

    public bool hasScoreLimit = false;
    public int scoreLimit = 1000;

    [HideInInspector]
    public List<Vector3> spawnZones = new List<Vector3>();

    public List<PlayerCharacter> players = new List<PlayerCharacter>();
    public Dictionary<PlayerCharacter, int> playerScores = new Dictionary<PlayerCharacter, int>();

    public MinigameUI minigameUI;
    public TutorialUI tutorialUI;
    public CountdownUI countdownUI;
    public ResultsUI resultsUI;

    #region Events
    public event Action onMinigameEnter;
    public void MinigameEnter()
    {
        DisplayMinigameUI(false);
        DisplayTutorialUI(true);
        DisplayCountdownUI(false);
        DisplayResultsUI(false);
        onMinigameEnter?.Invoke();
    }

    public event Action onMinigamePreStart;
    public void MinigamePreStart()
    {
        DisplayMinigameUI(false);
        DisplayTutorialUI(false);
        DisplayCountdownUI(true);
        DisplayResultsUI(false);
        onMinigamePreStart?.Invoke();
    }

    public event Action onMinigameStart;
    public void MinigameStart()
    {
        DisplayMinigameUI(true);
        DisplayTutorialUI(false);
        DisplayCountdownUI(false);
        DisplayResultsUI(false);
        onMinigameStart?.Invoke();
    }

    public event Action onMinigameFinish;
    public void MinigameFinish()
    {
        DisplayMinigameUI(false);
        DisplayTutorialUI(false);
        DisplayCountdownUI(false);
        DisplayResultsUI(true);
        onMinigameFinish?.Invoke();
    }

    public event Action onMinigameExit;
    public void MinigameExit()
    {
        onMinigameExit?.Invoke();
    }
    #endregion

    #region UI
    public void DisplayMinigameUI(bool display)
    {
        minigameUI.enabled = display;
        minigameUI.gameObject.SetActive(display);
    }
    public void DisplayTutorialUI(bool display)
    {
        tutorialUI.enabled = display;
        tutorialUI.gameObject.SetActive(display);
    }
    public void DisplayCountdownUI(bool display)
    {
        countdownUI.enabled = display;
        countdownUI.gameObject.SetActive(display);
    }
    public void DisplayResultsUI(bool display)
    {
        resultsUI.enabled = display;
        resultsUI.gameObject.SetActive(display);
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

        #region Players & SpawnZones
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("SpawnZone"))
        {
            spawnZones.Add(gO.transform.position);
        }
        InitializePlayers();
        SpawnPlayers();
        #endregion

        MinigameEnter();
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