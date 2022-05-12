using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardUIElement : MonoBehaviour
{
    public int amount
    {
        get
        {
            return int.Parse(_amount.text.Substring(1, _amount.text.Length));
        }
        set
        {
            _amount.text = $"x{value}";
        }
    }
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

    [SerializeField]
    private TextMeshProUGUI _amount;
    [SerializeField]
    private Image _icon;
}