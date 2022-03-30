using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    public float countdownSeconds = 3f;
    public TextMeshProUGUI countdownText;

    private float timer;
    private Coroutine _countdown;

    private void OnEnable()
    {
        _countdown = StartCoroutine(Countdown());
    }

    private void OnDisable()
    {
        StopCoroutine(_countdown);
    }

    public IEnumerator Countdown()
    {
        timer = countdownSeconds;
        while (timer >= 0)
        {
            countdownText.text = $"{(int) timer}";
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(.25f);
        MiniGame.singleton.MinigameStart();
    }
}
