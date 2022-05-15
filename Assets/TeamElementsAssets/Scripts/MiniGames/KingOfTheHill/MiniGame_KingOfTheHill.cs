using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_KingOfTheHill : MiniGame
{

    public Collider stayArea;

    public KingOfTheHillDeathBox deathBox;

    public Vector3 deathBoxPosition;
    public Vector3 deathBoxScale;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
        KingOfTheHillDeathBox dBox = Instantiate(deathBox);
        dBox.transform.position = deathBoxPosition;
        foreach(PlayerCharacter pC in players)
        {
            dBox.startPositions.Add(pC, pC.transform.position);
        }
        BoxCollider bC = dBox.gameObject.AddComponent<BoxCollider>();
        bC.size = deathBoxScale;
        bC.isTrigger = true;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(deathBoxPosition, deathBoxScale);
    }

    public override void MinigameEnter()
    {
        base.MinigameEnter();
        SoundManager.singleton.PlayWithFade("KingOfTheHillMusic");
    }

    public override void MinigameStart()
    {
        base.MinigameStart();
        Cursor.visible = true;
    }

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

    protected override void SpawnPlayers()
    {
        base.SpawnPlayers();
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].characterType == PlayerCharacter.CharacterType.AI)
            {
                KingOfTheHillAIController aiController = players[i].GetComponent<KingOfTheHillAIController>();
                aiController.TeleportTo(aiController.transform.position);
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