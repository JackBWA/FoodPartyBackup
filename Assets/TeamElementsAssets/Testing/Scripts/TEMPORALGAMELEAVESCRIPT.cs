using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMPORALGAMELEAVESCRIPT : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(JustLeaveLol());
    }

    public IEnumerator JustLeaveLol()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Board1");
    }
}
