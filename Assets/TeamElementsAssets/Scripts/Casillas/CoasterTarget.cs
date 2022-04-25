using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoasterTarget : MonoBehaviour, IPointerDownHandler
{
    public CoasterTargetSelector selector;

    public Coaster target;

    public void OnPointerDown(PointerEventData eventData)
    {
        selector.SetResult(target);
    }
}