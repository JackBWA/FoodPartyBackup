using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCatcherController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    protected Vector3 moveVector;
    protected float ySpeed;

    #region Awake/Start/Update
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        if (controller != null)
        {
            float magnitude = Mathf.Clamp01(moveVector.magnitude) * speed;
            moveVector.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (ySpeed < 0f && IsGrounded()) ySpeed = Vector3.kEpsilon;

            Vector3 velocity = moveVector * magnitude;
            velocity.y = ySpeed;

            //Debug.Log(velocity);

            controller.Move(velocity * Time.deltaTime);

            //Debug.Log($"{velocity.magnitude} | {Vector3.kEpsilon}");

            if (velocity.magnitude > Vector3.kEpsilon) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), rotationSpeed * Time.deltaTime);
        }
    }
    #endregion

    public void Jump()
    {
        if (controller != null && IsGrounded())
        {
            ySpeed = jumpForce;
        }
    }

    protected bool IsGrounded()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, 1 << LayerMask.NameToLayer("MapStatic"));
        return isGrounded;
    }

    public void Initialize()
    {
        if (!gameObject.TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
            controller.center = Vector3.up;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        PointCollect points;
        if (!other.TryGetComponent(out points)) return;
        MiniGame.singleton.AddScore(GetComponent<PlayerCharacter>(), points.value);
        MiniGame.singleton.UpdateScores();
        Destroy(points.gameObject);
    }
}
