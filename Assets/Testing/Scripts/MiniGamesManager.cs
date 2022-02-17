using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamesManager : MonoBehaviour
{
    public List<Scene> minigames = new List<Scene>();

    public static MiniGamesManager singleton;

    private void Awake()
    {
        #region Singleton
        if(singleton != null)
        {
            Debug.LogWarning("Multiple MiniGamesManager detected. This one has been disabled.");
            enabled = false;
            return;
        }
        singleton = this;
        DontDestroyOnLoad(this);
        #endregion
    }
}
