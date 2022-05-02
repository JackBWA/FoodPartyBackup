using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                shopInstance.shopInteractor = interactor;
                StartCoroutine(AutoBuy(shopInstance));
                break;
        }
    }

    public IEnumerator AutoBuy(Shop shop)
    {
        //Debug.Log("Auto buying.");
        Recipe recipe = GameBoardManager.singleton.recipeStates[shop.shopInteractor];

        //Debug.Log("Recipe at start:\n" + recipe.ToString());

        for (int i = 0; i < recipe.requiredElements.Count; i++)
        {
            //Debug.Log($"Loop {i}. Required element: {recipe.requiredElements.ElementAt(i).Key.name}");
            int j = 0;
            ShopElementUI sEui = null;
            while(j < shop.shopItems.Count)
            {
                //Debug.Log($"Checking if (j){shop.shopItems[j].recipeElement.name} == (i){recipe.requiredElements.ElementAt(i).Key.name}");
                if (recipe.requiredElements.ElementAt(i).Key == shop.shopItems[j].recipeElement)
                {
                    //Debug.Log("Result was true.");
                    sEui = shop.shopItems[j];
                    j = shop.shopItems.Count;
                }
                else
                {
                    //Debug.Log("Result was false.");
                    j++;
                }
            }

            //Debug.Log($"Was it found? {sEui != null}.");
            if (sEui == null) continue;
            //Debug.Log("Should print this if it wasn't null. (Output == true)");

            int buyableAmount = Mathf.Clamp(shop.shopInteractor.coins / (sEui.amount * sEui.recipeElement.buyCost), 0, sEui.amount);
            int requiredAmount = Mathf.Clamp(recipe.requiredElements[sEui.recipeElement] - recipe.currentElements[sEui.recipeElement], 0, recipe.requiredElements[sEui.recipeElement]);

            int resultAmount = buyableAmount - (buyableAmount - requiredAmount); ;
            if(requiredAmount >= buyableAmount)
            {
                resultAmount = buyableAmount;
            }

            //Debug.Log($"Buyable amount equals to: {buyableAmount}. Required amount equals to: {requiredAmount}. Result amount equals to: {resultAmount}.");

            //Debug.Log($"Buying {resultAmount} {sEui.recipeElement.name}(s) for {resultAmount * sEui.recipeElement.buyCost}$.");

            recipe.SetCurrentElement(sEui.recipeElement, recipe.currentElements[sEui.recipeElement] + resultAmount);
            shop.shopInteractor.coins -= resultAmount * sEui.recipeElement.buyCost;

            // RecipeManagerUI.singleton.UpdateDisplay(shop.shopInteractor); // Ya no ???

            //Debug.Log("=====Checking next required=====");
            yield return new WaitForSeconds(.25f);
        }
        //Debug.Log("!!!Checked all!!!");

        //Debug.Log("Recipe at end:\n" + recipe.ToString());

        yield return new WaitForSeconds(2f);
        EndInteract(shop.shopInteractor);
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
