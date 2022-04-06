using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceToTheTopController : MonoBehaviour
{

    public CharacterController controller;
    public Rigidbody rB;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    public Vector3 moveVector;
    public float ySpeed;

    public bool canMove
    {
        get
        {
            return knockbackDuration <= 0f;
        }
        set
        {
            if (value)
            {
                knockbackDuration = 0f;
            }
        }
    }
    private float knockbackDuration;
    private Coroutine knockbackCo;

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        knockbackDuration = 0f;
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
            if (!canMove)
            {
                ySpeed += Physics.gravity.y * Time.deltaTime; ;
                controller.Move(new Vector3(0f, ySpeed, 0f) * Time.deltaTime);
                return;
            }
            float magnitude = Mathf.Clamp01(moveVector.magnitude) * speed;
            moveVector.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

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

        if(!gameObject.TryGetComponent(out rB))
        {
            rB = gameObject.AddComponent<Rigidbody>();
            rB.useGravity = false;
            rB.constraints |= RigidbodyConstraints.FreezeRotation;
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

    public void KnockbackEffect(float duration)
    {
        knockbackDuration = duration;
        if (knockbackCo == null) knockbackCo = StartCoroutine(Knockback());
    }

    public void StopKnockbackEffect()
    {
        StopCoroutine(knockbackCo);
        knockbackCo = null;
    }

    private IEnumerator Knockback()
    {
        while (knockbackDuration >= 0f)
        {
            knockbackDuration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StopKnockbackEffect();
        yield return null;
    }
}
