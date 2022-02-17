using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAI : BoardEntity
{

    protected override void Awake()
    {
        base.Awake();
        //Debug.Log("AI");
    }

    public override void InitializeEntity()
    {
        base.InitializeEntity();
    }

    protected override void BindEvents()
    {
        base.BindEvents();
        onTurnStart += ThrowDice;
    }

    protected override void UnbindEvents()
    {
        base.BindEvents();
        onTurnStart -= ThrowDice;
    }
}
