using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardPlayer : MonoBehaviour
{

    private int moves;
    private Coaster currentCoaster;

    #region Input
    public BoardPlayerControls playerControls;
    #endregion

    #region Components
    NavMeshAgent agent;
    #endregion

    #region BoardPlayerEvents
    public event Action onPlayerTurnStart;
    public void PlayerTurnStart()
    {
        if(onPlayerTurnStart != null)
        {
            onPlayerTurnStart();
        }
    }
    #endregion

    private void Awake()
    {
        TryGetComponent(out agent);

        playerControls = new BoardPlayerControls();
        playerControls.Dice.Throw.performed += _ => ThrowDice();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void ForceStop()
    {
        moves = 0;
    }

    public void TeleportTo(Coaster coaster)
    {
        ReloadAgent();
        bool success = agent.Warp(coaster.transform.position + new Vector3(0, transform.localScale.y, 0));
        if (success)
        {
            currentCoaster = coaster;
        }
        //Debug.Log(result);
    }

    public void SetMoves(int amount)
    {
        moves = amount;
        // Notify
        StartCoroutine(Move(currentCoaster.next[0]));
    }

    public IEnumerator Move(Coaster target)
    {
        agent.SetDestination(target.transform.position);
        yield return new WaitForSeconds(0.1f); // Funciona de momento.
        while (agent.velocity.magnitude > Vector3.kEpsilon)
        {
            yield return new WaitForSeconds(0.025f);
        }
        currentCoaster = target;

        // En el futuro checkear si se ve forzado a parar en dicha casilla.
        currentCoaster.playerEnter(this);

        moves--;
        if (moves > 0)
        {
            StartCoroutine(Move(currentCoaster.next[0]));
        }
        else
        {
            currentCoaster.playerStop(this);
            Debug.Log("Next turn.");
        }
    }

    private void ThrowDice()
    {
        //Cambiar el spawn del dado a cuando es el turno del jugador.
        Dice dice = Instantiate(
            ((GameObject)Resources.Load("Dice")).GetComponent<Dice>(),
            transform.position + Vector3.up * 5, Quaternion.identity);
        dice.owner = this;
        dice.Throw();
    }

    // Necesario para cuando el agente se deslinkea de su navmesh.
    private void ReloadAgent()
    {
        agent.enabled = false;
        agent.enabled = true;
    }
}