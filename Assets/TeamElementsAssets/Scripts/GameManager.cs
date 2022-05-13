using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;
    public static int maxPlayers = 4;

    /*
    public enum GameState
    {
        DEFAULT,
        MAIN_MENU,
        CREATING_GAME,
        LOOKING_FOR_MULTIPLAYER_GAME,
        JOINING_LOBBY,
        IN_LOBBY,
        SELECTING_CHARACTER,
        LOADING_SCREEN,
        IN_GAME,
        AFK
    }

    public GameState gameState;
    */

    private void Awake()
    {
        #region Singleton
        if (singleton != null)
        {
            Debug.LogWarning($"Multiple instances of GameManager detected. Destroying {gameObject.name}.");
            Destroy(gameObject);
            return;
        }
        singleton = this;
        #endregion
        //gameState = GameState.MAIN_MENU;
        DontDestroyOnLoad(this);
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        PauseManager.singleton.canToggle = false;
    }
}