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
            if (!canUseItem) return;
            visible = !visible;
            ToggleItemsUI();
        };
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