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

    Rigidbody rB;

    private void Awake()
    {
        TryGetComponent(out rB);
        if (rB != null) rB.drag = UnityEngine.Random.Range(1f, 2f);
        if (random) value = UnityEngine.Random.Range(minValue, maxValue);
    }

    private void OnDestroy()
    {
        //Cagate
        ((MiniGame_FoodCatcher)MiniGame.singleton).Remove(this);
    }
}