using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDataPanelUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;

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

    public string itemDescription
    {
        get
        {
            return _itemDescription.text;
        }
        set
        {
            _itemDescription.text = value;
        }
    }

    private void Awake()
    {
        /*
        if (!TryGetComponent(out _itemName)) _itemName = gameObject.AddComponent<TextMeshProUGUI>();
        if (!TryGetComponent(out _itemDescription)) _itemDescription = gameObject.AddComponent<TextMeshProUGUI>();
        */
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
