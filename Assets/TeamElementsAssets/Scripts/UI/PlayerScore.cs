using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [HideInInspector]
    public PlayerCharacter characterReference;

    public Sprite icon
    {
        get
        {
            return _icon.sprite;
        }
        set
        {
            _icon.sprite = value;
        }
    }
    public int score
    {
        get
        {
            return int.Parse(_score.text);
        }
        set
        {
            _score.text = $"{value}";
        }
    }

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _score;
}
