using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayerInventory : BoardEntityInventory
{
    public BoardPlayerControls inputActions;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new BoardPlayerControls();
        inputActions.Inventory.Toggle.performed += _ => ToggleItemsUI();
    }

    public void ToggleItemsUI()
    {
        if(itemsCanvasInstance == null)
        {
            itemsCanvasInstance = Instantiate(itemsCanvasPrefab);
            List<BoardItem_Base> auxItems = new List<BoardItem_Base>();
            foreach(KeyValuePair<BoardItem_Base, int> kV in items)
            {
                auxItems.Add(kV.Key);
            }
            itemsCanvasInstance.SetItems(auxItems);
        } else
        {
            Destroy(itemsCanvasInstance.gameObject);
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
