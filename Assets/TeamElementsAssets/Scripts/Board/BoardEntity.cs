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
        onTurnStart?.Invoke();
    }

    public event Action onTurnEnd;
    public void TurnEnd()
    {
        GameBoardManager.singleton.TurnEnd(this);
        onTurnEnd?.Invoke();
    }
    #endregion

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        hasTurn = false; // Posibilidad que no se tenga que indicar aqui.
    }

    protected virtual void Start()
    {

    }

    private void Update()
    {
        
    }
    #endregion

    public virtual void Initialize()
    {
        if(!TryGetComponent(out agent))
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        agent.radius = 0.1f;
        DisableAgent();
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

    public void TeleportTo(Vector3 position)
    {
        DisableAgent();
        agent.Warp(position + new Vector3(0f, transform.localScale.y, 0f));
        EnableAgent();
    }

    public void SetMoves(int amount)
    {
        moves = amount;
        // Notify
        StartCoroutine(Move(currentCoaster.next[0]));
    }

    public IEnumerator Move(Coaster target, float checkRate = 0.25f, float distanceRadius = 0.2f) // Or Vector3 targetPosition
    {
        Debug.Log(target);
        currentCoaster.playerLeave(this, transform.position);
        if (target != null)
        {
            List<Vector3> waitZones = target.GetAvailableWaitZones();
            if(waitZones != null && waitZones.Count > 0)
            {
                Debug.Log(waitZones[0]);
                agent.SetDestination(waitZones[0]);
            }

            while(Vector3.Distance(transform.position, waitZones[0]) > distanceRadius)
            {
                Debug.Log(Vector3.Distance(transform.position, waitZones[0]));
                yield return new WaitForSeconds(checkRate);
            }

            currentCoaster = target;
            currentCoaster.playerEnter(this, waitZones[0]);
            moves--;

            if(moves > 0)
            {
                StartCoroutine(Move(currentCoaster));
            } else
            {
                currentCoaster.playerStop(this);
                TurnEnd();
            }
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

    // Necesario para cuando el agente se deslinkea de su navmesh. // Deprecate (?)
    protected void ReloadAgent()
    {
        agent.enabled = false;
        agent.enabled = true;
    }

    protected void EnableAgent()
    {
        if(!agent.enabled) agent.enabled = true;
    }

    protected void DisableAgent()
    {
        if(agent.enabled) agent.enabled = false;
    }
}