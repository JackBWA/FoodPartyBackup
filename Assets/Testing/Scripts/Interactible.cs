using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    public GameObject interactor; // Cambiar gameObject por ?

    [Flags]
    public enum InteractionType
    {
        TriggerEnter,
        TriggerExit
    }

    public virtual void Interact(GameObject interactor)
    {

    }
}
