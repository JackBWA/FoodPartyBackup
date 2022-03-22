using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardEntityInventory : MonoBehaviour
{
    public ItemsCanvas itemsCanvasPrefab;

    public BoardEntity owner;

    public Dictionary<BoardItem_Base, int> items = new Dictionary<BoardItem_Base, int>();

    protected ItemsCanvas itemsCanvasInstance;

    public bool canUseItem
    {
        get
        {
            return _canUseItem;
        }
        set
        {
            _canUseItem = value;
        }
    }
    private bool _canUseItem;

    public bool isUsingItem
    {
        get
        {
            return _isUsingItem;
        }
        set
        {
            _isUsingItem = value;
        }
    }
    private bool _isUsingItem;

    protected virtual void Awake()
    {
        TryGetComponent(out owner);
        AddItem(Resources.LoadAll<BoardItem_Base>("BoardItems/Items")[0], 1);
    }

    #region Inventory Methods
    public void AddItem(BoardItem_Base item, int amount = 1)
    {
        if (!items.ContainsKey(item))
        {
            items.Add(item, amount);
        }
        else
        {
            items[item] += amount;
        }
    }

    public void UpdateItem(BoardItem_Base item, int amount)
    {
        if (!items.ContainsKey(item))
        {
            return;
        }
        else
        {
            items[item] = amount;
        }
    }

    public bool HasItem(BoardItem_Base item)
    {
        return items.ContainsKey(item);
    }

    public void UseItem(BoardItem_Base item)
    {
        if (canUseItem && !isUsingItem)
        {
            BoardItem_Base itemInstance = Instantiate(item, transform.position + transform.forward + (transform.up * 2f), Quaternion.identity);
            itemInstance.owner = owner;
            ConsumeItem(item);
            StartUsingItem(itemInstance);
        }
    }

    private void ConsumeItem(BoardItem_Base item)
    {
        if (items.ContainsKey(item))
        {
            items[item]--;
            if (items[item] <= 0) items.Remove(item);
            canUseItem = false;
        }
        else
        {
            Debug.LogWarning("This inventory doesn't have this item.");
        }
    }
    #endregion

    #region Events
    public event Action onStartUsingItem;
    public void StartUsingItem(BoardItem_Base item)
    {
        canUseItem = false;
        isUsingItem = true;
        owner.SetCanThrowDice(false);
        onStartUsingItem?.Invoke();
    }

    public event Action onEndUsingItem;
    public void EndUsingItem(BoardItem_Base item)
    {
        isUsingItem = false;
        canUseItem = false;
        owner.SetCanThrowDice(true);
        onEndUsingItem?.Invoke();
    }

    public event Action onCancelUsingItem;
    public void CancelUsingItem(BoardItem_Base item)
    {
        isUsingItem = false;
        canUseItem = true;
        owner.SetCanThrowDice(true);
        onCancelUsingItem?.Invoke();
    }
    #endregion
}
