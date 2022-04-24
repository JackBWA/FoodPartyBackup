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

    public ParticleSystem flavorParticle;

    private Flavor GetRandomFlavor(BoardEntity interactor)
    {
        Flavor flavor;
        flavor = (Flavor) GameBoardManager.singleton.recipeStates[interactor].requiredElements.ElementAt(UnityEngine.Random.Range(0, GameBoardManager.singleton.recipeStates[interactor].requiredElements.Count)).Key;
        return flavor;
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
        base.Interact(interactor);

        StartCoroutine(RequestCo(interactor));

        //Debug.Log("Normal interact!");
        //base.EndInteract(interactor);
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
