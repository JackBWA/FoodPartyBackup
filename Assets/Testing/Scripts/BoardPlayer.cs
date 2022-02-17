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
        playerControls.Dice.Throw.performed += _ => ThrowDice();
        //Debug.Log("Player");
    }

    private void InitializeControls()
    {
        playerControls = new BoardPlayerControls();
    }

    public override void InitializeEntity()
    {
        InitializeControls();
        base.InitializeEntity();
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
