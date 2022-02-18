using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamesManager : MonoBehaviour
{
    public List<SceneAsset> minigames;
    public SceneAsset forcedNextMinigame;

    public static MiniGamesManager singleton;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        #region Singleton
        if (singleton != null)
        {
            Debug.LogWarning("Multiple MiniGamesManager detected. This one has been disabled.");
            enabled = false;
            return;
        }
        singleton = this;
        #endregion
    }

    public void LoadRandomMinigame()
    {
        if (forcedNextMinigame != null)
        {
            SceneAsset aux = forcedNextMinigame;
            forcedNextMinigame = null;
            SceneManager.LoadScene(aux.name);
            return;
        }

        string randomMinigame = minigames[Random.Range(0, minigames.Count)].name;
        SceneManager.LoadScene(randomMinigame);
    }
}
