using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameManager : MonoBehaviour
{

    //public const int MAX_PLAYERS = 4;

    private int currentTurnIndex = 0;

    #region Private Methods
    private void InitializeGame()
    {
        if(entities.Count <= 0)
        {
            Debug.LogWarning("No players.");
            return;
        }

        foreach (BoardEntity entity in entities)
        {
            if (entity != null)
            {
                /*
                List<Vector3> waitZones = Coaster.initialCoaster.GetAvailableWaitZones();
                if (waitZones != null)
                {
                    entity.TeleportTo(Coaster.initialCoaster, waitZones[0]);
                    Coaster.initialCoaster.OccupeWaitZone(entity, waitZones[0]);
                }
                else
                {
                    entity.TeleportTo(Coaster.initialCoaster);
                }
                */
                entity.TeleportTo(Coaster.initialCoaster);
            }
        }
        entities[0].hasTurn = true;
    }
    #endregion

    #region Singleton
    public static BoardGameManager singleton;

    private void Awake()
    {
        if(singleton != null)
        {
            enabled = false;
            Debug.LogWarning($"Multiple instances of BoardGameManager detected. {name} has been disabled.");
            return;
        }
        singleton = this;
    }

    private void Start()
    {
        InitializeGame();
    }
    #endregion

    #region Events
    public event Action<BoardEntity> onTurnEnd;
    public void TurnEnd(BoardEntity player)
    {
        player.hasTurn = false;
        currentTurnIndex++;
        if(currentTurnIndex >= entities.Count)
        {
            currentTurnIndex = 0;
            // Start minigame.
            Debug.Log("Minigame starting...");
        } else
        {
            entities[currentTurnIndex].hasTurn = true;
        }
        //==========================
        onTurnEnd?.Invoke(player);
    }
    #endregion

    /* De momento public */
    public List<BoardEntity> entities = new List<BoardEntity>();
}