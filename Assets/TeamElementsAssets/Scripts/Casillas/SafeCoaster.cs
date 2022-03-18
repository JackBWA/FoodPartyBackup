using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCoaster : Coaster
{

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
        Debug.Log("Safe interact!");
        EndInteract(interactor);
    }

    public override void EndInteract(BoardEntity interactor)
    {
        base.EndInteract(interactor);
    }

    public override void playerEnter(BoardEntity entity, Vector3 position)
    {
        base.playerEnter(entity, position);
        entity.isSafe = true;
    }

    public override void playerStop(BoardEntity entity)
    {
        base.playerStop(entity);
        entity.isSafe = true;
    }

    public override void playerLeave(BoardEntity entity)
    {
        base.playerLeave(entity);
        entity.isSafe = false;
    }
}
