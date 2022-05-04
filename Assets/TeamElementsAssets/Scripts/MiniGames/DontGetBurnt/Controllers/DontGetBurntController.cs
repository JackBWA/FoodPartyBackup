using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGetBurntController : MonoBehaviour
{

    public PlayerCharacter playerCharacter;
    
    public float speed = 5f;
    public float jumpForce = 7.5f;
    public float rotationSpeed = 10f;

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        TryGetComponent(out playerCharacter);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    #endregion

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

    public virtual void Initialize()
    {
        /*
        if(!gameObject.TryGetComponent(out rB))
        {
            rB = gameObject.AddComponent<Rigidbody>();
            rB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if(!gameObject.TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
            controller.center = Vector3.up;
        }
        */
    }
}