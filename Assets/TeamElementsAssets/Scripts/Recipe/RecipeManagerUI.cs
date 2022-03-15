using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagerUI : MonoBehaviour
{
    [HideInInspector]
    public Recipe recipe;

    public static RecipeManagerUI singleton;

    private RecipeElementUI elementPrefab;

    public GameObject flavorsHolder;
    public GameObject ingredientsHolder;

    [HideInInspector]
    public List<RecipeElementUI> recipeElementsUI;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;
        elementPrefab = Resources.Load<RecipeElementUI>("Recipes/UI/RecipeUIElement");
        ToggleDisplay(true);
    }

    private void OnEnable()
    {
        GameBoardManager.singleton.onTurnStart += UpdateDisplay;
    }

    private void OnDisable()
    {
        GameBoardManager.singleton.onTurnStart -= UpdateDisplay;
    }

    public void ToggleDisplay(bool enabled)
    {
        gameObject.SetActive(enabled);
        this.enabled = enabled;
    }

    public void UpdateDisplay(BoardEntity entity)
    {
        if(GameBoardManager.singleton.recipeStates.TryGetValue(entity, out recipe))
        {
            Display(recipe);
        }
    }

    public void Display(Recipe recipe)
    {
        Clear();
        for(int i = 0; i < recipe.flavors.Count; i++)
        {
            RecipeElementUI reUI = Instantiate(elementPrefab);
            FlavorAmount fA = recipe.flavors[i];
            if(fA.flavor.icon != null) reUI.SetImage(fA.flavor.icon);
            reUI.SetAmount(recipe.currentElements[fA.flavor], recipe.requiredElements[fA.flavor]);
            reUI.transform.SetParent(flavorsHolder.transform);
            recipeElementsUI.Add(reUI);
        }

        for(int i = 0; i < recipe.ingredients.Count; i++)
        {
            RecipeElementUI reUI = Instantiate(elementPrefab);
            IngredientAmount iA = recipe.ingredients[i];
            if(iA.ingredient.icon != null) reUI.SetImage(iA.ingredient.icon);
            reUI.SetAmount(recipe.currentElements[iA.ingredient], recipe.requiredElements[iA.ingredient]);
            reUI.transform.SetParent(ingredientsHolder.transform);
            recipeElementsUI.Add(reUI);
        }
    }

    private void Clear()
    {
        foreach(RecipeElementUI reUI in recipeElementsUI)
        {
            Destroy(reUI.gameObject);
        }
        recipeElementsUI.Clear();
    }
}