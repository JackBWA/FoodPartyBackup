using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapCoaster : Coaster
{

    public float healthAmount = 10f;
    public int coinsAmount = 15;
    public int itemsAmount = 1;

    public enum TrapType
    {
        LoseHealth,
        LoseCoins,
        LoseItem
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(BoardEntity interactor)
    {
        base.Interact(interactor);
        TrapType trapType = (TrapType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrapType)).Length);
        switch (trapType)
        {
            case TrapType.LoseHealth:
                interactor.health = Mathf.Clamp(interactor.health - healthAmount, 0, interactor.baseHealth);
                break;

            case TrapType.LoseCoins:
                interactor.coins = Mathf.Clamp(interactor.coins - coinsAmount, 0, int.MaxValue); ;
                break;

            case TrapType.LoseItem:
                if (interactor.inventory.items.Count <= 0) break;
                for (int i = 0; i < itemsAmount; i++)
                {
                    BoardItem_Base item = interactor.inventory.items.ElementAt(UnityEngine.Random.Range(0, interactor.inventory.items.Count)).Key;
                    interactor.inventory.UpdateItem(item, interactor.inventory.items[item] - 1);
                    if (interactor.inventory.items.Count <= 0) break;
                }
                break;
        }
        Debug.Log("Trap interact!");
        EndInteract(interactor);
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