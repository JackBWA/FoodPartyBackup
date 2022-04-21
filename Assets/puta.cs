using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puta : MonoBehaviour
{

    public Animator animator;

    PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();
        inputActions.Pause.Toggle.performed += _ => puto();
    }

    public void puto()
    {
        animator.Play("Jump");
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
