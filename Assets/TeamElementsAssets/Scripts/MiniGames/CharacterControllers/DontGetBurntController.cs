using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGetBurntController : MonoBehaviour
{

    public Rigidbody rB;
    public CharacterController controller;
    
    public float speed = 5f;
    public float jumpForce = 7.5f;

    #region Awake/Start/Update
    protected virtual void Awake()
    {

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
        MiniGame.singleton.onMinigameFinish += StopRb;
    }

    protected virtual void OnDisable()
    {
        MiniGame.singleton.onMinigameFinish -= StopRb;
    }

    public void StopRb()
    {
        rB.velocity = Vector3.zero;
    }

    public void Initialize()
    {
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
    }
}