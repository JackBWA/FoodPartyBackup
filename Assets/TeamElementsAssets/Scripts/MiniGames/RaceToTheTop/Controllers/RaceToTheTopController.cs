using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceToTheTopController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    protected Vector3 moveVector;
    private float ySpeed;

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

            if (gameObject.name.Equals("Player2(Clone)"))
            {
                Debug.Log($"{ySpeed < 0f} && {IsGrounded()}");
            }
            if (ySpeed < 0f && IsGrounded()) ySpeed = Vector3.kEpsilon;

            Vector3 velocity = moveVector * magnitude;
            velocity.y = ySpeed;

            controller.Move(velocity * Time.deltaTime);
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

    private bool IsGrounded()
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
}
