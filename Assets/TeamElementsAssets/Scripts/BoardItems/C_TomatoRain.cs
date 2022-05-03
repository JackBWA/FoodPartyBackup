using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TomatoRain : MonoBehaviour
{

    public BoardEntity owner;

    public float damage;

    public Animator animController;

    public SphereCollider sC;

    public Rigidbody rB;

    private void Awake()
    {
        TryGetComponent(out rB);
        TryGetComponent(out sC);
        sC.enabled = false;
    }

    public void Drop()
    {
        if (rB != null) rB.useGravity = true;
        if (sC != null) sC.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BoardEntity entity;
        if(other.TryGetComponent(out entity) /*&& entity.CompareTag("Player") */&& entity != owner)
        {
            other.GetComponent<BoardEntity>().health -= damage;
        }
    }
}