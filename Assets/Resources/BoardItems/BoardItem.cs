using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Board Item", menuName = "TeamElements/Items/New Item")]
public abstract class BoardItem<T> : ScriptableObject
{
    public new string name;

    [TextArea]
    public string description;

    public Sprite icon;

    public T prefab;

    protected T prefabInstance;

    public virtual void Use(BoardEntity interactor)
    {
        //prefabInstance = Instantiate<GameObject>(prefab) as T;
    }
}
