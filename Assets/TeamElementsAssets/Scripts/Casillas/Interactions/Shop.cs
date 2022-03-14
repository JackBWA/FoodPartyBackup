using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public TextMeshProUGUI timer;

    [HideInInspector]
    public BoardEntity shopInteractor;

    public ShopItemsPanel itemsPanel;
    public SelectedItem selectedItemPanel;

    public ShopElementUI shopItemPrefab;

    public List<ShopElementUI> shopItems = new List<ShopElementUI>();

    private void Awake()
    {
        shopItems = new List<ShopElementUI>();
        selectedItemPanel.shop = this;
        CreateItems();
        LoadItems();
    }

    private void CreateItems()
    {
        foreach(RecipeElement rE in Resources.LoadAll<RecipeElement>("Recipes/RecipeElements"))
        {
            Debug.Log(rE.GetType());
            ShopElementUI sEUI = Instantiate(shopItemPrefab);
            sEUI.shop = this;
            sEUI.recipeElement = rE;
            switch (rE)
            {
                case Flavor flavor:
                    sEUI.amount = 1;
                    sEUI.maxAmount = 1;
                    break;

                case Ingredient ingredient:
                    sEUI.amount = 5;
                    sEUI.maxAmount = 5;
                    break;
            }
            sEUI.gameObject.transform.SetParent(itemsPanel.elementsContentPanel.transform);
            shopItems.Add(sEUI);
        }
    }

    public void LoadItems()
    {
        foreach (ShopElementUI sEUI in shopItems)
        {
            sEUI.itemName = sEUI.recipeElement.name;
            sEUI.itemSprite = sEUI.recipeElement.icon;
            sEUI.itemCost = sEUI.recipeElement.buyCost;
        }
        shopItems[0].GetComponent<Button>().Select();
        shopItems[0].SelectItem();
    }

    public void OpenShop(BoardEntity entity)
    {
        shopInteractor = entity;
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        shopInteractor.currentCoaster.EndInteract();
        shopInteractor = null;
        gameObject.SetActive(false);
    }
}
