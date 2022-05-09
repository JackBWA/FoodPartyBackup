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
            itemImage.maskable = false;
        }
    }

    private Button button;

    private void Awake()
    {
        TryGetComponent(out button);
    }

    public void InitializeOnClick()
    {
        button.onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        ItemsCanvas.singleton.interactor.inventory.UseItem(item);

        /* // No hacia falta ya estaba hecho arriba xd.
        BoardItem_Base _item = Instantiate(item, interactor.transform.position + interactor.transform.forward + (interactor.transform.up * 2.5f), ItemsCanvas.singleton.interactor.transform.rotation);
        _item.owner = interactor;
        interactor.inventory.StartUsingItem(_item);
        */



        // This should be called once item using is done.
        /*
         * ItemsCanvas.singleton.interactor.inventory.itemsCanvasInstance.enabled = false;
           ItemsCanvas.singleton.interactor.inventory.itemsCanvasInstance.gameObject.SetActive(false);
        */
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer enter");
        ItemsCanvas.singleton.itemData.itemName = item.name;
        ItemsCanvas.singleton.itemData.itemDescription = item.description;

        //Debug.Log($"Is null? {ItemsCanvas.singleton.itemData == null}");
        //Debug.Log(ItemsCanvas.singleton.itemData);

        ItemsCanvas.singleton.itemData.enabled = true;
        ItemsCanvas.singleton.itemData.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer exit");
        ItemsCanvas.singleton.itemData.itemName = string.Empty;
        ItemsCanvas.singleton.itemData.itemDescription = string.Empty;
        ItemsCanvas.singleton.itemData.enabled = false;
        ItemsCanvas.singleton.itemData.gameObject.SetActive(false);
    }
}
