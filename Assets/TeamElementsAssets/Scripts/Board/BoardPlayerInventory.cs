using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayerInventory : BoardEntityInventory
{
    public BoardPlayerControls inputActions;

    protected override void Awake()
    {
        base.Awake();
        //ToggleItemsUI();
        inputActions = new BoardPlayerControls();
        inputActions.Inventory.Toggle.performed += _ =>
        {
            visible = !visible;
            ToggleItemsUI();
        };
    }

    public void ToggleItemsUI()
    {
        itemsCanvasInstance.enabled = visible;
        itemsCanvasInstance.gameObject.SetActive(visible);
    }

    public override void Create()
    {
        base.Create();
        inputActions.Enable();
    }

    public override void Delete()
    {
        base.Delete();
        inputActions.Disable();
    }
}