using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flavor", menuName = "Cooking Party/Recipe/Flavor")]
public class Flavor : ScriptableObject
{
    public new string name;

    public Texture2D icon;
    public GameObject modelPrefab;
}