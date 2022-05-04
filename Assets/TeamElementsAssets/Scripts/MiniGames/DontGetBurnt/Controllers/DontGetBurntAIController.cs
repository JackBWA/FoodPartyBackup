using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DontGetBurntAIController : DontGetBurntController
{

    public NavMeshAgent controller;

    public List<Vector3> targetPoints = new List<Vector3>();

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (playerCharacter != null) playerCharacter.animManager.ator.SetFloat("Speed", controller.velocity.magnitude / controller.speed);
    }
    #endregion

    public override void Initialize()
    {
        base.Initialize();
        if (!gameObject.TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<NavMeshAgent>();
        }

        foreach(Stove s in ((MiniGame_DontGetBurnt)MiniGame.singleton).stoves)
        {
            targetPoints.Add(s.transform.position);
        }
    }

    public void TeleportTo(Vector3 position)
    {
        if (controller != null) controller.enabled = false;
        controller.Warp(position + new Vector3(0f, transform.localScale.y, 0f));
        if (controller != null) controller.enabled = true;
    }

    public void SwapStove()
    {
        Vector3 random = targetPoints[UnityEngine.Random.Range(0, targetPoints.Count)];
        controller.SetDestination(random);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //MiniGame.singleton.onMinigameStart += SwapStove;
        ((MiniGame_DontGetBurnt)MiniGame.singleton).onTriggerStove += SwapStove;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //MiniGame.singleton.onMinigameStart -= SwapStove;
        ((MiniGame_DontGetBurnt)MiniGame.singleton).onTriggerStove -= SwapStove;
    }
}