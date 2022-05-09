using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapCoaster : Coaster
{

    public float healthAmount = 10f;
    public int coinsAmount = 15;
    public int itemsAmount = 1;

    public GameObject healthLoseParticlePrefab;

    public GameObject coinsLoseParticlePrefab;

    public GameObject ingredientLoseParticlePrefab;

    public enum TrapType
    {
        LoseHealth,
        LoseCoins,
        LoseIngredients
    }

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
        PlayerCharacter pC = interactor.GetComponent<PlayerCharacter>();
        if (pC != null && pC.characterType == PlayerCharacter.CharacterType.Player && !PlayerPrefs.HasKey(GetType().ToString()))
        {
            PlayerPrefs.SetInt(GetType().ToString(), 1);
            DisplayInfo(interactor/*title, description*/);
        } else
        {
            base.Interact(interactor);
            TrapType trapType = (TrapType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrapType)).Length);
            switch (trapType)
            {
                case TrapType.LoseHealth:
                    interactor.health = Mathf.Clamp(interactor.health - healthAmount, 0, interactor.baseHealth);
                    break;

                case TrapType.LoseCoins:
                    interactor.coins = Mathf.Clamp(interactor.coins - coinsAmount, 0, int.MaxValue); ;
                    break;

                case TrapType.LoseIngredients:
                    List<KeyValuePair<RecipeElement, int>> ingredients = GameBoardManager.singleton.recipeStates[interactor].currentElements.Where(rE => rE.Key.GetType() == typeof(Ingredient)).ToList();
                    foreach (KeyValuePair<RecipeElement, int> kV in ingredients)
                    {
                        GameBoardManager.singleton.recipeStates[interactor].SetCurrentElement(kV.Key, kV.Value - UnityEngine.Random.Range(0, kV.Value / 2));
                    }
                    /*
                    if (interactor.inventory.items.Count <= 0) break;
                    for (int i = 0; i < itemsAmount; i++)
                    {
                        BoardItem_Base item = interactor.inventory.items.ElementAt(UnityEngine.Random.Range(0, interactor.inventory.items.Count)).Key;
                        interactor.inventory.UpdateItem(item, interactor.inventory.items[item] - 1);
                        if (interactor.inventory.items.Count <= 0) break;
                    }
                    */
                    break;
            }

            StartCoroutine(ShowFeedback(trapType, interactor));
        }
    }

    private IEnumerator ShowFeedback(TrapType trapType, BoardEntity interactor)
    {
        switch (trapType)
        {
            case TrapType.LoseHealth:
                Instantiate(healthLoseParticlePrefab).transform.position = interactor.transform.position;
                break;

            case TrapType.LoseCoins:
                Instantiate(coinsLoseParticlePrefab).transform.position = interactor.transform.position;
                break;

            case TrapType.LoseIngredients:
                Instantiate(ingredientLoseParticlePrefab).transform.position = interactor.transform.position;
                break;
        }
        yield return new WaitForSeconds(1f);
        EndInteract(interactor);
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