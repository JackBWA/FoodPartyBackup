using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyPotCanvas : MonoBehaviour
{
    public Transform elementsParent;
    public GreedyPotUIElement prefab;

    public void AddElement(Sprite icon, int amount)
    {
        GreedyPotUIElement instance = Instantiate(prefab);
        instance.icon = icon;
        instance.amount = amount;
        instance.transform.SetParent(elementsParent);
    }
}
