using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardItem : ScriptableObject
{
    public new string name;

    [TextArea]
    public string description;

    public Sprite icon;

    public GameObject prefab;

    public abstract void Use(BoardEntity interactor);
}
