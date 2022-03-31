using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
