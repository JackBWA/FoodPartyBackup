using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardGameManager : MonoBehaviour
{

    //public const int MAX_PLAYERS = 4;

    private int currentTurnIndex = 0;
    public GameObject boardBackup;

    public Dictionary<BoardPlayer, Recipe> recipes = new Dictionary<BoardPlayer, Recipe>();

    #region Private Methods

    private void InitializeEntities()
    {
        entities.ForEach((e) =>
        {
            e.InitializeEntity();
        });
    }

    private void InitializeCoasters()
    {
        Coaster[] coasters = FindObjectsOfType<Coaster>();
        foreach (Coaster c in coasters)
        {
            c.Initialize();
            c.CreateWaitZones();
        }
    }

    private void RandomizeOrder()
    {
        /*
        foreach (BoardEntity a in entities)
        {
            Debug.Log(a);
        }
        */

        List<BoardEntity> aux = new List<BoardEntity>();
        int count = entities.Count;
        //Debug.Log(entities.Count);

        while(entities.Count != 0)
        {
            BoardEntity temp = entities[UnityEngine.Random.Range(0, entities.Count)];
            aux.Add(temp);
            entities.Remove(temp);
            //Debug.Log($"{aux.Count} | {entities.Count}");
        }

        entities = aux;
        /*
        Debug.Log(entities.Count);
        Debug.Log("========================================");
        */

        /*
        foreach (BoardEntity b in entities)
        {
            Debug.Log(b);
        }
        */
    }

    private void TeleportToWaitZones()
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
                List<Vector3> waitZones = Coaster.initialCoaster.GetAvailableWaitZones();
                if (waitZones != null)
                {
                    entity.TeleportTo(Coaster.initialCoaster, waitZones[0]);
                    Coaster.initialCoaster.OccupeWaitZone(entity, waitZones[0]);
                }
                /*
                else
                {
                    entity.TeleportTo(Coaster.initialCoaster);
                }
                */
            }
        }
        entities[0].hasTurn = true;
    }
    #endregion

    #region Singleton
    public static BoardGameManager singleton;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Time.timeScale = 15f; // Para ahorrar tiempo.
        if(singleton != null)
        {
            enabled = false;
            Debug.LogWarning($"Multiple instances of BoardGameManager detected. {name} has been disabled.");
            return;
        }
        singleton = this;

        InitializeEntities();
        InitializeCoasters();
        RandomizeOrder();
        TeleportToWaitZones();
    }

    private void Start()
    {

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
            /*
            SaveState();
            MiniGamesManager.singleton.LoadRandomMinigame();
            */
        } else
        {
            entities[currentTurnIndex].hasTurn = true;
        }
        //==========================
        onTurnEnd?.Invoke(player);
    }
    #endregion

    public void SaveState()
    {
        if (boardBackup == null)
        {
            boardBackup = new GameObject("Board Backup");
            DontDestroyOnLoad(boardBackup);
        }
        GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject sceneObj in objects)
        {
            sceneObj.transform.parent = boardBackup.transform;
        }
        boardBackup.SetActive(false);
        /* No va.
        CreateSceneParameters sceneParams = new CreateSceneParameters(LocalPhysicsMode.None);
        Scene backUpScene = SceneManager.CreateScene("Board State", sceneParams);
        */
    }

    /*
    public void LoadState()
    {
        SceneManager.MoveGameObjectToScene(boardBackup, SceneManager.GetActiveScene());
        boardBackup.SetActive(true);
        foreach (Transform obj in boardBackup.transform)
        {
            obj.parent = null;
        }
        DontDestroyOnLoad(boardBackup);
        boardBackup.SetActive(false);
    }
    */

    /* De momento public */
    public List<BoardEntity> entities = new List<BoardEntity>();
}