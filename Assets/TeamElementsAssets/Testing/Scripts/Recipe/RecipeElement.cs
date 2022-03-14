using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeElement : ScriptableObject
{
    public new string name;
    public int buyCost;
    public int sellCost;

    public Sprite icon;
    public GameObject modelPrefab;
}
