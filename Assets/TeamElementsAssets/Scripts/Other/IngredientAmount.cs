using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientAmount
{
    [SerializeField]
    public Ingredient ingredient;

    [SerializeField]
    public bool randomAmount;

    [SerializeField]
    public int amount;

    [SerializeField]
    private int minAmount;

    [SerializeField]
    private int maxAmount;

    public int GetRandomAmount()
    {
        return Random.Range(minAmount, maxAmount + 1);
    }
}
