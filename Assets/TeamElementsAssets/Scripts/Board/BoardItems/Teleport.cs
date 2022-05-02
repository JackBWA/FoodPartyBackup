using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : BoardItem_Base
{

    BoardItemControls inputActions;

    public TeleportCanvas canvasPrefab;

    TeleportCanvas canvasInstance;

    public BoardEntity target;

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

    private IEnumerator TeleportToPlayer()
    {
        Destroy(canvasInstance.gameObject);
        owner.currentCoaster.playerLeave(owner);
        owner.currentCoaster = target.currentCoaster;
        Vector3 zone = owner.currentCoaster.GetAvailableWaitZones()[0];
        owner.TeleportTo(zone);
        owner.GetDice().transform.position = owner.transform.position + Vector3.up * 5f;
        yield return new WaitForSeconds(2.5f);
        //owner.currentCoaster.playerEnter(owner, zone);
        EndUse();
    }
}