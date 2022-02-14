using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoardPlayer : MonoBehaviour
{

    public int movesLeft { get; private set; }

    #region Components
    NavMeshAgent agent;
    #endregion

    private void Start()
    {
        TryGetComponent(out agent);
    }

    public void ForceStop()
    {
        movesLeft = 0;
    }

    public void TeleportTo(Coaster coaster)
    {
        bool result = agent.Warp(coaster.transform.position + new Vector3(0, transform.localScale.y, 0));

        Debug.Log(result);

        agent.SetDestination(coaster.next[0].transform.position/* de momento [0] no hay multiples caminos aun*/);
    }
}