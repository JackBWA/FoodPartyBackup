using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCoaster : Coaster
{

    public Shop shopPrefab;

    private Shop shopInstance;

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
        Debug.Log("Shop interact!");
        shopInstance = Instantiate(shopPrefab);
        switch (interactor.GetComponent<PlayerCharacter>().characterType)
        {
            case PlayerCharacter.CharacterType.Player:
                shopInstance.OpenShop(interactor);
                break;

            case PlayerCharacter.CharacterType.AI:
                StartCoroutine(AutoBuy(shopInstance));
                EndInteract(interactor);
                break;
        }
    }

    public IEnumerator AutoBuy(Shop shop)
    {
        List<ShopElementUI> items = shop.shopItems;
        yield return null;
    }

    public override void EndInteract(BoardEntity interactor)
    {
        base.EndInteract(interactor);
        Debug.Log("Shop end interact!");
        Destroy(shopInstance.gameObject);
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
