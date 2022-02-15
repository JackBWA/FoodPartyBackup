using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coaster : MonoBehaviour
{
    public List<Coaster> next = new List<Coaster>();
    public List<BoardPlayer> players = new List<BoardPlayer>();

    public static Coaster initialCoaster;

    public bool isInitial
    {
        get
        {
            return type == CoasterType.Initial;
        }
    }

    /*
     * Que tipo de casillas hay:
     * 
     * Casilla normal.
     * Casilla de inicio.
     * Casilla de meta.
     * Casilla de protecci�n.
     * Casilla de teletransporte.
     * Casilla de tienda.
     * Casilla de bonus.
     * Casilla de trampa. (A�n se est� pensando).
     * 
     */

    // Editor (tal vez se haga xd)
    public enum CoasterType
    {
        Normal,
        Initial,
        Finish,
        Safe,
        Teleport,
        Shop,
        Bonus,
        Trap_STILL_THINKIN_PENSIVE
    }

    public CoasterType type;

    public bool canForceStop;

    private void Awake()
    {
        if (isInitial)
        {
            if(initialCoaster != null)
            {
                Debug.LogWarning($"Warning. There can be only one initial coaster. {name} will now be disabled.");
                enabled = false;
                return;
            }
            initialCoaster = this;
        }
    }

    // Realizar su funci�n.
    public virtual void Interact()
    {
        
    }

    // Movimiento en casillas.
    protected event Action<BoardPlayer> onPlayerEnter;
    public void playerEnter(BoardPlayer player)
    {
        Debug.Log("Player entered the coaster!");
        if(onPlayerEnter != null)
        {
            onPlayerEnter(player);
        }
    }

    protected event Action<BoardPlayer> onPlayerStop;
    public void playerStop(BoardPlayer player)
    {
        Debug.Log("Player reached the coaster!");
        if(onPlayerStop != null)
        {
            onPlayerStop(player);
        }
    }

    protected event Action<BoardPlayer> onPlayerLeave;
    public void playerLeave(BoardPlayer player)
    {
        Debug.Log("Player left the coaster!");
        if (onPlayerLeave != null)
        {
            onPlayerLeave(player);
        }
    }

    // Forzar la detenci�n del player.
    public void ForceStop(BoardPlayer player)
    {
        player.ForceStop();
    }

    // Almacenar ingredientes.
    /*
    public List<Interactible> ingredients = new List<Interactible>();
    public void StoreIngredient(InteractibleIngredient ingredient)
    {
        ingredients.Add(ingredient);
        // Mostrar visualmente. (FEEDBACK VISUAL!)
    }
    */

    // Poner trampas
    public List</*Replace with: Trap*/GameObject> traps = new List</*Replace with: Trap*/GameObject>();
    public void SetTrap(/*Trap trap*/)
    {
        //traps.Add(trap);
    }

    // Activarse o desactivarse.
    public bool isCoasterEnabled = true;
    public void ToggleCoasterState()
    {
        isCoasterEnabled = !isCoasterEnabled;
    }
    public void SetCoasterState(bool enabled)
    {
        isCoasterEnabled = enabled;
    }

    /* Que cosas puede hacer una casilla:
     * 
     * Realizar su funci�n.
     * Dar paso a otras casillas.
     * Forzar la detenci�n del player.
     * Almacenar ingredientes.
     * Poner trampas.
     * Activarse o desactivarse.
     */
}