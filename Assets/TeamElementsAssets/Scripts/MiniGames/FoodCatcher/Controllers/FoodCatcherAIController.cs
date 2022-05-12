using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FoodCatcherAIController : FoodCatcherController
{

    private float checkRate = 1f;
    private float checkRadius = 25f;
    private float distanceToJump = 2f;

    float timer = 0f;

    public NavMeshAgent agent;

    Coroutine _moveCo;

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
        timer += Time.deltaTime;
        if(timer >= checkRate)
        {
            MoveToRandomNearbyCollectable();
        }
        if (playerCharacter != null) playerCharacter.animManager.ator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(agent != null)
        {
            moveVector = agent.velocity/*.normalized*/;
            float magnitude = Mathf.Clamp01(moveVector.magnitude) * speed;
            moveVector.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (ySpeed < 0f && IsGrounded()) ySpeed = Vector3.kEpsilon;

            Vector3 velocity = moveVector * magnitude;
            velocity.y = ySpeed;

            if (velocity.magnitude > Vector3.kEpsilon) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), rotationSpeed * Time.deltaTime);
        }
    }
    #endregion

    public override void Initialize()
    {
        base.Initialize();
        if (!gameObject.TryGetComponent(out agent))
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.speed = speed;
            agent.acceleration = 100f;
        }
    }

    public void TeleportTo(Vector3 position)
    {
        if (agent != null) agent.enabled = false;
        agent.Warp(position + new Vector3(0f, transform.localScale.y, 0f));
        if (agent != null) agent.enabled = true;
    }
    
    /*
    private void MoveToRandomNearbyCollectable()
    {
        //timer = 0f;
        //if (_moveCo != null) return;
        //List<Collider> pointCollectables = Physics.OverlapSphere(transform.position, checkRadius, LayerMask.GetMask("Collectable"))/*.Where(aux => aux.gameObject.GetComponent<PointCollect>()).ToList();

        PointCollect randomCollectable = pointCollectables[UnityEngine.Random.Range(0, pointCollectables.Count)].GetComponent<PointCollect>();
        if (randomCollectable.value <= 0f)
        {
            float random = UnityEngine.Random.Range(0, 100f);
            if (random < 50f)
            {
                _moveCo = StartCoroutine(MoveCo(randomCollectable));
                return;
            }
            Debug.Log("OTRO");
            pointCollectables.Remove(randomCollectable.GetComponent<Collider>());
            randomCollectable = pointCollectables[UnityEngine.Random.Range(0, pointCollectables.Count)].GetComponent<PointCollect>();
        }
        _moveCo = StartCoroutine(MoveCo(randomCollectable));
    }
    */

    private void MoveToRandomNearbyCollectable()
    {
        timer = 0f;
        if (_moveCo != null) return;
        List<Collider> pointCollectables = Physics.OverlapSphere(transform.position, checkRadius, LayerMask.GetMask("Collectable"))/*.Where(aux => aux.gameObject.GetComponent<PointCollect>())*/.ToList();
        if(pointCollectables.Count > 0) _moveCo = StartCoroutine(MoveCo(pointCollectables[UnityEngine.Random.Range(0, pointCollectables.Count)].GetComponent<PointCollect>()));
    }

    private IEnumerator MoveCo(PointCollect pC, float checkRate = 0.25f)
    {
        Vector3 destination = pC.transform.position;
        destination.y = transform.position.y;
        agent.SetDestination(destination);
        while (pC != null && Vector3.Distance(transform.position, pC.transform.position) >= distanceToJump)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(.1f, .25f));
        }
        StopMoveCo();
        yield return null;
    }

    private void StopMoveCo()
    {
        if (_moveCo != null) StopCoroutine(_moveCo);
        _moveCo = null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (!other.gameObject.GetComponent<PointCollect>()) return;
        StopMoveCo();
        MoveToRandomNearbyCollectable();
    }
}