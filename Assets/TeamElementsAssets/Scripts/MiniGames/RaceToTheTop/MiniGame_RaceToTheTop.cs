using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_RaceToTheTop : MiniGame
{
    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
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
        foreach (PlayerCharacter pC in players)
        {
            switch (pC.characterType)
            {
                case PlayerCharacter.CharacterType.AI:
                    RaceToTheTopAIController aiController = pC.gameObject.AddComponent<RaceToTheTopAIController>();
                    aiController.Initialize();
                    aiController.enabled = false;
                    break;

                case PlayerCharacter.CharacterType.Player:
                    RaceToTheTopPlayerController playerController = pC.gameObject.AddComponent<RaceToTheTopPlayerController>();
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
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onMinigameStart -= EnableControllers;
    }

    public void EnableControllers()
    {
        foreach (PlayerCharacter pC in players)
        {
            RaceToTheTopController controller = pC.GetComponent<RaceToTheTopController>();
            controller.enabled = true;
        }
    }
}