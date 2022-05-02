using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TomatoRain : MonoBehaviour
{

    public Animator animController;

    public void Drop()
    {
        animController.SetBool("Drop", true);
    }
}
