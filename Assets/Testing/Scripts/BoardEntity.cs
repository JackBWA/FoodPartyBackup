using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardEntity : MonoBehaviour
{
    public bool hasTurn
    {
        get
        {
            return turn;
        }

        set
        {
            turn = value;
            if (turn)
            {
                TurnStart();
            }
        }
    }

    protected bool turn;
    protected Dice dice;
    protected int moves;

    public Coaster currentCoaster;

    #region Components
    protected NavMeshAgent agent;
    #endregion

    #region Events
    public event Action onTurnStart;
    public void TurnStart()
    {
        if (onTurnStart != null)
        {
            onTurnStart();
        }
    }
    #endregion

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        //TryGetComponent(out agent);
        //Debug.Log("Entity");
    }

    protected virtual void Start()
    {

    }
    #endregion

    public virtual void InitializeEntity()
    {
        TryGetComponent(out agent);
        BindEvents();
    }

    protected virtual void BindEvents()
    {
        onTurnStart += SpawnDice;
    }

    protected virtual void UnbindEvents()
    {
        onTurnStart -= SpawnDice;
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

    public void TeleportTo(Coaster coaster, Vector3 position)
    {
        ReloadAgent();
        bool success = agent.Warp(position + new Vector3(0, transform.localScale.y, 0));
        if (success)
        {
            currentCoaster = coaster;
        }
    }

    public void SetMoves(int amount)
    {
        moves = amount;
        // Notify
        StartCoroutine(Move(currentCoaster.next[0]));
    }

    public IEnumerator Move(Coaster target)
    {
        List<Vector3> availableWaitZones = target.GetAvailableWaitZones();
        /*
        Debug.Log("=========================");
        if(availableWaitZones != null)
        {
            foreach(Vector3 zone in availableWaitZones)
            {
                Debug.Log(zone);
            }
        }
        */
        if (availableWaitZones != null && availableWaitZones.Count > 0)
        {
            agent.SetDestination(availableWaitZones[0]);
        } else
        {
            // No deberia de triggerearse.
            agent.SetDestination(target.transform.position);
        }

        yield return new WaitForSeconds(0.1f); // Funciona de momento.
        while (agent.velocity.magnitude > Vector3.kEpsilon)
        {
            yield return new WaitForSeconds(0.025f);
        }
        currentCoaster = target;

        // En el futuro checkear si se ve forzado a parar en dicha casilla.
        if(availableWaitZones != null) currentCoaster.playerEnter(this, availableWaitZones[0]);
        else currentCoaster.playerEnter(this, currentCoaster.transform.position);

        moves--;
        if (moves > 0)
        {
            currentCoaster.playerLeave(this, availableWaitZones[0]);
            StartCoroutine(Move(currentCoaster.next[0]));
        }
        else
        {
            currentCoaster.playerStop(this);
            BoardGameManager.singleton.TurnEnd(this);
            Debug.Log("Next turn.");
        }
    }

    protected void SpawnDice()
    {
        dice = Instantiate(
            ((GameObject)Resources.Load("Dice")).GetComponent<Dice>());
        dice.transform.position = transform.position + Vector3.up * 5;
        dice.owner = this;
    }

    public void ThrowDice()
    {
        /*
        //Cambiar el spawn del dado a cuando es el turno del jugador.
        Dice dice = Instantiate(
            ((GameObject)Resources.Load("Dice")).GetComponent<Dice>(),
            transform.position + Vector3.up * 5, Quaternion.identity);
        dice.owner = this;
        */
        if (!turn || dice == null || dice.used)
        {
            return;
        }

        ObjectRotator objRot;
        if (dice.TryGetComponent(out objRot))
        {
            objRot.enabled = false;
        }
        dice.Throw();
    }

    // Necesario para cuando el agente se deslinkea de su navmesh.
    protected void ReloadAgent()
    {
        agent.enabled = false;
        agent.enabled = true;
    }
}