using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class NormalCoaster : Coaster
{

    public CinemachineVirtualCamera cam;

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
                    flavorCoaster = this;
                    canForceInteract = true;
                    flavorParticle.Play();
                    flavorChest.gameObject.SetActive(true);
                }
                else
                {
                    canForceInteract = false;
                    flavorParticle?.Stop();
                    flavorChest.gameObject.SetActive(false);
                }
            }
        }
    }
    private bool _hasFlavor = false;

    public int interactionCost = 50;

    public ParticleSystem flavorParticle;

    public Animation flavorChest;

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

    private void OnEnable()
    {
        if (hasFlavor)
        {
            flavorParticle.Play();
        }
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
                            StartCoroutine(RetrieveFlavor(interactor));
                        }
                        else
                        {
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

    public IEnumerator RetrieveFlavor(BoardEntity interactor)
    {
        Flavor flavor = GetRandomFlavor(interactor);
        interactor.coins -= interactionCost;
        GameBoardManager.singleton.recipeStates[interactor].SetCurrentElement(flavor, GameBoardManager.singleton.recipeStates[interactor].currentElements[flavor] + 1);
        flavorChest.Play("bandejacofreopen");
        yield return new WaitForSeconds(flavorChest.GetClip("bandejacofreopen").length);
        NormalCoaster next = GameBoardManager.singleton.SpawnFlavorOnRandomNormalCoaster();
        yield return new WaitForSeconds(.25f);
        next.cam.enabled = true;
        yield return new WaitForSeconds(1.25f);
        next.flavorChest.Play("bandejacofrespawn");
        yield return new WaitForSeconds(next.flavorChest.GetClip("bandejacofrespawn").length + .5f);
        next.cam.enabled = false;
        yield return new WaitForSeconds(1.5f);
        EndInteract(interactor);
    }

    private IEnumerator RequestCo(BoardEntity interactor)
    {
        interactor.LockTPC();
        RequestManager requestManager = Instantiate(Resources.Load<RequestManager>("UI/RequestCanvas"));
        requestManager.title = "Has encontrado el condimento!";
        requestManager.message = $"Te gustaria obtenerlo por {interactionCost} monedas?";
        requestManager.acceptButtonText = "Si";
        requestManager.declineButtonText = "No";

        while (!requestManager.hasSubmittedRequest)
        {
            yield return null;
        }

        bool result = requestManager.requestResult;
        Destroy(requestManager.gameObject);

        if (result)
        {
            if (interactor.coins >= interactionCost)
            {
                StartCoroutine(RetrieveFlavor(interactor));
            } else
            {
                EndInteract(interactor);
            }
            /*
            Flavor flavor = GetRandomFlavor(interactor);
            GameBoardManager.singleton.recipeStates[interactor].SetCurrentElement(flavor, GameBoardManager.singleton.recipeStates[interactor].currentElements[flavor] + 1);
            GameBoardManager.singleton.SpawnFlavorOnRandomNormalCoaster();
            */
        }
        interactor.UnlockTPC();
        //EndInteract(interactor);
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
