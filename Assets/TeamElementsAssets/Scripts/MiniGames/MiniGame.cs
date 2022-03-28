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
        singleton = this;
        foreach (GameObject gO in GameObject.FindGameObjectsWithTag("SpawnZone"))
        {
            spawnZones.Add(gO.transform.position);
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    #endregion
}