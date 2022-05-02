using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GreddyPot : BoardItem_Base
{

    BoardItemControls inputActions;

    public int maxFlavors = 1;
    public int maxIngredients = 15;

    public GreedyPotCanvas canvasPrefab;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new BoardItemControls();
        InitializeControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void InitializeControls()
    {
        inputActions.General.Use.performed += _ => Use();
        inputActions.General.Cancel.performed += _ => Cancel();
    }

    public override void Use()
    {
        base.Use();
        StartCoroutine(GiveRecipeElements());
    }

    private IEnumerator GiveRecipeElements()
    {
        List<KeyValuePair<RecipeElement, int>> flavorList = GameBoardManager.singleton.recipeStates[owner].requiredElements.Where(rE => rE.Key.GetType() == typeof(Flavor)).ToList();
        Flavor flavor = (Flavor)flavorList[UnityEngine.Random.Range(0, flavorList.Count)].Key;

        List<KeyValuePair<RecipeElement, int>> ingredientList = GameBoardManager.singleton.recipeStates[owner].requiredElements.Where(rE => rE.Key.GetType() == typeof(Ingredient)).ToList();
        Dictionary<Ingredient, int> ingredients = new Dictionary<Ingredient, int>();

        for(int i = 0; i < maxIngredients; i++)
        {
            Ingredient rndIngredient = (Ingredient) ingredientList[UnityEngine.Random.Range(0, ingredientList.Count)].Key;
            if (!ingredients.ContainsKey(rndIngredient))
                ingredients.Add(rndIngredient, 0);

            ingredients[rndIngredient]++;
        }
        
        GameBoardManager.singleton.recipeStates[owner].SetCurrentElement(flavor, GameBoardManager.singleton.recipeStates[owner].currentElements[flavor] + 1);
        foreach(KeyValuePair<Ingredient, int> iA in ingredients)
        {
            GameBoardManager.singleton.recipeStates[owner].SetCurrentElement(iA.Key, iA.Value);
        }

        GreedyPotCanvas canvasInstance = Instantiate(canvasPrefab);
        canvasInstance.AddElement(flavor.icon, 1);
        yield return new WaitForSeconds(.1f);
        foreach(KeyValuePair<Ingredient, int> kV in ingredients)
        {
            canvasInstance.AddElement(kV.Key.icon, kV.Value);
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(5f);
        Destroy(canvasInstance.gameObject);
        EndUse();
    }
}