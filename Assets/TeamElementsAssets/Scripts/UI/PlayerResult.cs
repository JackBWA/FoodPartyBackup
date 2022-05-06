using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerResult : MonoBehaviour
{
    public Sprite icon
    {
        get
        {
            return _icon.sprite;
        }
        set
        {
            _icon.sprite = value;
            _icon.preserveAspect = true;
        }
    }
    public int position
    {
        get
        {
            return int.Parse(_position.text.Substring(1));
        }
        set
        {
            _position.text = $"#{value}";
        }
    }
    public string playerName
    {
        get
        {
            return _playerName.text;
        }
        set
        {
            _playerName.text = value;
        }
    }
    public float resultScore
    {
        get
        {
            return int.Parse(_resultScore.text);
        }
        set
        {
            _resultScore.text = $"{value}";
        }
    }

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _position;
    [SerializeField]
    private TextMeshProUGUI _playerName;
    [SerializeField]
    private TextMeshProUGUI _resultScore;
}
