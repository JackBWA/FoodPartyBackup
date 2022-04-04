using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_KingOfTheHill : MiniGame
{

    public Collider stayArea;

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
                    KingOfTheHillAIController aiController = pC.gameObject.AddComponent<KingOfTheHillAIController>();
                    aiController.Initialize();
                    aiController.enabled = false;
                    break;

                case PlayerCharacter.CharacterType.Player:
                    KingOfTheHillPlayerController playerController = pC.gameObject.AddComponent<KingOfTheHillPlayerController>();
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
            KingOfTheHillController controller = pC.GetComponent<KingOfTheHillController>();
            controller.enabled = true;
        }
    }
}
