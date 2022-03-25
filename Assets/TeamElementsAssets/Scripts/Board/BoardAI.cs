using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAI : BoardEntity
{

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    protected override void BindEvents()
    {
        base.BindEvents();
        onTurnStart += ThrowDice;
    }

    protected override void UnbindEvents()
    {
        base.UnbindEvents();
        onTurnStart -= ThrowDice;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
