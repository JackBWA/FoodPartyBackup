using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCoaster : Coaster
{

    public Coaster teleportTarget;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(BoardEntity interactor)
    {
        base.Interact(interactor);
        Debug.Log("Teleport interact!");
    }

    public override void EndInteract(BoardEntity interactor)
    {
        base.EndInteract(interactor);
    }

    public override void playerEnter(BoardEntity entity, Vector3 position)
    {
        base.playerEnter(entity, position);
    }

    public override void playerStop(BoardEntity entity)
    {
        base.playerStop(entity);
    }

    public override void playerLeave(BoardEntity entity)
    {
        base.playerLeave(entity);
    }
}
