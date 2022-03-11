using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCameraController : MonoBehaviour
{

    public float movementSpeed = 15f;

    BoardPlayerControls inputActions;

    bool moving;

    Vector2 movementVector;

    Vector3 initialPosition;

    private void Awake()
    {
        inputActions = new BoardPlayerControls();
        inputActions.Map.Move.performed += ctx => Move(ctx.ReadValue<Vector2>(), true);
        inputActions.Map.Move.canceled += ctx => Move(ctx.ReadValue<Vector2>(), false);

        initialPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        transform.localPosition = initialPosition;
        inputActions.Disable();
    }

    public void Move(Vector2 input, bool moving)
    {
        this.moving = moving;
        movementVector = input;
    }

    private void Update()
    {
        if (moving)
        {
            transform.position += new Vector3(movementVector.x, 0f, movementVector.y) * (movementSpeed * Time.deltaTime);
        }
    }
}