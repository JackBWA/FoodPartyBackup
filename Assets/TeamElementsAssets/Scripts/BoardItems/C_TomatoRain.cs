using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TomatoRain : MonoBehaviour
{

    public BoardEntity owner;

    public float damage;

    public Animator animController;

    public Rigidbody rB;

    private void Awake()
    {
        TryGetComponent(out rB);
    }

    public void Drop()
    {
        rB.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<BoardEntity>() != owner)
        {
            other.GetComponent<BoardEntity>().health -= damage;
        }
    }
}
