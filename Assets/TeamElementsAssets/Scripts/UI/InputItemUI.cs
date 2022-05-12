using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputItemUI : MonoBehaviour
{
    public Sprite inputImage
    {
        get
        {
            return _inputImage.sprite;
        }
        set
        {
            _inputImage.sprite = value;
        }
    }

    public string inputText
    {
        get
        {
            return _inputText.text;
        }
        set
        {
            _inputText.text = value;
        }
    }

    [SerializeField]
    private Image _inputImage;
    [SerializeField]
    private TextMeshProUGUI _inputText;
}