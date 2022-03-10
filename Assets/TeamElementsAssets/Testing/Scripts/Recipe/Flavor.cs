using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Flavor", menuName = "Cooking Party/Recipe/Flavor")]
public class Flavor : ScriptableObject
{
    public new string name;

    public Image icon;
    public GameObject modelPrefab;
}