using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoaster : Coaster
{

    public float healthAmount = 30;
    public int coinsAmount = 20;
    public int ingredientsAmount = 25;

    public static List<BoardItem_Base> obtainableItems = new List<BoardItem_Base>();

    public enum BonusType
    {
        HealthBonus,
        CoinsBonus,
        ItemBonus,
        IngredientBonus
    }

    protected override void Awake()
    {
        base.Awake();
        /* // Moving to interact.
        foreach(BoardItem_Base item in Resources.LoadAll<BoardItem_Base>("BoardItems/Items")){
            obtainableItems.Add(item);
        }
        */
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(BoardEntity interactor)
    {
        PlayerCharacter pC = interactor.GetComponent<PlayerCharacter>();
        if (pC != null && pC.characterType == PlayerCharacter.CharacterType.Player && !PlayerPrefs.HasKey(GetType().ToString()))
        {
            PlayerPrefs.SetInt(GetType().ToString(), 1);
            DisplayInfo(interactor/*title, description*/);
        } else
        {
            base.Interact(interactor);

            BonusType bonusType = (BonusType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(BonusType)).Length);
            switch (bonusType)
            {
                case BonusType.HealthBonus:
                    interactor.health = Mathf.Clamp(interactor.health + healthAmount, 0, interactor.baseHealth);
                    break;

                case BonusType.CoinsBonus:
                    interactor.coins = Mathf.Clamp(interactor.coins + coinsAmount, 0, int.MaxValue);
                    break;

                case BonusType.ItemBonus:
                    BoardItem_Base randomItem;
                    BoardItem_Base[] itemList = Resources.LoadAll<BoardItem_Base>("BoardItems/Items");
                    randomItem = itemList[UnityEngine.Random.Range(0, itemList.Length)];
                    /*
                     * 
                     * Give some feedback.
                     * 
                     */
                    interactor.inventory.AddItem(randomItem);
                    break;

                case BonusType.IngredientBonus:
                    List<Ingredient> obtainableIngredients = new List<Ingredient>();
                    foreach (Ingredient i in GameBoardManager.singleton.recipeStates[interactor].requiredElements.Keys.Where((e) => e.GetType() == typeof(Ingredient)).ToList())
                    {
                        obtainableIngredients.Add(i);
                    }

                    for (int i = 0; i < ingredientsAmount; i++)
                    {
                        Ingredient ingredient = obtainableIngredients[UnityEngine.Random.Range(0, obtainableIngredients.Count)];
                        GameBoardManager.singleton.recipeStates[interactor].currentElements[ingredient]++;
                    }
                    break;
            }

            EndInteract(interactor);
        }
    }

    public override void EndInteract(BoardEntity interactor)
    {
        base.EndInteract(interactor);
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