using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;

    private void Awake()
    {
        #region Singleton
        if (singleton != null)
        {
            Debug.LogWarning($"Multiple instanced of GameManager detected. Disabling {this.gameObject.name}.");
            enabled = false;
            return;
        }
        singleton = this;
        DontDestroyOnLoad(this);
        #endregion
    }
}