using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class BoardEntity : MonoBehaviour
{

    public BoardEntityInventory inventory;

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

    public float baseHealth = 50f;
    public float health
    {
        get
        {
            return _health;
        } set
        {
            _health = Mathf.Clamp(value, 0f, baseHealth);
            HealthChange(health);
        }
    }

    private float _health;

    public int coins
    {
        get
        {
            return _coins;
        }

        set
        {
            _coins = value;
        }
    }
    private int _coins;

    public bool isSafe
    {
        get
        {
            return _isSafe;
        }
        set
        {
            _isSafe = value;
        }
    }
    private bool _isSafe;

    protected bool turn;
    protected Dice dice;
    protected int moves;

    public Coaster currentCoaster;

    public bool canToggleCameraView;
    public bool isViewingMap;

    [HideInInspector]
    public CinemachineVirtualCamera thirdPersonCamera;
    [HideInInspector]
    public CinemachineVirtualCamera topCamera;
    public TopViewCameraController topCameraController;

    #region Components
    protected NavMeshAgent agent;
    #endregion

    #region Events
    public event Action onTurnStart;
    public void TurnStart()
    {
        inventory.enabled = true;
        inventory.Create();
        inventory.canUseItem = true;
        onTurnStart?.Invoke();
    }
    
    public event Action onTurnEnd;
    public void TurnEnd()
    {
        inventory.enabled = false;
        inventory.Delete();
        inventory.canUseItem = false;
        GameBoardManager.singleton.TurnEnd(this);
        onTurnEnd?.Invoke();
    }

    public event Action onStartViewMap;
    public void StartViewMap()
    {
        ActivateTC();
        onStartViewMap?.Invoke();
    }

    public event Action onStopViewMap;
    public void StopViewMap()
    {
        DeactivateTC();
        onStopViewMap?.Invoke();
    }

    public event Action<float> onHealthChange;
    public void HealthChange(float health)
    {
        //Debug.Log($"Health changed on {name}.");
        onHealthChange?.Invoke(health);
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

    protected virtual void OnEnable()
    {
        BindEvents();
    }

    protected virtual void OnDisable()
    {
        UnbindEvents();
    }

    public void DisableCanToggleCameraView()
    {
        //Debug.Log("Disable toggle camera view.");
        canToggleCameraView = false;
    }
    
    public void EnableCanToggleCameraView()
    {
        //Debug.Log("Enable toggle camera view.");
        canToggleCameraView = true;
    }

    public void ActivateTPC()
    {
        //Debug.Log("Activate third person camera.");
        isViewingMap = false;
        thirdPersonCamera.enabled = true;
    }

    public void DeactivateTPC()
    {
        //Debug.Log("Deactivate third person camera.");
        thirdPersonCamera.enabled = false;
    }

    public void ActivateTC()
    {
        //Debug.Log("Activate top camera.");
        isViewingMap = true;
        topCamera.enabled = true;
        topCameraController.enabled = true;
        topCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        DeactivateTPC();
    }

    public void DeactivateTC()
    {
        //Debug.Log("Deactivate top camera.");
        isViewingMap = false;
        topCamera.enabled = false;
        topCameraController.enabled = false;
        ActivateTPC();
    }

    public void DeactivateAllCameras()
    {
        //Debug.Log("Deactivate all cameras.");
        isViewingMap = false;
        thirdPersonCamera.enabled = false;
        topCamera.enabled = false;
    }

    public virtual void Initialize()
    {
        if(!TryGetComponent(out agent))
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.speed = 10f;
            agent.acceleration = 40f;
        }
        if(!TryGetComponent(out inventory))
        {
            if(GetComponent<PlayerCharacter>().characterType == PlayerCharacter.CharacterType.Player)
            {
                inventory = gameObject.AddComponent<BoardPlayerInventory>();
            } else
            {
                // For now
                inventory = gameObject.AddComponent<BoardAIInventory>();
            }
        }
        inventory.enabled = false;

        agent.radius = 0.1f;
        health = baseHealth;
        coins = 50000;
        CreateCameras();
        DisableAgent();
    }

    private void CreateCameras()
    {
        CameraBoardManager.CreateEntityCameras(this);
        thirdPersonCamera.enabled = false;
        topCamera.enabled = false;
        topCameraController.enabled = false;
    }

    protected virtual void BindEvents()
    {
        //Debug.Log("Binding events. " + gameObject.name);
        onTurnStart += SpawnDice;
        onTurnStart += ActivateTPC;
        onTurnStart += EnableCanToggleCameraView;
        onThrowDice += DisableCanToggleCameraView;
        onThrowDice += DeactivateTC;
        onTurnEnd += DeactivateAllCameras;
    }

    protected virtual void UnbindEvents()
    {
        //Debug.Log("Unbinding events. " + gameObject.name);
        onTurnStart -= SpawnDice;
        onTurnStart -= ActivateTPC;
        onTurnStart -= EnableCanToggleCameraView;
        onThrowDice -= DisableCanToggleCameraView;
        onThrowDice -= DeactivateTC;
        onTurnEnd -= DeactivateAllCameras;
    }

    public void ForceStop(bool forcedInteract)
    {
        if (!forcedInteract)
        {
            moves = 0;
            ContinueMoving();
        } else
        {
            currentCoaster.Interact(this);
        }
    }

    public void TeleportTo(Vector3 position)
    {
        DisableAgent();
        agent.Warp(position + new Vector3(0f, transform.localScale.y, 0f));
        EnableAgent();
    }

    public void SetMoves(int amount)
    {
        //Debug.Log($"Dice: {amount}");
        moves = amount;
        // Notify
        ContinueMoving();
    }

    public IEnumerator RequestInteract(string title = "Request", string message = "Would you like to interact?", string acceptText = "Accept", string declineText = "Decline")
    {
        /*
        Debug.Log(title);
        Debug.Log(message);
        Debug.Log(acceptText);
        Debug.Log(declineText);
        */

        RequestManager requestManager = Instantiate(Resources.Load<RequestManager>("RequestCanvas"));
        requestManager.title = title;
        requestManager.message = message;
        requestManager.acceptButtonText = acceptText;
        requestManager.declineButtonText = declineText;

        while (!requestManager.hasSubmittedRequest)
        {
            yield return null;
        }

        bool result = requestManager.requestResult;
        Destroy(requestManager.gameObject);

        if (result)
        {
            currentCoaster.Interact(this);
        } else
        {
            ContinueMoving();
        }
        //yield return null;
    }

    public void ContinueMoving()
    {
        moves--;
        if (moves >= 0)
        {
            switch (currentCoaster.next.Count)
            {
                case 0:
                    currentCoaster.playerStop(this);
                    //TurnEnd();
                    break;

                case 1:
                    StartCoroutine(Move(currentCoaster.next[0]));
                    break;

                default:
                    StartCoroutine(Move(currentCoaster.next[UnityEngine.Random.Range(0, currentCoaster.next.Count)]));


                    /* // OnMouseDown() isn't working. Fix needed.
                    if (GetComponent<PlayerCharacter>().characterType == PlayerCharacter.CharacterType.AI)
                    {
                        StartCoroutine(Move(currentCoaster.next[UnityEngine.Random.Range(0, currentCoaster.next.Count)]));
                        break;
                    }
                    CoasterTargetSelector selector = Instantiate(Resources.Load<CoasterTargetSelector>("CoasterTargetSelector"));
                    selector.interactor = this;
                    selector.CreateSelectors();
                    */
                    break;
            }


            // Old.
            //StartCoroutine(Move(currentCoaster.next[0]));
        }
        else
        {
            TurnEnd();
            //currentCoaster.playerStop(this);
        }
    }

    public IEnumerator Move(Coaster target, float checkRate = 0.25f, float distanceRadius = 0.2f) // Or Vector3 targetPosition
    {
        //Debug.Log(target);
        currentCoaster.playerLeave(this);
        if (target != null)
        {
            // Aqui peta al ir a la initial.
            List<Vector3> waitZones = target.GetAvailableWaitZones();
            if(waitZones != null && waitZones.Count > 0)
            {
                //Debug.Log(waitZones[0]);
                agent.SetDestination(waitZones[0]);
            } else
            {
                // If this triggers there's an error. (99% sure).
                TurnEnd();
            }

            while(Vector3.Distance(transform.position, waitZones[0]) > distanceRadius)
            {
                //Debug.Log(Vector3.Distance(transform.position, waitZones[0]));
                yield return new WaitForSeconds(checkRate);
            }

            currentCoaster = target;
            currentCoaster.playerEnter(this, waitZones[0]);
        }
    }

    protected void SpawnDice()
    {
        dice = Instantiate(
            ((GameObject)Resources.Load("Dice")).GetComponent<Dice>());
        dice.transform.position = transform.position + Vector3.up * 5f;
        int randomAxis = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObjectRotator.RotationAxis)).Length);
        dice.GetComponent<ObjectRotator>().rotationAxis = (ObjectRotator.RotationAxis) randomAxis;
        dice.canThrow = true;
        dice.owner = this;
    }

    public void SetCanThrowDice(bool canThrow)
    {
        dice.canThrow = canThrow;
    }

    public event Action onThrowDice;
    public void ThrowDice()
    {
        /*
        //Cambiar el spawn del dado a cuando es el turno del jugador.
        Dice dice = Instantiate(
            ((GameObject)Resources.Load("Dice")).GetComponent<Dice>(),
            transform.position + Vector3.up * 5, Quaternion.identity);
        dice.owner = this;
        */
        if (!hasTurn || dice == null || dice.used || !dice.canThrow)
        {
            return;
        }

        inventory.isUsingItem = false;
        inventory.canUseItem = false;

        ObjectRotator objRot;
        if (dice.TryGetComponent(out objRot))
        {
            objRot.enabled = false;
        }
        dice.Throw();

        onThrowDice?.Invoke();
    }

    public void ToggleMapView()
    {
        /*
        Debug.Log("Can toggle camera view? " + canToggleCameraView);
        Debug.Log("Has turn? " + hasTurn);
        Debug.Log("Is viewing map? " + isViewingMap);
        */
        if (canToggleCameraView && hasTurn)
        {
            isViewingMap = !isViewingMap;
            if (isViewingMap)
            {
                StartViewMap();
            }
            else
            {
                StopViewMap();
            }
        }
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