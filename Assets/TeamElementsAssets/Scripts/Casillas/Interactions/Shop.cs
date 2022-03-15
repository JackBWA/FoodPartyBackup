using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public float shopDuration;

    private bool isOpen;

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
        gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        float counter = 0f;
        while (counter < shopDuration && isOpen)
        {
            timer.text = $"{(int)(shopDuration - counter)}";
            counter += Time.deltaTime;
            yield return null;
        }
        shopInteractor.currentCoaster.EndInteract(shopInteractor);
    }

    private void CreateItems()
    {
        foreach(RecipeElement rE in Resources.LoadAll<RecipeElement>("Recipes/RecipeElements"))
        {
            //Debug.Log(rE.GetType());
            ShopElementUI sEUI = Instantiate(shopItemPrefab);
            sEUI.shop = this;
            sEUI.recipeElement = rE;
            switch (rE)
            {
                case Flavor flavor:
                    sEUI.amount = 1;
                    //sEUI.maxAmount = 1;
                    break;

                case Ingredient ingredient:
                    sEUI.amount = 5;
                    //sEUI.maxAmount = 5;
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
        isOpen = true;
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        //shopInteractor.currentCoaster.EndInteract(); // Moved to coroutine
        isOpen = false;
        //gameObject.SetActive(false);
    }

    public void BuyItem()
    {
        if(selectedItemPanel.itemAmount > selectedItemPanel.selected.amount || selectedItemPanel.buyCost > shopInteractor.coins)
        {
            Debug.LogWarning("Couldn't proceed with the purchase because interactor doesn't have enough coins.");
            return;
        }
        // Update coins
        shopInteractor.coins -= selectedItemPanel.buyCost;
        if (!GameBoardManager.singleton.recipeStates[shopInteractor].currentElements.ContainsKey(selectedItemPanel.selected.recipeElement)) GameBoardManager.singleton.recipeStates[shopInteractor].currentElements.Add(selectedItemPanel.selected.recipeElement, selectedItemPanel.itemAmount);
        else
        {
            int newAmount = GameBoardManager.singleton.recipeStates[shopInteractor].currentElements[selectedItemPanel.selected.recipeElement] + selectedItemPanel.itemAmount;
            // Add X amount of recipe elements
            GameBoardManager.singleton.recipeStates[shopInteractor].SetCurrentElement(selectedItemPanel.selected.recipeElement, newAmount);
        }

        // Update shop element UI amount value.
        selectedItemPanel.selected.amount -= selectedItemPanel.itemAmount;
        selectedItemPanel.UpdatePanel();

        // Update recipe display
        RecipeManagerUI.singleton.UpdateDisplay(shopInteractor);
    }

    public void SellItem()
    {
        if (GameBoardManager.singleton.recipeStates[shopInteractor].currentElements.ContainsKey(selectedItemPanel.selected.recipeElement) && selectedItemPanel.itemAmount > GameBoardManager.singleton.recipeStates[shopInteractor].currentElements[selectedItemPanel.selected.recipeElement])
        {
            Debug.LogWarning("Couldn't proceed with the sale because interactor doesn't have enough elements.");
            return;
        }

        // Update coins
        shopInteractor.coins += selectedItemPanel.sellCost;
        int newAmount = GameBoardManager.singleton.recipeStates[shopInteractor].currentElements[selectedItemPanel.selected.recipeElement] - selectedItemPanel.itemAmount;
        
        // Remove X amount of recipe elements
        GameBoardManager.singleton.recipeStates[shopInteractor].SetCurrentElement(selectedItemPanel.selected.recipeElement, newAmount);

        // Update shop element UI amount value.
        selectedItemPanel.selected.amount += selectedItemPanel.itemAmount;
        selectedItemPanel.UpdatePanel();

        // Update recipe display
        RecipeManagerUI.singleton.UpdateDisplay(shopInteractor);
    }
}