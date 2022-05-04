using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCatcherController : MonoBehaviour
{

    public PlayerCharacter playerCharacter;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    protected Vector3 moveVector;
    protected float ySpeed;

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        TryGetComponent(out playerCharacter);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        
    }
    #endregion

    public virtual void Jump()
    {

    }

    protected virtual bool IsGrounded()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, 1 << LayerMask.NameToLayer("MapStatic"));
        return isGrounded;
    }

    public virtual void Initialize()
    {

    }

    protected virtual void OnEnable()
    {
        MiniGame.singleton.onMinigameFinish += StopMovement;
    }

    protected virtual void OnDisable()
    {
        MiniGame.singleton.onMinigameFinish -= StopMovement;
    }

    public void StopMovement()
    {
        enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        PointCollect points;
        if (!other.TryGetComponent(out points)) return;
        MiniGame.singleton.AddScore(GetComponent<PlayerCharacter>(), points.value);
        MiniGame.singleton.UpdateScores();
        Destroy(points.gameObject);
    }
}