using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCoaster : Coaster
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void RequestInteract(BoardEntity interactor, string title = "Petición", string message = "Mensaje", string acceptText = "Si", string declineText = "No")
    {
        base.RequestInteract(interactor, "Casilla de meta", "Te gustaria detenerte aquí?", acceptText, declineText);
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
            //Debug.Log("Finish interact!");

            //Debug.Log("Recipe completed? " + GameBoardManager.singleton.recipeStates[interactor].isCompleted);
            if (GameBoardManager.singleton.recipeStates[interactor].isCompleted)
            {
                GameBoardManager.singleton.winner = interactor;
            }
            else
            {
                // Might save ingredients and flavors (remove them from the required on the recipe).
                EndInteract(interactor);
            }
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

    protected override void RequestChecker(BoardEntity entity)
    {
        switch (entity.GetComponent<PlayerCharacter>().characterType)
        {
            case PlayerCharacter.CharacterType.Player:
                base.RequestChecker(entity);
                break;

            case PlayerCharacter.CharacterType.AI:
                // Aqui el bot "piensa" xd.
                if (GameBoardManager.singleton.recipeStates[entity].isCompleted)
                {
                    Interact(entity);
                } else
                {
                    if (entity.GetMoves() > 0) entity.ContinueMoving();
                    else entity.TurnEnd();
                }
                break;
        }
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
