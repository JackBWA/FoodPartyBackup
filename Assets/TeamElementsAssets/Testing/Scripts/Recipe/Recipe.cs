using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Cooking Party/Recipe/Empty Recipe")]
public class Recipe : ScriptableObject
{
    public string title;

    public List<FlavorAmount> flavors = new List<FlavorAmount>();
    public List<IngredientAmount> ingredients = new List<IngredientAmount>();

    /*
    public Dictionary<Flavor, int> requiredFlavors;
    public Dictionary<Ingredient, int> requiredIngredients;
    */

    public Dictionary<RecipeElement, int> requiredElements;
    public Dictionary<RecipeElement, int> currentElements;

    /*
    public Dictionary<Flavor, int> currentFlavors;
    public Dictionary<Ingredient, int> currentIngredients;
    */

    public bool isCompleted
    {
        get
        {
            bool result = true;
            foreach(KeyValuePair<RecipeElement, int> recipeElement in currentElements)
            {
                if (recipeElement.Value != requiredElements[recipeElement.Key]) result = false;
            }

            /*
            foreach (KeyValuePair<Flavor, int> flavor in currentFlavors)
            {
                if (flavor.Value != requiredFlavors[flavor.Key])
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
            */
            return result;
        }
    }

    public event Action onRecipeUpdate;
    public void RecipeUpdate()
    {
        onRecipeUpdate?.Invoke();
    }

    public void CopyFrom(Recipe recipe)
    {
        title = recipe.title;

        flavors = new List<FlavorAmount>();
        foreach(FlavorAmount flavor in recipe.flavors)
        {
            flavors.Add(flavor);
        }

        ingredients = new List<IngredientAmount>();
        foreach (IngredientAmount ingredient in recipe.ingredients)
        {
            ingredients.Add(ingredient);
        }

        requiredElements = new Dictionary<RecipeElement, int>();
        foreach(KeyValuePair<RecipeElement, int> recipeElement in recipe.requiredElements)
        {
            requiredElements.Add(recipeElement.Key, recipeElement.Value);
        }

        currentElements = new Dictionary<RecipeElement, int>();
        foreach (KeyValuePair<RecipeElement, int> recipeElement in recipe.currentElements)
        {
            currentElements.Add(recipeElement.Key, recipeElement.Value);
        }

        RecipeUpdate();

        /*
        requiredFlavors = new Dictionary<Flavor, int>();
        foreach(KeyValuePair<Flavor, int> kV in recipe.requiredFlavors)
        {
            requiredFlavors.Add(kV.Key, kV.Value);
        }

        requiredIngredients = new Dictionary<Ingredient, int>();
        foreach (KeyValuePair<Ingredient, int> kV in recipe.requiredIngredients)
        {
            requiredIngredients.Add(kV.Key, kV.Value);
        }

        currentFlavors = new Dictionary<Flavor, int>();
        foreach (KeyValuePair<Flavor, int> kV in recipe.currentFlavors)
        {
            currentFlavors.Add(kV.Key, kV.Value);
        }

        currentIngredients = new Dictionary<Ingredient, int>();
        foreach (KeyValuePair<Ingredient, int> kV in recipe.currentIngredients)
        {
            currentIngredients.Add(kV.Key, kV.Value);
        }
        */

        /*
        flavors = recipe.flavors;
        ingredients = recipe.ingredients;

        requiredFlavors = recipe.requiredFlavors;
        requiredIngredients = recipe.requiredIngredients;

        currentFlavors = recipe.currentFlavors;
        currentIngredients = recipe.currentIngredients;
        */
    }

    public void Complete()
    {
        foreach(KeyValuePair<RecipeElement, int> recipeElement in requiredElements)
        {
            currentElements[recipeElement.Key] = requiredElements[recipeElement.Key];
        }

        RecipeUpdate();

        /*
        foreach(KeyValuePair<Flavor, int> kV in requiredFlavors)
        {
            currentFlavors[kV.Key] = requiredFlavors[kV.Key];
        }

        foreach(KeyValuePair<Ingredient, int> kV in requiredIngredients)
        {
            currentIngredients[kV.Key] = requiredIngredients[kV.Key];
        }
        */
    }

    public void Initialize()
    {

        requiredElements = new Dictionary<RecipeElement, int>();

        /*
        requiredFlavors = new Dictionary<Flavor, int>();
        requiredIngredients = new Dictionary<Ingredient, int>();
        */

        currentElements = new Dictionary<RecipeElement, int>();

        /*
        currentFlavors = new Dictionary<Flavor, int>();
        currentIngredients = new Dictionary<Ingredient, int>();
        */

        foreach (FlavorAmount flavor in flavors)
        {
            int amount = flavor.amount;
            if (flavor.randomAmount)
            {
                amount = flavor.GetRandomAmount();
            }

            requiredElements.Add(flavor.flavor, amount);
            currentElements.Add(flavor.flavor, 0);

            /*
            requiredFlavors.Add(flavor.flavor, amount);
            currentFlavors.Add(flavor.flavor, 0);
            */
        }

        foreach(IngredientAmount ingredient in ingredients)
        {
            int amount = ingredient.amount;
            if (ingredient.randomAmount)
            {
                amount = ingredient.GetRandomAmount();
            }

            requiredElements.Add(ingredient.ingredient, amount);
            currentElements.Add(ingredient.ingredient, 0);

            /*
            requiredIngredients.Add(ingredient.ingredient, amount);
            currentIngredients.Add(ingredient.ingredient, 0);
            */
        }
    }

    public override string ToString()
    {
        // NEW
        Dictionary<Flavor, int> auxFlavors = new Dictionary<Flavor, int>();
        Dictionary<Ingredient, int> auxIngredients = new Dictionary<Ingredient, int>();
        
        foreach(KeyValuePair<RecipeElement, int> rE in currentElements)
        {
            switch (rE.Key)
            {
                case Flavor f:
                    auxFlavors.Add(f, rE.Value);
                    break;

                case Ingredient i:
                    auxIngredients.Add(i, rE.Value);
                    break;
            }
        }
        // END NEW

        string result = $"Recipe for {title}!\n" +
            $"Flavors:\n";
        foreach (KeyValuePair<Flavor, int> kv in auxFlavors)
        {
            result += $"     {kv.Key.name}: {kv.Value}/{requiredElements[kv.Key]}\n";
        }
        result += $"Ingredients:\n";
        foreach (KeyValuePair<Ingredient, int> kv in auxIngredients)
        {
            result += $"     {kv.Key.name}: {kv.Value}/{requiredElements[kv.Key]}\n";
        }
        return result;
    }

    #region Recipe Methods
    public bool AddRequiredElement(RecipeElement element, int amount)
    {
        bool added = !requiredElements.ContainsKey(element);
        if (added) requiredElements.Add(element, amount);

        RecipeUpdate();
        /*
        bool containsFlavor = requiredFlavors.ContainsKey(flavor);
        if (!containsFlavor) requiredFlavors.Add(flavor, amount);
        */
        return added;
    }

    public bool RemoveRequiredElement(RecipeElement element)
    {
        bool removed = requiredElements.ContainsKey(element);
        if (removed) requiredElements.Remove(element);

        RecipeUpdate();
        /*
        bool containsFlavor = requiredFlavors.ContainsKey(flavor);
        if (containsFlavor) requiredFlavors.Remove(flavor);
        */
        return removed;
    }

    public bool UpdateRequiredElement(RecipeElement element, int newAmount)
    {
        bool wasUpdated = requiredElements.ContainsKey(element);
        if (wasUpdated) requiredElements[element] = newAmount;

        RecipeUpdate();
        /*
        bool wasUpdated = requiredFlavors.ContainsKey(flavor);
        if (wasUpdated) requiredFlavors[flavor] = newAmount;
        */
        return wasUpdated;
    }

    public void SetCurrentElement(RecipeElement element, int newAmount)
    {
        if (currentElements.ContainsKey(element))
        {
            currentElements[element] = newAmount;
        }

        RecipeUpdate();
        /*
        if (currentFlavors.ContainsKey(flavor))
        {
            currentFlavors[flavor] = newAmount;
        }
        */
    }

    #endregion


    // Deprecated since everything was packed to the same parent class.
    #region Ingredient Methods (deprecated)
    /*
    public bool AddRequiredIngredient(Ingredient ingredient, int amount)
    {
        bool containsFlavor = requiredIngredients.ContainsKey(ingredient);
        if (!containsFlavor) requiredIngredients.Add(ingredient, amount);
        return added;
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
    */
    #endregion
}