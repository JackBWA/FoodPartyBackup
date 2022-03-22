using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RottenTomato : BoardItem<RottenTomato>
{
    public override void Use(BoardEntity interactor)
    {
        prefabInstance = Instantiate(prefab);
    }
}
