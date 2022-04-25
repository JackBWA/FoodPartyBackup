using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_DontGetBurnt : MiniGame
{
    [HideInInspector]
    public List<PlayerCharacter> playersLeft;

    public List<Stove> stoves = new List<Stove>();
    public float stoveTriggerDelay = 10f;
    private float timer = 0f;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        playersLeft = new List<PlayerCharacter>();
        foreach(PlayerCharacter pC in players)
        {
            playersLeft.Add(pC);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

    }
    #endregion

    protected override void InitializePlayers()
    {
        base.InitializePlayers();
        foreach(PlayerCharacter pC in players)
        {
            switch (pC.characterType)
            {
                case PlayerCharacter.CharacterType.AI:
                    DontGetBurntAIController aiController = pC.gameObject.AddComponent<DontGetBurntAIController>();
                    aiController.Initialize();
                    aiController.enabled = false;
                    break;

                case PlayerCharacter.CharacterType.Player:
                    DontGetBurntPlayerController playerController = pC.gameObject.AddComponent<DontGetBurntPlayerController>();
                    playerController.Initialize();
                    playerController.enabled = false;
                    break;
            }
        }
    }

    protected override void SpawnPlayers()
    {
        base.SpawnPlayers();
        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].characterType == PlayerCharacter.CharacterType.AI)
            {
                DontGetBurntAIController aiController = players[i].GetComponent<DontGetBurntAIController>();
                aiController.TeleportTo(aiController.transform.position);
            }
            /*
            players[i].transform.position = spawnZones[Mathf.Clamp(i, 0, spawnZones.Count - 1)].position;
            players[i].transform.rotation = Quaternion.Euler(spawnZones[Mathf.Clamp(i, 0, spawnZones.Count - 1)].rotation);
            */
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        onMinigameStart += EnableControllers;
        onMinigameStart += StartStoves;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onMinigameStart -= EnableControllers;
        onMinigameStart -= StartStoves;
    }

    public override void MinigameStart()
    {
        base.MinigameStart();
        foreach (PlayerCharacter pC in players)
        {
            DontGetBurntAIController aiCutreXd = pC.GetComponent<DontGetBurntAIController>();
            if(aiCutreXd != null)
            {
                aiCutreXd.SwapStove();
            }
        }
    }

    public void EnableControllers()
    {
        foreach(PlayerCharacter pC in players)
        {
            DontGetBurntController controller = pC.GetComponent<DontGetBurntController>();
            controller.enabled = true;
        }
    }

    public void StartStoves()
    {
        StartCoroutine(StovesCo());
    }

    private IEnumerator StovesCo()
    {
        timer = 0f;
        while(miniGameState != MinigameState.FINISHED)
        {
            timer += Time.deltaTime;
            if(timer >= stoveTriggerDelay)
            {
                timer = 0f;
                TriggerStove();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public event Action onTriggerStove;
    public void TriggerStove()
    {
        onTriggerStove?.Invoke();

        Stove stove = stoves[UnityEngine.Random.Range(0, stoves.Count)];
        stove.Trigger();

        foreach (PlayerCharacter pC in playersLeft)
        {
            AddScore(pC);
        }

        UpdateScores();

        #region Finish Check
        if (((MiniGame_DontGetBurnt)MiniGame.singleton).playersLeft.Count <= 1)
        {
            StopCoroutine(timerCo);
            MinigameFinish();
            return;
        }
        #endregion
    }

    public void RemovePlayer(PlayerCharacter pC)
    {
        playersLeft.Remove(pC);
        pC.enabled = false;
        pC.gameObject.SetActive(false);
    }
}