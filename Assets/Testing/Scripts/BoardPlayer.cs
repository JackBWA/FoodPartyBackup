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
        TryGetComponent(out agent);
        playerControls = new BoardPlayerControls();
        playerControls.Dice.Throw.performed += _ => ThrowDice();
        //Debug.Log("Player");
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
