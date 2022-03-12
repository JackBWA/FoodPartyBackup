using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Cooking Party/Recipe/Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name;

    public Sprite icon;
    public GameObject modelPrefab;
}