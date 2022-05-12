using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public int minCoins;
    public int maxCoins;
    public int minIngredients;
    public int maxIngredients;
    public int minItems;
    public int maxItems;

    public int coinsAmount
    {
        get
        {
            return UnityEngine.Random.Range(minCoins, maxCoins);
        }
    }
    public int ingredientsAmount
    {
        get
        {
            return UnityEngine.Random.Range(minIngredients, maxIngredients);
        }
    }
    public Dictionary<Ingredient, int> obtainedIngredients = new Dictionary<Ingredient, int>();
    public int itemsAmount
    {
        get
        {
            return UnityEngine.Random.Range(minItems, maxItems);
        }
    }
    public Dictionary<BoardItem_Base, int> obtainedItems = new Dictionary<BoardItem_Base, int>();

    public void GetReward(BoardEntity entity)
    {
        entity.coins += coinsAmount;
        List<Ingredient> obtainableIngredients = new List<Ingredient>();

        foreach(RecipeElement rE in GameBoardManager.singleton.recipeStates[entity].requiredElements.Keys)
        {
            if (rE.GetType() == typeof(Ingredient)) obtainableIngredients.Add((Ingredient) rE);
        }

        for(int i = 0; i < ingredientsAmount; i++)
        {
            Ingredient ingredient = obtainableIngredients[UnityEngine.Random.Range(0, obtainableIngredients.Count)];
            if (!obtainedIngredients.ContainsKey(ingredient)) obtainedIngredients.Add(ingredient, 0);
            obtainedIngredients[ingredient] += 1;
            GameBoardManager.singleton.recipeStates[entity].SetCurrentElement(ingredient, GameBoardManager.singleton.recipeStates[entity].currentElements[ingredient] + 1);
        }

        List<BoardItem_Base> obtainableItems = Resources.LoadAll<BoardItem_Base>("BoardItems/Items").ToList();
        for (int i = 0; i < itemsAmount; i++)
        {
            BoardItem_Base item = obtainableItems[UnityEngine.Random.Range(0, obtainableItems.Count)];
            if (!obtainedItems.ContainsKey(item)) obtainedItems.Add(item, 0);
            obtainedItems[item] += 1;
            entity.inventory.AddItem(item);
        }
    }
}
