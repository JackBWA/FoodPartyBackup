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
}
