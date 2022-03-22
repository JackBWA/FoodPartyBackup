using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoaster : Coaster
{

    public static List<BoardItem_Base> obtainableItems = new List<BoardItem_Base>();

    protected override void Awake()
    {
        base.Awake();
        /* // Moving to interact.
        foreach(BoardItem_Base item in Resources.LoadAll<BoardItem_Base>("BoardItems/Items")){
            obtainableItems.Add(item);
        }
        */
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(BoardEntity interactor)
    {
        base.Interact(interactor);
        BoardItem_Base randomItem;
        BoardItem_Base[] itemList = Resources.LoadAll<BoardItem_Base>("BoardItems/Items");
        randomItem = itemList[Random.Range(0, itemList.Length)];
        /*
         * 
         * Give some feedback.
         * 
         */
        interactor.inventory.AddItem(randomItem);
        Debug.Log("Bonus interact!");
    }

    public override void EndInteract(BoardEntity interactor)
    {
        base.EndInteract(interactor);
    }

    public override void playerEnter(BoardEntity entity, Vector3 position)
    {
        base.playerEnter(entity, position);
    }

    public override void playerStop(BoardEntity entity)
    {
        base.playerStop(entity);
    }

    public override void playerLeave(BoardEntity entity)
    {
        base.playerLeave(entity);
    }
}