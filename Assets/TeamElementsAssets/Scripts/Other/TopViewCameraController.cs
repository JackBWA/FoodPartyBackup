using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCameraController : MonoBehaviour
{

    public float movementSpeed = 15f;

    BoardPlayerControls inputActions;

    bool moving;

    float speedMultiplier = 1f;

    Vector2 movementVector;

    Vector3 initialPosition;

    private void Awake()
    {
        inputActions = new BoardPlayerControls();
        inputActions.Map.Move.performed += ctx => Move(ctx.ReadValue<Vector2>(), true);
        inputActions.Map.Move.canceled += ctx => Move(ctx.ReadValue<Vector2>(), false);
        inputActions.Map.Accelerate.performed += _ => Accelerate(true);
        inputActions.Map.Accelerate.canceled += _ => Accelerate(false);

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

    public void Accelerate(bool value)
    {
        if (value)
        {
            speedMultiplier = 3f;
        } else
        {
            speedMultiplier = 1f;
        }
    }

    private void Update()
    {
        if (moving)
        {
            transform.position += new Vector3(movementVector.x, 0f, movementVector.y) * ((movementSpeed * speedMultiplier) * Time.deltaTime);
        }
    }
}