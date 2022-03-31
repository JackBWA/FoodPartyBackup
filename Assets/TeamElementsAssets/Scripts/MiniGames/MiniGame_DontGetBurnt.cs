using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_DontGetBurnt : MiniGame
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
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onMinigameStart -= EnableControllers;
    }

    public void EnableControllers()
    {
        foreach(PlayerCharacter pC in players)
        {
            DontGetBurntController controller = pC.GetComponent<DontGetBurntController>();
            controller.enabled = true;
        }
    }
}