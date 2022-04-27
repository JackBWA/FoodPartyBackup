using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillPlayerController : KingOfTheHillController
{

    public CharacterController controller;

    MinigamePlayerControls inputActions;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        inputActions = new MinigamePlayerControls();
        inputActions.KingOfTheHill.Move.performed += ctx =>
        {
            if (isStunned) return;
            Vector2 _ = ctx.ReadValue<Vector2>();
            moveVector = new Vector3(_.x, moveVector.y, _.y);
        };
        inputActions.KingOfTheHill.Move.canceled += _ => moveVector = Vector2.zero;
        inputActions.KingOfTheHill.Jump.performed += _ => Jump();
        inputActions.KingOfTheHill.Punch.performed += _ =>
        {
            if (canPunch) Punch();
        };
        inputActions.KingOfTheHill.MouseLook.performed += ctx =>
        {
            RaycastHit hit;
            Vector2 value = ctx.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(value);
            if(Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                hitPoint.y = transform.position.y;
                transform.LookAt(hitPoint);
            }
        };
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

    public override void Jump()
    {
        base.Jump();
        if (controller != null && IsGrounded())
        {
            ySpeed = jumpForce;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        if (!gameObject.TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
            controller.center = Vector3.up;
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

    protected override IEnumerator coStun(Vector3 force)
    {
        return base.coStun(force);
    }
}