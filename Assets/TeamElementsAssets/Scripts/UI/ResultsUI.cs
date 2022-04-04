using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    public PlayerResult playerResultUIPrefab;

    public float resultsDisplayTime = 10f;

    public string winnerText
    {
        get
        {
            return _winnerText.text;
        }
        set
        {
            _winnerText.text = value;
        }
    }
    public Transform scoresListParent
    {
        get
        {
            return _scoresListParent;
        }
        set
        {
            _scoresListParent = value;
        }
    }
    public string leavingText
    {
        get
        {
            return _leavingText.text;
        }
        set
        {
            _leavingText.text = value;
        }
    }

    [SerializeField]
    private TextMeshProUGUI _winnerText;
    [SerializeField]
    private Transform _scoresListParent;
    [SerializeField]
    private TextMeshProUGUI _leavingText;

    private void LoadResults()
    {
        Dictionary<PlayerCharacter, int> sortedResults = new Dictionary<PlayerCharacter, int>();
        foreach (KeyValuePair<PlayerCharacter, int> kV in MiniGame.singleton.playerScores.OrderByDescending(ctx => ctx.Value))
        {
            sortedResults.Add(kV.Key, kV.Value);
        }

        winnerText = $"{sortedResults.ElementAt(0).Key.name} is the winner!";

        int i = 1;
        foreach (KeyValuePair<PlayerCharacter, int> kV in MiniGame.singleton.playerScores.OrderByDescending(ctx => ctx.Value))
        {
            PlayerResult instance = Instantiate(playerResultUIPrefab);
            instance.position = i;
            instance.playerName = kV.Key.name;
            instance.resultScore = kV.Value;

            instance.transform.SetParent(scoresListParent);

            i++;
        }
        StartCoroutine(ExitCountdown());
    }
    
    private IEnumerator ExitCountdown()
    {
        float timeLeft = resultsDisplayTime;
        while(timeLeft >= 0f)
        {
            timeLeft -= Time.deltaTime;
            leavingText = $"Leaving in {((int) timeLeft) + 1} seconds...";
            yield return new WaitForEndOfFrame();
        }
        MiniGame.singleton.MinigameExit();
    }

    private void OnEnable()
    {
        LoadResults();
    }

    private void OnDisable()
    {
        
    }
}