using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeElement : ScriptableObject
{
    public new string name;
    public int cost;

    public Sprite icon;
    public GameObject modelPrefab;
}
