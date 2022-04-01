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

    public int pointsPerRound = 50;

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

    public void TriggerStove()
    {
        Stove stove = stoves[Random.Range(0, stoves.Count)];
        stove.Trigger();

        #region Finish Check
        if (((MiniGame_DontGetBurnt)MiniGame.singleton).playersLeft.Count <= 1)
        {
            MinigameFinish();
            return;
        }
        #endregion

        foreach(PlayerCharacter pC in playersLeft)
        {
            playerScores[pC] += pointsPerRound;
        }
    }

    public void RemovePlayer(PlayerCharacter pC)
    {
        playersLeft.Remove(pC);
        pC.enabled = false;
        pC.gameObject.SetActive(false);
    }
}