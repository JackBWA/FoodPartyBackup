using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{

    public float displayTime = 2f;

    public string text
    {
        get
        {
            return _text.text;
        }
        set
        {
            _text.text = value;
        }
    }
    [SerializeField]
    private TextMeshProUGUI _text;
}