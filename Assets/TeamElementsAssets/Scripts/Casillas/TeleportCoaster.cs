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
        teleportTarget = next[next.Count - 1];
        next.Remove(teleportTarget);
    }

    protected override void RequestInteract(BoardEntity interactor, string title = "Request", string message = "Message", string acceptText = "Accept", string declineText = "Decline")
    {
        base.RequestInteract(interactor, "Teleport request", "Would you like to teleport?", acceptText, declineText);
    }

    public override void Interact(BoardEntity interactor)
    {
        base.Interact(interactor);
        Debug.Log("Teleport interact!");
        playerLeave(interactor);
        Vector3 zone = teleportTarget.GetAvailableWaitZones()[0];
        interactor.TeleportTo(zone);
        interactor.currentCoaster = teleportTarget;
        interactor.currentCoaster.SetWaitZoneState(teleportTarget.GetAvailableWaitZones()[0], interactor);
        EndInteract(interactor);
    }

    public override void EndInteract(BoardEntity interactor)
    {
        base.EndInteract(interactor);
    }

    public override void playerEnter(BoardEntity entity, Vector3 position)
    {
        base.playerEnter(entity, position);
    }

    protected override void RequestChecker(BoardEntity entity)
    {
        switch (entity.GetComponent<PlayerCharacter>().characterType)
        {
            case PlayerCharacter.CharacterType.Player:
                base.RequestChecker(entity);
                break;

            case PlayerCharacter.CharacterType.AI:
                int rnd = Random.Range(0, 2);
                Debug.Log(rnd);
                if(rnd >= 1)
                {
                    Interact(entity);
                } else
                {
                    if (entity.GetMoves() > 0) entity.ContinueMoving();
                    else entity.TurnEnd();
                }
                break;
        }
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