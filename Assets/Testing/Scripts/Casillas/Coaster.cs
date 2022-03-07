using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coaster : MonoBehaviour
{
    public List<Coaster> next = new List<Coaster>();
    public List<BoardEntity> players = new List<BoardEntity>();

    public static Coaster initialCoaster;

    public int coasterId { get; private set; }
    private static int autoId = 0;

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
     * Casilla de protección.
     * Casilla de teletransporte.
     * Casilla de tienda.
     * Casilla de bonus.
     * Casilla de trampa. (Aún se está pensando).
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
        Shop/*,
        Bonus,
        Trap*/
    }

    public CoasterType type;

    public bool canRequestStop;
    private bool canForceStop;

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        coasterId = autoId;
        autoId++;
    }

    protected virtual void Start()
    {
        if(autoId != 0) autoId = 0;
    }
    #endregion

    public void Initialize(int waitZonesAmount)
    {
        if (isInitial)
        {
            if (initialCoaster != null)
            {
                Debug.LogWarning($"Warning. There can be only one initial coaster. {name} will now be disabled.");
                enabled = false;
                return;
            }
            initialCoaster = this;
        }
    }

    // Realizar su función.
    public virtual void Interact()
    {

    }

    // Movimiento en casillas.
    protected event Action<BoardEntity, Vector3> onPlayerEnter;
    public void playerEnter(BoardEntity player, Vector3 position)
    {
        //Debug.Log("Player entered the coaster!");
        if(onPlayerEnter != null)
        {
            onPlayerEnter(player, position);
        }
    }

    protected event Action<BoardEntity> onPlayerStop;
    public void playerStop(BoardEntity player)
    {
        //Debug.Log("Player stopped on coaster!");
        Interact();
        if(onPlayerStop != null)
        {
            onPlayerStop(player);
        }
    }

    protected event Action<BoardEntity, Vector3> onPlayerLeave;
    public void playerLeave(BoardEntity entity, Vector3 position)
    {
        //Debug.Log("Player left the coaster!");
        if (onPlayerLeave != null)
        {
            onPlayerLeave(entity, position);
        }
    }

    // Forzar la detención del player.
    public void ForceStop(BoardEntity player)
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
     * Realizar su función.
     * Dar paso a otras casillas.
     * Forzar la detención del player.
     * Almacenar ingredientes.
     * Poner trampas.
     * Activarse o desactivarse.
     */
}