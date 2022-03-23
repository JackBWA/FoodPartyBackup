using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCanvas : MonoBehaviour
{

    public static ItemsCanvas singleton;

    public BoardEntity interactor;

    [HideInInspector]
    public BoardItem_Base selectedItem;

    public List<BoardItem_Base> items;

    public ItemsDataPanelUI itemData;

    public Transform contentParent;

    public ItemUIPrefab prefab;

    private void Awake()
    {
        if(singleton != null)
        {
            enabled = false;
            return;
        }
        singleton = this;
    }

    public void SetItems(List<BoardItem_Base> items)
    {
        this.items = items;
        foreach(BoardItem_Base item in items)
        {
            ItemUIPrefab itemUI = Instantiate(prefab);
            itemUI.transform.SetParent(contentParent);
            itemUI.item = item;
            itemUI.icon = item.icon;
            itemUI.InitializeOnClick();
        }
        itemData.enabled = false;
    }

    private void OnEnable()
    {
        //Debug.Log("Enableeee");
        //gameObject.SetActive(true); // No va porque al deshabilitar no se detecta. Metodologia cambiada. :)
    }

    private void OnDisable()
    {
        //Debug.Log("Disableeee");
    }
}
