using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardPlayer : BoardEntity
{
    #region Input
    public BoardPlayerControls playerControls;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerControls = new BoardPlayerControls();
        LoadInputs();
    }

    private void LoadInputs()
    {
        playerControls.Camera.Lock.performed += _ => LockTPC();
        playerControls.Camera.Lock.canceled += _ => UnlockTPC();
        playerControls.Info.Toggle.performed += _ =>
        {
            if (InputsDisplayer.singleton != null) InputsDisplayer.singleton.ToggleVisibility();
        };
        playerControls.Dice.Throw.performed += _ => ThrowDice();
        playerControls.Map.Toggle.performed += _ => ToggleMapView();
        playerControls.Inventory.TestItem.performed += _ => inventory.UseItem(inventory.items.ElementAt(0).Key);
    }

    public void xdaa()
    {
        GameBoardManager.singleton.GameEnd();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override void BindEvents()
    {
        base.BindEvents();
    }

    protected override void UnbindEvents()
    {
        base.UnbindEvents();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerControls.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerControls.Disable();
    }
}