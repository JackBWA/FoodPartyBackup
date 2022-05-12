using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BoardItem_Base
{

    BoardItemControls inputActions;

    public TeleportCanvas canvasPrefab;

    TeleportCanvas canvasInstance;

    public BoardEntity target;

    public GameObject teleportParticlesPrefab;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new BoardItemControls();
        InitializeControls();
    }

    private IEnumerator SetTargets()
    {
        while(owner == null)
        {
            yield return new WaitForEndOfFrame();
        }
        canvasInstance.SetTargets(owner);
    }

    private void OnEnable()
    {
        inputActions.Enable();
        canvasInstance = Instantiate(canvasPrefab);
        canvasInstance.item = this;
        StartCoroutine(SetTargets());
    }

    private void OnDisable()
    {
        inputActions.Disable();
        if (canvasInstance != null) Destroy(canvasInstance.gameObject);
    }

    public void InitializeControls()
    {
        inputActions.General.Use.performed += _ => Use();
        inputActions.General.Cancel.performed += _ => Cancel();
    }

    public override void Use()
    {
        base.Use();
        StartCoroutine(TeleportToPlayer());
    }

    public override void Cancel()
    {
        target.DeactivateTPC();
        owner.ActivateTPC();
        base.Cancel();
    }

    private IEnumerator TeleportToPlayer()
    {
        canvasInstance.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        owner.currentCoaster.playerLeave(owner);
        owner.currentCoaster = target.currentCoaster;
        Vector3 zone = owner.currentCoaster.GetAvailableWaitZones()[0];
        ParticleSystem pS = Instantiate(teleportParticlesPrefab).GetComponentInChildren<ParticleSystem>();
        pS.transform.position = owner.transform.position;
        owner.playerCharacter._renderer.enabled = false;
        yield return new WaitForSeconds(1f);
        owner.TeleportTo(zone);
        owner.currentCoaster.SetWaitZoneState(zone, owner);
        yield return new WaitForSeconds(1f);
        pS = Instantiate(teleportParticlesPrefab).GetComponentInChildren<ParticleSystem>();
        pS.transform.position = owner.transform.position;
        yield return new WaitForSeconds(.1f);
        owner.playerCharacter._renderer.enabled = true;
        owner.GetDice().transform.position = owner.transform.position + Vector3.up * 5f;
        Destroy(canvasInstance.gameObject);
        yield return new WaitForSeconds(2.5f);
        //owner.currentCoaster.playerEnter(owner, zone);
        EndUse();
    }
}