using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuEventSystem : MonoBehaviour
{

    public void Hi()
    {
        Debug.Log("Hi");
    }

    #region Variables

    #endregion

    #region Events
    public void PressSingleplayer()
    {
        StartSingleplayerGame();
    }

    public void PressMultiplayer()
    {
        ShowMultiplayerMenu();
    }

    public void PressOptions()
    {
        ShowOptionsMenu();
    }

    public void PressExit()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    #endregion

    #region Private Methods
    private void StartSingleplayerGame()
    {
        // Hardcoded de momento.
        SceneManager.LoadScene("SingleplayerConfig");
    }

    private void ShowMultiplayerMenu()
    {
        Debug.Log("Coming soon...");
    }

    private void ShowOptionsMenu()
    {
        Debug.Log("WIP");
    }
    #endregion
}