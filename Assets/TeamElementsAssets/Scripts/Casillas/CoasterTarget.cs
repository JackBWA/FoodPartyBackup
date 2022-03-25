using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoasterTarget : MonoBehaviour
{
    public CoasterTargetSelector selector;

    public Coaster target;

    private void OnMouseDown()
    {
        Debug.Log("Mouse down.");
        selector.SetResult(target);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse enter.");
        transform.localScale *= 1.2f;
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse exit.");
        transform.localScale /= 1.2f;
    }
}
