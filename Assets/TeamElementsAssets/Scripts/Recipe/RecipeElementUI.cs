using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeElementUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI amount;

    public void SetImage(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.preserveAspect = true;
    }

    public void SetAmount(int current, int required)
    {
        amount.text = $"{current}/{required}";
    }
}
