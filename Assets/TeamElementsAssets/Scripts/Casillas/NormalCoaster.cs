using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NormalCoaster : Coaster
{

    public bool hasFlavor
    {
        get
        {
            return _hasFlavor;
        }
        set
        {
            _hasFlavor = value;
            if(flavorParticle != null)
            {
                if (hasFlavor)
                {
                    canForceInteract = true;
                    flavorParticle.Play();
                }
                else
                {
                    canForceInteract = false;
                    flavorParticle?.Stop();
                }
            }
        }
    }
    private bool _hasFlavor = false;

    public int interactionCost = 50;

    public ParticleSystem flavorParticle;

    private Flavor GetRandomFlavor(BoardEntity interactor)
    {
        Flavor flavor = null;
        /*
        foreach(KeyValuePair<RecipeElement, int> xd in GameBoardManager.singleton.recipeStates[interactor].requiredElements)
        {
            /Debug.Log($"{xd.Key.name} | {xd.Key.GetType()}");
        }
        */
        //Debug.Log("========================================================================");
        List<KeyValuePair<RecipeElement, int>> list = GameBoardManager.singleton.recipeStates[interactor].requiredElements.Where(rE => rE.Key.GetType() == typeof(Flavor)).ToList();
        //Debug.Log("List size is: " + list.Count);
        int random = UnityEngine.Random.Range(0, list.Count);
        //Debug.Log("Random number generated is: " + random);
        //Debug.Log("List elements are: ");
        /*
        foreach(KeyValuePair<RecipeElement, int> kvp in list)
        {
            Debug.Log(kvp.Key.name + " | Type of: " + kvp.Key.GetType().ToString());
        }
        */
        //Debug.Log("Flavor should be null here: " + (flavor == null));
        flavor = (Flavor) list[UnityEngine.Random.Range(0, list.Count)].Key;
        //Debug.Log("Flavor should NOT be null here: " + (flavor == null));
        //Debug.Log("Flavor should NOT be null here. Value equals to: " + flavor);
        return flavor;
        //flavor =  GameBoardManager.singleton.recipeStates[interactor].requiredElements.Where(rE => rE.GetType().Equals(typeof(Flavor))).ToList().ElementAt(UnityEngine.Random.Range(0, GameBoardManager.singleton.recipeStates[interactor].requiredElements.Count)).Key;
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
            if (hasFlavor)
            {
                switch (interactor)
                {
                    case BoardPlayer player:
                        StartCoroutine(RequestCo(interactor));
                        break;

                    case BoardAI ai:
                        if (interactor.coins >= interactionCost)
                        {
                            Flavor flavor = GetRandomFlavor(interactor);
                            GameBoardManager.singleton.recipeStates[interactor].SetCurrentElement(flavor, GameBoardManager.singleton.recipeStates[interactor].currentElements[flavor] + 1);
                            GameBoardManager.singleton.SpawnFlavorOnRandomNormalCoaster();
                            EndInteract(interactor);
                        }
                        break;
                }
            }
            else
            {
                EndInteract(interactor);
            }
            //Debug.Log("Normal interact!");
            //base.EndInteract(interactor);
        }
    }

    private IEnumerator RequestCo(BoardEntity interactor)
    {
        RequestManager requestManager = Instantiate(Resources.Load<RequestManager>("RequestCanvas"));
        requestManager.title = "You've reached the flavor coaster!";
        requestManager.message = "Would you like to obtain this flavor?";
        requestManager.acceptButtonText = "Yes";
        requestManager.declineButtonText = "No";

        while (!requestManager.hasSubmittedRequest)
        {
            yield return null;
        }

        bool result = requestManager.requestResult;
        Destroy(requestManager.gameObject);

        if (result)
        {
            Flavor flavor = GetRandomFlavor(interactor);
            GameBoardManager.singleton.recipeStates[interactor].SetCurrentElement(flavor, GameBoardManager.singleton.recipeStates[interactor].currentElements[flavor] + 1);
            GameBoardManager.singleton.SpawnFlavorOnRandomNormalCoaster();
        }
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
