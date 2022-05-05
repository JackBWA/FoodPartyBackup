using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager singleton;

    public PlayerActions inputActions;

    [HideInInspector]
    public bool canToggle;
    [HideInInspector]
    public bool isPaused
    {
        get
        {
            return _isPaused;
        }
        set
        {
            _isPaused = value;
            PauseStateChanged(isPaused);
        }
    }

    private bool _isPaused;

    private float defaultTimeScale;
    private Canvas canvas;

    public event Action<bool> onPauseStateChanged;
    public void PauseStateChanged(bool isPaused)
    {
        onPauseStateChanged?.Invoke(isPaused);
    }

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(this);

        defaultTimeScale = Time.timeScale;
        canvas = GetComponent<Canvas>();

        inputActions = new PlayerActions();
        inputActions.Pause.Toggle.performed += _ => Toggle();
    }

    private void Start()
    {
        canToggle = false;
        isPaused = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        onPauseStateChanged += ToggleVisibility;
    }

    private void OnDisable()
    {
        if(inputActions != null) inputActions.Disable();
        onPauseStateChanged -= ToggleVisibility;
    }

    private void ToggleVisibility(bool isPaused)
    {
        canvas.enabled = isPaused;
    }

    public void Toggle()
    {
        if (!canToggle) return;
        if (!isPaused)
        {
            Pause();
        } else
        {
            Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = defaultTimeScale;
        isPaused = false;
    }

    public void Exit()
    {
        canToggle = false;
        Resume();
        Destroy(GameBoardManager.singleton.persistentBoardObjects);
        Destroy(CameraBoardManager.singleton.gameObject);
        Destroy(GameBoardManager.singleton.gameObject);
        CharacterManager.aiCharacters.Clear();
        CharacterManager.selectedCharacter = null; // I don't know xd.
        GameManager.singleton.ExitGame();
    }
}