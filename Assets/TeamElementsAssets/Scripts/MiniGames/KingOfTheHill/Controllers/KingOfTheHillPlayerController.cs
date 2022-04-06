using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillPlayerController : KingOfTheHillController
{

    MinigamePlayerControls inputActions;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        inputActions = new MinigamePlayerControls();
        inputActions.KingOfTheHill.Move.performed += ctx =>
        {
            if (!canMove) return;
            Vector2 _ = ctx.ReadValue<Vector2>();
            moveVector = new Vector3(_.x, moveVector.y, _.y);
        };
        inputActions.KingOfTheHill.Move.canceled += _ => moveVector = Vector2.zero;
        inputActions.KingOfTheHill.Jump.performed += _ => Jump();
        inputActions.KingOfTheHill.Punch.performed += _ =>
        {
            Debug.Log("Punch input performed.");
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