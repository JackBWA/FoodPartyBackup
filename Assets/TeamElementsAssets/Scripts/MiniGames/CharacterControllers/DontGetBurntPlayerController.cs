using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGetBurntPlayerController : DontGetBurntController
{

    MinigamePlayerControls inputActions;

    private Vector2 moveVector;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        inputActions = new MinigamePlayerControls();
        inputActions.DontGetBurnt.Move.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        inputActions.DontGetBurnt.Move.canceled += _ => moveVector = Vector2.zero;
        inputActions.DontGetBurnt.Jump.performed += _ => Jump();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(rB != null) rB.velocity = new Vector3(moveVector.x * speed, rB.velocity.y, moveVector.y * speed);
    }
    #endregion

    public void Jump()
    {
        Debug.Log(CanJump());
        if (rB != null && CanJump()) rB.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private bool CanJump()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, .1f);
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