using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCoaster : Coaster
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Teleport action.");
    }
}
