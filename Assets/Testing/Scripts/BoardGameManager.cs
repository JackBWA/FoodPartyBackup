using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameManager : MonoBehaviour
{

    //public const int MAX_PLAYERS = 4;

    private int currentTurnIndex = 0;

    #region Private Methods
    /*
    private int GetPlayerIndex(BoardPlayer player)
    {
        int indexResult = -1;
        int i = 0;
        while(i < players.Length)
        {
            if (players[i] == player)
            {
                indexResult = i;
                i = players.Length;
            } else
            {
                i++;
            }
        }
        return indexResult;
    }
    */

    private void InitializeGame()
    {
        foreach (BoardPlayer player in players)
        {
            if (player != null)
            {
                player.TeleportTo(Coaster.initialCoaster);
            }
        }
        players[0].hasTurn = true;
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
    #endregion

    #region Events
    public event Action<BoardPlayer> onTurnEnd;
    public void TurnEnd(BoardPlayer player)
    {
        player.hasTurn = false;
        currentTurnIndex++;
        if(currentTurnIndex >= players.Count)
        {
            currentTurnIndex = 0;
            // Start minigame.
            Debug.Log("Minigame starting...");
        } else
        {
            players[currentTurnIndex].hasTurn = true;
        }
        //==========================
        onTurnEnd?.Invoke(player);
    }
    #endregion

    /* De momento public */
    public List<BoardPlayer> players = new List<BoardPlayer>();

    private void Start()
    {
        InitializeGame();
    }
}