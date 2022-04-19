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

    protected override void RequestInteract(BoardEntity interactor, string title = "Request", string message = "Message", string acceptText = "Accept", string declineText = "Decline")
    {
        base.RequestInteract(interactor, "End Goal", "Would you like to stop here?", acceptText, declineText);
    }

    public override void Interact(BoardEntity interactor)
    {
        base.Interact(interactor);
        Debug.Log("Finish interact!");

        //Debug.Log("Recipe completed? " + GameBoardManager.singleton.recipeStates[interactor].isCompleted);
        if (GameBoardManager.singleton.recipeStates[interactor].isCompleted)
        {
            GameBoardManager.singleton.winner = interactor;
        } else
        {
            // Might save ingredients and flavors (remove them from the required on the recipe).
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
