using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        playerControls.Dice.Throw.performed += _ => ThrowDice();
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
        base.BindEvents();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
