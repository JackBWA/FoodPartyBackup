using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{

    public enum MinigameState
    {
        STARTING,
        STARTED,
        FINISHED
    };
    protected MinigameState miniGameState = MinigameState.STARTING;

    public static MiniGame singleton;

    public string minigameName = "Awesome Minigame";
    [TextArea]
    public string minigameDescription = "Use this box to describe the minigame objective.";

    public bool hasTimeLimit = true;
    private float timeLeft;
    public float timeLimit = 60f;
    protected Coroutine timerCo;

    public bool hasScoreLimit = false;
    public int scoreLimit = 1000;
    public int scorePerRound = 50;

    [HideInInspector]
    public List<PositionRotation> spawnZones = new List<PositionRotation>();

    public List<PlayerCharacter> players = new List<PlayerCharacter>();
    public Dictionary<PlayerCharacter, int> playerScores = new Dictionary<PlayerCharacter, int>();

    public MinigameUI minigameUI;
    public TutorialUI tutorialUI;
    public CountdownUI countdownUI;
    public ResultsUI resultsUI;

    #region Events
    public event Action<float> onTimeLeftChange;
    public void TimeLeftChange(float newValue)
    {
        onTimeLeftChange?.Invoke(newValue);
    }

    public event Action onMinigameEnter;
    public virtual void MinigameEnter()
    {
        onMinigameEnter?.Invoke();
        Cursor.visible = true;
        miniGameState = MinigameState.STARTING;
        DisplayMinigameUI(false);
        DisplayTutorialUI(true);
        DisplayCountdownUI(false);
        DisplayResultsUI(false);
    }

    public event Action onMinigamePreStart;
    public virtual void MinigamePreStart()
    {
        onMinigamePreStart?.Invoke();
        Cursor.visible = false;
        miniGameState = MinigameState.STARTING;
        DisplayMinigameUI(false);
        DisplayTutorialUI(false);
        DisplayCountdownUI(true);
        DisplayResultsUI(false);
    }

    public event Action onMinigameStart;
    public virtual void MinigameStart()
    {
        onMinigameStart?.Invoke();

        miniGameState = MinigameState.STARTED;
        DisplayMinigameUI(true);
        DisplayTutorialUI(false);
        DisplayCountdownUI(false);
        DisplayResultsUI(false);
    }

    public event Action onMinigameFinish;
    public virtual void MinigameFinish()
    {
        onMinigameFinish?.Invoke();
        Cursor.visible = true;
        miniGameState = MinigameState.FINISHED;
        DisplayMinigameUI(false);
        DisplayTutorialUI(false);
        DisplayCountdownUI(false);
        DisplayResultsUI(true);
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

    protected virtual void OnEnable()
    {
        onMinigameExit += GameBoardManager.singleton.EventEnd;
    }

    protected virtual void OnDisable()
    {
        onMinigameExit -= GameBoardManager.singleton.EventEnd;
    }

    public void StartTimer()
    {
        timerCo = StartCoroutine(TimerStart());
    }

    private IEnumerator TimerStart()
    {
        timeLeft = timeLimit;
        while(timeLeft >= 0f)
        {
            timeLeft -= Time.deltaTime;
            TimeLeftChange(timeLeft);
            yield return new WaitForEndOfFrame();
        }
        MinigameFinish();
        yield return null;
    }

    public void StopTimer()
    {
        StopCoroutine(timerCo);
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
            spawnZones.Add(new PositionRotation(gO.transform.position, gO.transform.rotation));
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
        players.Add(playerChar);
        for (int i = 0; i < CharacterManager.aiCharacters.Count; i++)
        {
            PlayerCharacter aiChar = Instantiate(CharacterManager.aiCharacters[i]);
            players.Add(aiChar);
        }
    }

    protected virtual void SpawnPlayers()
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnZones[Mathf.Clamp(i, 0, spawnZones.Count - 1)].position;
            players[i].transform.rotation = Quaternion.Euler(spawnZones[Mathf.Clamp(i, 0, spawnZones.Count - 1)].rotation);
        }
    }

    public void AddScore(PlayerCharacter pC)
    {
        playerScores[pC] += scorePerRound;
    }

    public void AddScore(PlayerCharacter pC, int score)
    {
        playerScores[pC] += score;
    }

    public void UpdateScores()
    {
        foreach(PlayerScore pS in minigameUI.scoresUI)
        {
            pS.score = playerScores[pS.characterReference];
        }
    }
}