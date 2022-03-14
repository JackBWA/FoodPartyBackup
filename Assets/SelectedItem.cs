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
            _itemAmount = value;
            UpdateAmountsDisplay();
        }
    }

    public int maxAmount;

    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private Image _itemSprite;

    private int _itemAmount;

    [SerializeField]
    private Slider _itemAmountSlider;
    [SerializeField]
    private TMP_InputField _itemAmountInput;

    public Button itemBuyButton;
    public Button itemSellButton;

    public void UpdateItemAmountWithoutNotify(int value)
    {
        _itemAmount = value;
    }

    private void Awake()
    {
        _itemAmountSlider.onValueChanged.AddListener(UpdateInputValue);
        _itemAmountInput.onValueChanged.AddListener(UpdateSliderValue);
    }

    public void UpdateInputValue(float newValue)
    {
        _itemAmountInput.SetTextWithoutNotify($"{(int) newValue}");
        UpdateItemAmountWithoutNotify((int) newValue);
    }

    public void UpdateSliderValue(string newValue)
    {
        _itemAmountSlider.SetValueWithoutNotify(int.Parse(newValue));
        UpdateItemAmountWithoutNotify(int.Parse(newValue));
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
        itemAmount = selected.amount;
    }

    public void UpdateAmountsDisplay()
    {
        maxAmount = selected.maxAmount;
        _itemAmountInput.text = $"{itemAmount}";
        _itemAmountSlider.minValue = 0;
        _itemAmountSlider.maxValue = maxAmount;
        _itemAmountSlider.value = Mathf.Clamp(selected.amount, _itemAmountSlider.minValue, _itemAmountSlider.maxValue);
    }
}