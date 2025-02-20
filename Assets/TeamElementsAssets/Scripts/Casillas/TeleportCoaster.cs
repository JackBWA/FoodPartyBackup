using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCoaster : Coaster
{

    public Coaster teleportTarget;

    public GameObject onTeleportParticlePrefab;

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

    protected override void RequestInteract(BoardEntity interactor, string title = "Petici�n", string message = "Mensaje", string acceptText = "Si", string declineText = "No")
    {
        base.RequestInteract(interactor, "Teletransportador", "Te gustaria teletransportarte?", acceptText, declineText);
    }

    public override void Interact(BoardEntity interactor)
    {
        PlayerCharacter pC = interactor.GetComponent<PlayerCharacter>();
        if (pC != null && pC.characterType == PlayerCharacter.CharacterType.Player && !PlayerPrefs.HasKey(GetType().ToString()))
        {
            PlayerPrefs.SetInt(GetType().ToString(), 1);
            DisplayInfo(interactor/*title, description*/);
        } else
        {
            base.Interact(interactor);
            StartCoroutine(Teleport(interactor));
        }
    }

    private IEnumerator Teleport(BoardEntity interactor)
    {
        playerLeave(interactor);
        Vector3 zone = teleportTarget.GetAvailableWaitZones()[0];
        ParticleSystem pS = Instantiate(onTeleportParticlePrefab).GetComponentInChildren<ParticleSystem>();
        pS.transform.position = interactor.transform.position;
        interactor.playerCharacter._renderer.enabled = false;
        yield return new WaitForSeconds(1f);
        interactor.TeleportTo(zone);
        interactor.currentCoaster = teleportTarget;
        //interactor.currentCoaster.SetWaitZoneState(teleportTarget.GetAvailableWaitZones()[0], interactor);
        yield return new WaitForSeconds(1f);
        pS = Instantiate(onTeleportParticlePrefab).GetComponentInChildren<ParticleSystem>();
        pS.transform.position = interactor.transform.position;
        yield return new WaitForSeconds(.1f);
        interactor.playerCharacter._renderer.enabled = true;
        yield return new WaitForSeconds(1f);
        interactor.currentCoaster.playerEnter(interactor, zone);
        //EndInteract(interactor); // PlayerEnter instead???
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
                //Debug.Log(rnd);
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