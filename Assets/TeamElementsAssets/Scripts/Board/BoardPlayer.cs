using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardPlayer : BoardEntity
{
    #region Input
    public BoardPlayerControls playerControls;

    public PlayerActions xd;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerControls = new BoardPlayerControls();
        xd = new PlayerActions();
        LoadInputs();
    }

    private void LoadInputs()
    {
        xd.Pause.lalalalala.performed += _ => xdaa();
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
        xd.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerControls.Disable();
        xd.Disable();
    }
}
