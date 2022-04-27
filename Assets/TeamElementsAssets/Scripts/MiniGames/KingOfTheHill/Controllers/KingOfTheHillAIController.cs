using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KingOfTheHillAIController : KingOfTheHillController
{
    public NavMeshAgent agent;

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
    }
    #endregion

    public void GoToArea()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        MiniGame.singleton.onMinigameStart += GoToArea;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MiniGame.singleton.onMinigameStart -= GoToArea;
    }

    public void TeleportTo(Vector3 position)
    {
        if (agent != null) agent.enabled = false;
        agent.Warp(position + new Vector3(0f, transform.localScale.y, 0f));
        if (agent != null) agent.enabled = true;
    }

    public override void Initialize()
    {
        base.Initialize();
        if (!gameObject.TryGetComponent(out agent))
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.speed = 8f;
        }
    }

    protected override IEnumerator coStun(Vector3 force)
    {
        isStunned = true;
        agent.enabled = false;
        rB.AddForce(force, ForceMode.VelocityChange);
        yield return new WaitForSeconds(.25f);
        while (!IsGrounded())
        {
            yield return new WaitForSeconds(.5f);
        }
        isStunned = false;
        agent.enabled = true;
        GoToArea();
        yield return null;
    }
}