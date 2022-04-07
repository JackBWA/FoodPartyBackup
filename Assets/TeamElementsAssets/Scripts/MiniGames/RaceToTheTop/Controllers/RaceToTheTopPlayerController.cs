using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceToTheTopPlayerController : RaceToTheTopController
{

    MinigamePlayerControls inputActions;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        inputActions = new MinigamePlayerControls();
        inputActions.RaceToTheTop.Move.performed += ctx =>
        {
            Vector2 _ = ctx.ReadValue<Vector2>();
            moveVector = new Vector3(_.x, moveVector.y, _.y);
        };
        inputActions.RaceToTheTop.Move.canceled += _ => moveVector = Vector2.zero;
        inputActions.RaceToTheTop.Jump.performed += _ => Jump();
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
    }
    #endregion

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
