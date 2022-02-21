using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    public string minigameName;
    public string minigameDescription;
    public List<Transform> spawnZones = new List<Transform>();

    public static MiniGame singleton;

    #region Events
    public event Action onMinigameEnter;
    public void MinigameEnter()
    {
        onMinigameEnter?.Invoke();
    }

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
        //LoadBoardScene();
    }
    #endregion

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        singleton = this;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    #endregion

    /*
    public void LoadBoardScene()
    {
        Scene lastMiniGameScene = SceneManager.GetActiveScene();
        SceneManager.CreateScene("BoardGameScene");
        SceneManager.UnloadSceneAsync(lastMiniGameScene);
        BoardGameManager.singleton.LoadState();
    }
    */
}