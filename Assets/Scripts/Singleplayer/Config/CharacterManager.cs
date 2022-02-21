using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private void Awake()
    {
        Object[] characters = Resources.LoadAll("Characters/");
        foreach(Object o in characters)
        {
            Instantiate(o);
        }
    }
}
