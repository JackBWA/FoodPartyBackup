using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coaster : MonoBehaviour
{
    public List<Coaster> next = new List<Coaster>();
    public List<BoardEntity> players = new List<BoardEntity>();
    public GameObject[] waitZones;

    private Dictionary<Vector3, BoardEntity> occupiedWaitZones = new Dictionary<Vector3, BoardEntity>();

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
        Shop,
        Bonus,
        Trap_STILL_THINKIN_PENSIVE
    }

    public CoasterType type;

    public bool canRequestStop;
    private bool canForceStop;

    #region Awake/Start/Update
    protected virtual void Awake()
    {
        /*
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
        CreateWaitZones();
        */
    }

    protected virtual void Start()
    {

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
        CreateWaitZones(waitZonesAmount);
    }

    private void CreateWaitZones(int amount)
    {
        waitZones = new GameObject[amount/*BoardGameManager.singleton.entities.Count*/];
        int subdivisionAngle = 360 / waitZones.Length;
        for(int i = 0; i < waitZones.Length; i++)
        {
            GameObject waitZone = new GameObject("Wait Zone");
            waitZone.transform.position = transform.position;
            waitZone.transform.eulerAngles = new Vector3(0f, (i + 1) * subdivisionAngle, 0f);
            waitZone.transform.position += waitZone.transform.forward.normalized * (transform.localScale.magnitude / 2.5f);
            waitZone.transform.parent = transform;
            waitZones[i] = waitZone;
            occupiedWaitZones.Add(waitZone.transform.position, null);
        }
    }

    public List<Vector3> GetAvailableWaitZones()
    {
        List<Vector3> result = null;

        foreach (KeyValuePair<Vector3, BoardEntity> kv in occupiedWaitZones)
        {
            if(kv.Value == null)
            {
                if (result == null) result = new List<Vector3>();
                result.Add(kv.Key);
            }
        }
        return result;
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
        OccupeWaitZone(player, position);
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
        DisoccupeWaitZone(position);
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

    public void OccupeWaitZone(BoardEntity entity, Vector3 position)
    {
        if (occupiedWaitZones.ContainsKey(position)) occupiedWaitZones.Remove(position);
        occupiedWaitZones.Add(position, entity);
        /*
        if (occupiedWaitZones.ContainsKey(position) || occupiedWaitZones.ContainsValue(entity)) return false;
        else
        {
            occupiedWaitZones.Add(position, entity);
            return true;
        }
        */
    }

    public void DisoccupeWaitZone(Vector3 position)
    {
        if (occupiedWaitZones.ContainsKey(position)) occupiedWaitZones.Remove(position);
        occupiedWaitZones.Add(position, null);
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