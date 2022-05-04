using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCatcherPlayerController : FoodCatcherController
{

    MinigamePlayerControls inputActions;

    public CharacterController controller;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        inputActions = new MinigamePlayerControls();
        inputActions.FoodCatcher.Move.performed += ctx =>
        {
            //Debug.Log(ctx.valueType);
            float _ = ctx.ReadValue<float>();
            //Debug.Log(_);
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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (controller != null)
        {
            float magnitude = Mathf.Clamp01(moveVector.magnitude) * speed;
            moveVector.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (ySpeed < 0f && IsGrounded())
            {
                ySpeed = Vector3.kEpsilon;
                playerCharacter.animManager.ator.SetBool("InAir", false);
            }

            Vector3 velocity = moveVector * magnitude;
            velocity.y = ySpeed;

            //Debug.Log(velocity);

            if (playerCharacter != null) playerCharacter.animManager.ator.SetFloat("Speed", moveVector.magnitude);

            controller.Move(velocity * Time.deltaTime);

            //Debug.Log($"{velocity.magnitude} | {Vector3.kEpsilon}");

            if (velocity.magnitude > Vector3.kEpsilon) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVector), rotationSpeed * Time.deltaTime);
        }
    }
    #endregion

    public override void Initialize()
    {
        base.Initialize();
        if (!gameObject.TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
            controller.center = Vector3.up;
        }
    }

    public override void Jump()
    {
        base.Jump();
        if (controller != null && IsGrounded())
        {
            ySpeed = jumpForce;
            if (playerCharacter != null) playerCharacter.animManager.ator.SetBool("InAir", true);
        }
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
