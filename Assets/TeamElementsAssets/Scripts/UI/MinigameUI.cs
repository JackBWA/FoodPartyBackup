using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameUI : MonoBehaviour
{
    public int timer
    {
        get
        {
            return int.Parse(_timer.text);
        }
        set
        {
            _timer.text = $"{value}";
        }
    }
    [SerializeField]
    private TextMeshProUGUI _timer;

    public Transform scoresParentUI;

    public PlayerScore playerScoreUIPrefab;
    public List<PlayerScore> scoresUI = new List<PlayerScore>();

    private void CreateUI()
    {
        foreach (PlayerCharacter pC in MiniGame.singleton.players)
        {
            PlayerScore instance = Instantiate(playerScoreUIPrefab);
            instance.characterReference = pC;
            // Set icon lol.
            instance.transform.SetParent(scoresParentUI);
            scoresUI.Add(instance);
            MiniGame.singleton.playerScores.Add(pC, instance.score);
        }
    }

    private void OnEnable()
    {
        if(scoresUI == null || scoresUI.Count == 0)
        {
            CreateUI();
        }
        MiniGame.singleton.onTimeLeftChange += UpdateTimer;
        MiniGame.singleton.StartTimer();
    }

    private void OnDisable()
    {
        MiniGame.singleton.onTimeLeftChange -= UpdateTimer;
        MiniGame.singleton.StopTimer();
    }

    public void UpdateTimer(float newValue)
    {
        timer = (int) newValue;
    }
}
