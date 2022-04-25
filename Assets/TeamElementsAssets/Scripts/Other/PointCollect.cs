using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollect : MonoBehaviour
{
    public int value;
    public bool random;
    [SerializeField]
    private int minValue;
    [SerializeField]
    private int maxValue;

    private void Awake()
    {
        if (random) value = UnityEngine.Random.Range(minValue, maxValue);
    }

    private void OnDestroy()
    {
        //Cagate
        ((MiniGame_FoodCatcher)MiniGame.singleton).Remove(this);
    }
}