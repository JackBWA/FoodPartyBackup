using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElementUI : MonoBehaviour
{
    public Shop shop;

    public RecipeElement recipeElement;
    public int amount;
    //public int maxAmount;

    public string itemName
    {
        get
        {
            return _itemName.text;
        }

        set
        {
            _itemName.text = value;
        }
    }
    public Sprite itemSprite
    {
        get
        {
            return _itemSprite.sprite;
        }

        set
        {
            _itemSprite.sprite = value;
            _itemSprite.preserveAspect = true;
        }
    }
    public int itemCost
    {
        get
        {
            return int.Parse(_itemCost.text);
        }

        set
        {
            _itemCost.text = $"{value}";
        }
    }

    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private Image _itemSprite;
    [SerializeField]
    private TextMeshProUGUI _itemCost;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SelectItem);
    }

    public void SelectItem()
    {
        shop.selectedItemPanel.SetSelected(this);
    }
}
