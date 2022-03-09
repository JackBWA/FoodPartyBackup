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
        yield return new WaitForSeconds(20f);
        MiniGame.singleton.MinigameExit();
    }
}
