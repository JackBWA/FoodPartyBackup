using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameManager : MonoBehaviour
{

    public const int MAX_PLAYERS = 4;

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

    /* De momento public */public BoardPlayer[] players = new BoardPlayer[MAX_PLAYERS];

    private void Start()
    {
        foreach(BoardPlayer player in players)
        {
            if(player != null)
            {
                player.TeleportTo(Coaster.initialCoaster);
            }
        }
    }
}