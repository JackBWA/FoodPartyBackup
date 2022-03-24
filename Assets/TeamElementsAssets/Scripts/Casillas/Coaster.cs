using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
public class Coaster : MonoBehaviour
{
    public List<Coaster> next = new List<Coaster>();
    public List<BoardEntity> players = new List<BoardEntity>();

    public static Coaster initialCoaster;

    public List<Vector3> waitZones = new List<Vector3>();
    private Dictionary<Vector3, BoardEntity> waitZonesState = new Dictionary<Vector3, BoardEntity>();

    //public int coasterId { get; private set; } // Not using anymore wtf lol.

    public bool isInitial
    {
        get
        {
            return type == CoasterType.Initial;
        }
    }

    public enum CoasterType
    {
        Normal,
        Initial,
        Finish,
        Safe,
        Teleport,
        Shop,
        Bonus,
        Trap
    }

    public CoasterType type;

    public bool canRequestInteract;
    public bool keepMovesOnRequestAccept;

    private bool canForceStop = false;

    #region Awake/Start/Update
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }
    #endregion

    public void Initialize()
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
        CreateWaitZones(GameManager.maxPlayers);
        //GameObjectUtility.SetStaticEditorFlags(gameObject, StaticEditorFlags.NavigationStatic);
        NavMeshSurface nms = gameObject.AddComponent<NavMeshSurface>();
        nms.collectObjects = CollectObjects.Children;
    }

    // Realizar su función.
    public virtual void Interact(BoardEntity interactor)
    {
        Debug.Log("Base interact!");
    }

    public virtual void EndInteract(BoardEntity interactor)
    {
        Debug.Log("Base end interact!");
        if (!keepMovesOnRequestAccept)
        {
            interactor.TurnEnd();
        } else
        {
            interactor.ContinueMoving();
        }
    }

    private void CreateWaitZones(int amount)
    {
        int subdivisionAngle = 360 / amount;
        for(int i = 0; i < amount; i++)
        {
            GameObject waitZone = new GameObject("Wait Zone");
            waitZone.transform.position = transform.position;
            waitZone.transform.eulerAngles = new Vector3(0f, (i + 1) * subdivisionAngle, 0f);
            waitZone.transform.position += waitZone.transform.forward.normalized * (transform.localScale.magnitude / 4f);
            waitZone.transform.parent = transform;
            waitZones.Add(waitZone.transform.position);
            waitZonesState.Add(waitZone.transform.position, null);
        }
    }

    public List<Vector3> GetAvailableWaitZones()
    {
        //Debug.Log("===========START FINDING WAIT ZONES============");
        List<Vector3> result = new List<Vector3>();
        foreach(Vector3 waitZone in waitZones)
        {
            if(waitZonesState.ContainsKey(waitZone) && waitZonesState[waitZone] == null)
            {
                result.Add(waitZone);
            }
        }
        /*
        Debug.Log("Final result: Found " + result.Count);
        Debug.Log("===========END FINDING WAIT ZONES============");
        */
        return result;
    }

    public void SetWaitZoneState(Vector3 waitZone, BoardEntity entity)
    {
        //Debug.Log("Requested zone to update: " + waitZone);
        //Debug.Log("Entity value as parameter: " + entity);
        //Debug.Log("Is wait zone on the dictionary? " + waitZonesState.ContainsKey(waitZone));
        //Debug.Log("List of all value in the dictionary:");
        foreach(KeyValuePair<Vector3, BoardEntity> kp in waitZonesState)
        {
            //Debug.Log("Key: " + kp.Key + " | Value: " + kp.Value);
        }
        waitZonesState[waitZone] = entity;
        //Debug.Log("Is wait zone still on the dictionary? " + waitZonesState.ContainsKey(waitZone));
    }

    // Movimiento en casillas.
    protected event Action<BoardEntity, Vector3> onPlayerEnter;
    public virtual void playerEnter(BoardEntity entity, Vector3 position)
    {
        //Debug.Log("Player entered the coaster!");
        SetWaitZoneState(position, entity);

        /*
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         * CANFORCESTOP=FALSE ES TEMPORAL GRACIAS POR LEER.
         */
        canForceStop = false;

        if (canForceStop)
        {
            entity.ForceStop();
            return;
        }

        if (canRequestInteract)
        {
            Debug.Log("Requesting interact.");
            RequestInteract(entity);
            //StartCoroutine(entity.RequestInteract());
        } else
        {
            entity.ContinueMoving();
        }
        onPlayerEnter?.Invoke(entity, position);
    }

    protected virtual void RequestInteract(BoardEntity interactor, string title = "Request", string message = "Message", string acceptText = "Accept", string declineText = "Decline")
    {
        Debug.Log("Coaster request interact!");

        if (string.IsNullOrEmpty(title)) title = "Request";
        if (string.IsNullOrEmpty(message)) message = "Would you like to interact?";
        if (string.IsNullOrEmpty(acceptText)) acceptText = "Accept";
        if (string.IsNullOrEmpty(declineText)) declineText = "Decline";

        StartCoroutine(interactor.RequestInteract(title, message, acceptText, declineText));
    }

    protected event Action<BoardEntity> onPlayerStop;
    public virtual void playerStop(BoardEntity entity)
    {
        //Debug.Log("Player stopped on coaster!");
        Interact(entity);
        onPlayerStop?.Invoke(entity);
    }

    protected event Action<BoardEntity> onPlayerLeave;
    public virtual void playerLeave(BoardEntity entity)
    {
        //Debug.Log("Player left the coaster!");

        int i = 0;
        while(i < waitZonesState.Count)
        {
            if(waitZonesState[waitZones[i]] == entity)
            {
                waitZonesState[waitZones[i]] = null;
                i = waitZonesState.Count;
            } else
            {
                i++;
            }
        }

        /*
        foreach(KeyValuePair<Vector3, BoardEntity> kP in waitZonesState)
        {
            if(kP.Value == entity)
            {
                waitZonesState[kP.Key] = null;
            }
        }
        */

        //SetWaitZoneState(position, null);
        onPlayerLeave?.Invoke(entity);
    }

    // Forzar la detención del player.
    public void ForceStop(BoardEntity player)
    {
        player.ForceStop();
    }

    /* (DON'T USE FOR NOW)
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
    */

    /* Que cosas puede hacer una casilla:
     * 
     * Realizar su función.
     * Dar paso a otras casillas.
     * Forzar la detención del player.
     * Almacenar ingredientes.
     * Poner trampas.
     * Activarse o desactivarse. (De momento no).
     */
}