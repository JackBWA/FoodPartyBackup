using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameDebugQuit : MonoBehaviour
{

    MiniGame minigame;

    float timer;

    void Start()
    {
        TryGetComponent(out minigame);
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 10f)
        {
            minigame.MinigameExit();
            Destroy(this);
        }
    }
}
