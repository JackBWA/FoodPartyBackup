using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillDeathBox : MonoBehaviour
{

    public Dictionary<PlayerCharacter, Vector3> startPositions = new Dictionary<PlayerCharacter, Vector3>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCharacter pC = other.GetComponent<PlayerCharacter>();
            if (!startPositions.ContainsKey(pC)) return;
            KingOfTheHillController controller = pC.gameObject.GetComponent<KingOfTheHillController>();
            switch (controller)
            {
                case KingOfTheHillAIController ai:
                    ai.agent.Warp(startPositions[pC] + Vector3.up);
                    ai.agent.enabled = false;
                    ai.agent.enabled = true;
                    break;

                case KingOfTheHillPlayerController player:
                    player.gameObject.transform.position = startPositions[pC] + Vector3.up;
                    break;
            }
        }
    }
}
