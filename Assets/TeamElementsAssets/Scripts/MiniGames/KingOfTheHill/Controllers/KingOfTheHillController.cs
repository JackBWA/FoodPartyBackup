using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KingOfTheHillController : MonoBehaviour
{

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    protected Vector3 moveVector;
    protected float ySpeed;

    public float punchForce = 20f;
    public float punchCooldown = .75f;

    protected Rigidbody rB;

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
    public bool isStunned
    {
        get
        {
            return _isStunned;
        }
        set
        {
            _isStunned = value;
            if (isStunned)
            {
                moveVector = Vector3.zero;
            }
        }
    }
    private bool _isStunned;

    /*
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
    */

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        canPunch = true;
        isStunned = false;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    #endregion

    public virtual void Jump()
    {

    }

    protected bool IsGrounded()
    {
        bool isGrounded = Physics.CheckSphere(transform.position, 0.2f, 1 << LayerMask.NameToLayer("MapStatic"));
        return isGrounded;
    }

    public virtual void Initialize()
    {
        if (!gameObject.TryGetComponent(out rB))
        {
            rB = gameObject.AddComponent<Rigidbody>();
            rB.constraints = RigidbodyConstraints.FreezeRotation;
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
        Debug.Log("He punchiao");
        timer = 0f;

        // Do stuff.
        // transform.position + transform.forward + transform.up; // OVERLAP AREA
        foreach(Collider overlapped in Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1.5f).Where(obj => obj.gameObject.CompareTag("Player")).ToArray()){
            if(overlapped.gameObject != gameObject)
            {
                KingOfTheHillController _controller = overlapped.gameObject.GetComponent<KingOfTheHillController>();
                _controller.Stun((_controller.transform.position - transform.position).normalized * punchForce);
            }
        }

        StartCoroutine(PunchTimer());
    }

    public void Stun(Vector3 force)
    {
        StartCoroutine(coStun(force));
        moveVector = force;
        ySpeed = punchForce / 2f;
    }

    protected virtual IEnumerator coStun(Vector3 force)
    {
        yield return null;
    }
}