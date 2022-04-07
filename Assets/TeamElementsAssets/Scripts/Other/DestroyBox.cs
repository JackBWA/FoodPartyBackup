using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{

    public LayerMask layers;

    private void OnTriggerEnter(Collider other)
    {
        if(layers == (layers | 1 << other.gameObject.layer))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (layers == (layers | 1 << collision.gameObject.layer))
        {
            Destroy(collision.gameObject);
        }
    }
}