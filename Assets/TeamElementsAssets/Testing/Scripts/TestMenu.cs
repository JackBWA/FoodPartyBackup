using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenu : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Play"))
        {
            SceneManager.LoadScene("Blockout");
        }
    }
}
