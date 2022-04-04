using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCatcherPlayerController : FoodCatcherController
{

    MinigamePlayerControls inputActions;

    private Vector3 moveVector;
    private float ySpeed;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        inputActions = new MinigamePlayerControls();
        inputActions.FoodCatcher.Move.performed += ctx =>
        {
            //Debug.Log(ctx.valueType);
            float _ = ctx.ReadValue<float>();
            Debug.Log(_);
            moveVector = new Vector3(-_, moveVector.y, moveVector.z);
        };
        inputActions.FoodCatcher.Move.canceled += _ => moveVector = Vector2.zero;
        inputActions.FoodCatcher.Jump.performed += _ => Jump();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
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

    private bool IsGrounded()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, 1 << LayerMask.NameToLayer("Floor"));
        return isGrounded;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        inputActions.Disable();
    }
}
