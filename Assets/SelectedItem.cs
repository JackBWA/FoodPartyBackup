using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{

    public Shop shop;

    public ShopElementUI selected { get; private set; }

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
        }
    }
    public int itemAmount
    {
        get
        {
            return _itemAmount;
        }
        set
        {
            _itemAmount = Mathf.Clamp(value, 0, selected.amount);
            //Debug.Log(_itemAmount);
            UpdateAmountsDisplay();
        }
    }

    public int interactorAmount
    {
        get
        {
            return _interactorAmount;
        }
        set
        {
            _interactorAmount = value;
            _interactorAmountTxt.text = $"Your amount: {value}";
        }
    }

    public int buyCost
    {
        get
        {
            return selected.recipeElement.buyCost * itemAmount;
        }
        set
        {
            _buyCost.text = $"-{value}";
        }
    }
    public int sellCost
    {
        get
        {
            return selected.recipeElement.sellCost * itemAmount;
        }
        set
        {
            _sellCost.text = $"+{value}";
        }
    }

    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private Image _itemSprite;

    private int _itemAmount;

    private int _interactorAmount;

    [SerializeField]
    private TextMeshProUGUI _interactorAmountTxt;

    [SerializeField]
    private Slider _itemAmountSlider;
    [SerializeField]
    private TMP_InputField _itemAmountInput;

    [SerializeField]
    private TextMeshProUGUI _buyCost;
    [SerializeField]
    private TextMeshProUGUI _sellCost;

    public Button itemBuyButton;
    public Button itemSellButton;

    public void UpdateItemAmountWithoutNotify(int amount)
    {
        _itemAmount = amount;
        buyCost = buyCost;
        sellCost = sellCost;
    }

    private void Awake()
    {
        _itemAmountSlider.onValueChanged.AddListener(UpdateInputValue);
        _itemAmountInput.onValueChanged.AddListener(UpdateSliderValue);
    }

    public void UpdateInputValue(float newValue)
    {
        int finalValue = Mathf.Clamp((int) newValue, 0, selected.amount);
        _itemAmountInput.SetTextWithoutNotify($"{(int) newValue}");
        UpdateItemAmountWithoutNotify((int) newValue);
    }

    public void UpdateSliderValue(string newValue)
    {
        int finalValue = Mathf.Clamp(int.Parse(newValue), 0, selected.amount);
        _itemAmountSlider.SetValueWithoutNotify(finalValue);
        UpdateItemAmountWithoutNotify(finalValue);
    }

    public void SetSelected(ShopElementUI selected)
    {
        this.selected = selected;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        itemName = selected.recipeElement.name;
        itemSprite = selected.recipeElement.icon;
        int amount = 0;
        GameBoardManager.singleton.recipeStates[shop.shopInteractor].currentElements.TryGetValue(selected.recipeElement, out amount);
        //Debug.Log(amount);
        interactorAmount = amount;
        itemAmount = 0;
    }

    public void UpdateAmountsDisplay()
    {
        _itemAmountInput.text = $"{itemAmount}";
        _itemAmountSlider.minValue = 0;
        _itemAmountSlider.maxValue = selected.amount;
        _itemAmountSlider.value = Mathf.Clamp(itemAmount, _itemAmountSlider.minValue, _itemAmountSlider.maxValue);
        buyCost = buyCost;
        sellCost = sellCost;
    }
}