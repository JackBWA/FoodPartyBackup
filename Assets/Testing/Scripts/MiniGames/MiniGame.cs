using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public string minigameName;
    public string minigameDescription;

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
    }
    #endregion
}
