using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAI : BoardEntity
{

    protected override void Awake()
    {
        base.Awake();
        onTurnStart += ThrowDice;
        //Debug.Log("AI");
    }
}
