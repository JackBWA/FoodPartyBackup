using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using Cinemachine;

public class MainMenuEventSystem : MonoBehaviour
{

    #region Variables

    public GameObject contentParent;

    public OptionsMenu optionsMenu;

    public CinemachineVirtualCamera mainMenuVCam;
    public CinemachineVirtualCamera initialPositionVCam;

    public enum CurrentMenu
    {
        MAIN,
        OPTIONS
    }
    #endregion

    private void Awake()
    {
        DisplayContent(0);
    }

    private void Start()
    {
        mainMenuVCam.gameObject.SetActive(true);
        initialPositionVCam.gameObject.SetActive(false);
    }

    #region Events
    public void PressMainMenu()
    {
        ShowMainMenu();
    }

    public void PressSingleplayer()
    {
        ShowCharacterSelection();
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
    private void ShowMainMenu()
    {
        DisplayContent(0);
    }   

    private void ShowCharacterSelection()
    {
        //SceneManager.LoadScene("SingleplayerConfig");
        CharacterManager charMan = CharacterManager.singleton;
        if(charMan != null && charMan.playableCharacters.Count <= 0)
        {
            charMan.LoadPlayableCharacters();
        }
        charMan.UpdateCharacter();
        DisplayContent(2);
    }

    private void ShowMultiplayerMenu()
    {
        Debug.Log("Coming soon...");
    }

    private void ShowOptionsMenu()
    {
        DisplayContent(1);
    }
    #endregion

    #region Public Methods
    public void DisplayContent(int index)
    {
        for(int i = 0; i < contentParent.transform.childCount; i++)
        {
            if(i == index)
            {
                contentParent.transform.GetChild(i).gameObject.SetActive(true);
            } else
            {
                contentParent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    #endregion
}