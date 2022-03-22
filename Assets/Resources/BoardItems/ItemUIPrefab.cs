using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemUIPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public BoardItem_Base item;

    [SerializeField] private Image itemImage;

    public Sprite icon
    {
        get
        {
            return itemImage.sprite;
        }
        set
        {
            itemImage.sprite = value;
        }
    }

    private Button button;

    private void Awake()
    {
        TryGetComponent(out button);
    }

    public void InitializeOnClick()
    {
        button.onClick.AddListener(delegate { Clicked(); });
    }

    public void Clicked()
    {
        // When clicks lol.
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemsCanvas.singleton.itemData.itemName = item.name;
        ItemsCanvas.singleton.itemData.itemDescription = item.description;
        ItemsCanvas.singleton.itemData.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemsCanvas.singleton.itemData.itemName = string.Empty;
        ItemsCanvas.singleton.itemData.itemDescription = string.Empty;
        ItemsCanvas.singleton.itemData.enabled = false;
    }
}
