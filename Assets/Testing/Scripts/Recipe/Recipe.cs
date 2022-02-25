using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cooking Party/Recipe/Empty Recipe")]
public class Recipe : ScriptableObject
{
    public string title;

    [TextArea]
    public string description;

    public Dictionary<Flavor, int> requiredFlavors;
    public Dictionary<Ingredient, int> requiredIngredients;

    public Dictionary<Flavor, int> currentFlavors;
    public Dictionary<Ingredient, int> currentIngredients;

    public bool IsRecipeCompleted
    {
        get
        {
            bool result = true;
            foreach (KeyValuePair<Flavor, int> flavor in currentFlavors)
            {
                if(flavor.Value != requiredFlavors[flavor.Key])
                {
                    result = false;
                }
            }
            if (result)
            {
                foreach (KeyValuePair<Ingredient, int> ingredient in currentIngredients)
                {
                    if (ingredient.Value != requiredIngredients[ingredient.Key])
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }

    #region Flavor Methods
    public bool AddRequiredFlavor(Flavor flavor, int amount)
    {
        bool containsFlavor = requiredFlavors.ContainsKey(flavor);
        if (!containsFlavor) requiredFlavors.Add(flavor, amount);
        return containsFlavor;
    }

    public bool RemoveRequiredFlavor(Flavor flavor)
    {
        bool containsFlavor = requiredFlavors.ContainsKey(flavor);
        if (containsFlavor) requiredFlavors.Remove(flavor);
        return containsFlavor;
    }

    public bool UpdateRequiredFlavor(Flavor flavor, int newAmount)
    {
        bool wasUpdated = requiredFlavors.ContainsKey(flavor);
        if (wasUpdated) requiredFlavors[flavor] = newAmount;
        return wasUpdated;
    }

    public void SetCurrentFlavor(Flavor flavor, int newAmount)
    {
        if (currentFlavors.ContainsKey(flavor))
        {
            currentFlavors[flavor] = newAmount;
        }
    }

    #endregion

    #region Ingredient Methods
    public bool AddRequiredIngredient(Ingredient ingredient, int amount)
    {
        bool containsFlavor = requiredIngredients.ContainsKey(ingredient);
        if (!containsFlavor) requiredIngredients.Add(ingredient, amount);
        return containsFlavor;
    }

    public bool RemoveRequiredIngredient(Ingredient ingredient)
    {
        bool containsIngredient = requiredIngredients.ContainsKey(ingredient);
        if (containsIngredient) requiredIngredients.Remove(ingredient);
        return containsIngredient;
    }

    public bool UpdateRequiredIngredient(Ingredient ingredient, int newAmount)
    {
        bool wasUpdated = requiredIngredients.ContainsKey(ingredient);
        if (wasUpdated) requiredIngredients[ingredient] = newAmount;
        return wasUpdated;
    }

    public void SetCurrentIngredient(Ingredient ingredient, int newAmount)
    {
        if (currentIngredients.ContainsKey(ingredient))
        {
            currentIngredients[ingredient] = newAmount;
        }
    }

    #endregion

    public static Recipe CreateRandomRecipe(int flavorsAmount, int ingredientsAmount, List<Flavor> usableFlavors, List<Ingredient> usableIngredients)
    {
        Recipe recipe = new Recipe();
        for(int f = 0; f < flavorsAmount; f++)
        {
            Flavor rnd = usableFlavors[Random.Range(0, usableFlavors.Count)];
            while (recipe.AddRequiredFlavor(rnd, 1))
            {
                rnd = usableFlavors[Random.Range(0, usableFlavors.Count)];
            }
            recipe.SetCurrentFlavor(rnd, 0);
        }

        for(int i = 0; i < ingredientsAmount; i++)
        {
            Ingredient rnd = usableIngredients[Random.Range(0, usableIngredients.Count)];
            while (recipe.AddRequiredIngredient(rnd, 5))
            {
                rnd = usableIngredients[Random.Range(0, usableIngredients.Count)];
            }
            recipe.SetCurrentIngredient(rnd, 0);
        }
        return recipe;
    }
}