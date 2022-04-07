using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingOfTheHillController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    protected Vector3 moveVector;
    private float ySpeed;

    public float punchForce = 20f;
    public float punchCooldown = .75f;
    public bool canPunch
    {
        get
        {
            return timer >= punchCooldown;
        }
        set
        {
            if (value)
            {
                timer = punchCooldown;
            }
        }
    }
    private float timer;

    private Coroutine scoreCo;

    public float stunDuration = 2f;
    protected bool canMove
    {
        get
        {
            return _canMove;
        }
        set
        {
            _canMove = value;
        }
    }
    private bool _canMove;

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        canPunch = true;
        canMove = true;
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

            if(gameObject.name.Equals("Player2(Clone)"))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(((MiniGame_KingOfTheHill)MiniGame.singleton).stayArea)) scoreCo = StartCoroutine(IncreasePoints());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(((MiniGame_KingOfTheHill)MiniGame.singleton).stayArea)) StopCoroutine(scoreCo);
    }

    public IEnumerator PunchTimer()
    {
        while(timer < punchCooldown)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator IncreasePoints()
    {
        PlayerCharacter pC = GetComponent<PlayerCharacter>();
        while (enabled)
        {
            MiniGame.singleton.AddScore(pC);
            MiniGame.singleton.UpdateScores();
            yield return new WaitForSeconds(.125f);
        }
        yield return null;
    }

    public void Punch()
    {
        timer = 0f;

        // Do stuff.
        // transform.position + transform.forward + transform.up; // OVERLAP AREA
        foreach(Collider overlapped in Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1.5f).Where(obj => obj.gameObject.CompareTag("Player")).ToArray()){
            if(overlapped.gameObject != gameObject)
            {
                KingOfTheHillController _controller = overlapped.gameObject.GetComponent<KingOfTheHillController>();
                _controller.Stun(stunDuration, (_controller.transform.position - transform.position).normalized * punchForce);
            }
        }

        StartCoroutine(PunchTimer());
    }

    public void Stun(float duration, Vector3 force)
    {
        StartCoroutine(coStun(duration));
        moveVector = force;
        ySpeed = punchForce / 2f;
    }

    private IEnumerator coStun(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
        moveVector = Vector3.zero;
        yield return null;
    }
}