using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class KingOfTheHillAIController : KingOfTheHillController
{
    public NavMeshAgent agent;

    public List<GameObject> targets = new List<GameObject>();

    GameObject currentTarget;

    bool reachedDestination
    {
        get
        {
            return _reachedDestination;
        }
        set
        {
            _reachedDestination = value;
            if (reachedDestination)
            {
                GoToArea();
            }
        }
    }
    bool _reachedDestination;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        targets = GameObject.FindGameObjectsWithTag("Player").ToList();
        targets.Remove(gameObject);
        currentTarget = null;
        StartCoroutine(AimForClosestTarget());
        StartCoroutine(PunchDetect());
    }

    protected override void Update()
    {
        base.Update();
        Vector3 lookDirection = currentTarget.transform.position - transform.position;
        lookDirection.y = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
        if (agent.enabled)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + 0.5f)
            {
                reachedDestination = true;
            }
        }
    }
    #endregion

    private IEnumerator AimForClosestTarget(float checkRate = 1f)
    {
        while (enabled)
        {
            foreach (GameObject t in targets)
            {
                if (currentTarget == null)
                {
                    currentTarget = t;
                    continue;
                }
                if(Vector3.Distance(t.transform.position, transform.position) < Vector3.Distance(currentTarget.transform.position, transform.position))
                {
                    currentTarget = t;
                }
            }
            yield return new WaitForSeconds(checkRate);
        }
        yield return null;
    }

    private IEnumerator PunchDetect(float checkRate = .1f)
    {
        while (enabled)
        {
            //transform.position + (transform.forward * 1.5f) + transform.up, 2.25f
            List<Collider> detected = Physics.OverlapSphere(transform.position + (transform.forward * 1.5f) + transform.up, 2.25f).Where(x => x.CompareTag("Player")).ToList();
            if (detected.Count > 0)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.05f));
                Punch();
            }
            yield return new WaitForSeconds(checkRate);
        }
        yield return null;
    }

    public void GoToArea()
    {
        SphereCollider areaRef = (SphereCollider) ((MiniGame_KingOfTheHill)MiniGame.singleton).stayArea;
        NavMeshHit hit;
        Vector3 randomDirection = Random.insideUnitSphere * areaRef.radius;
        randomDirection += areaRef.transform.position;
        NavMesh.SamplePosition(randomDirection, out hit, areaRef.radius, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
        reachedDestination = false;
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
            agent.speed = speed;
        }
    }

    protected override IEnumerator coStun(Vector3 force)
    {
        agent.enabled = false;
        yield return new WaitForSeconds(.1f);
        rB.AddForce(force, ForceMode.VelocityChange);
        yield return new WaitForSeconds(.25f);
        while (!IsGrounded())
        {
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(stunDuration);
        agent.enabled = true;
        isStunned = false;
        GoToArea();
        yield return null;
    }
}