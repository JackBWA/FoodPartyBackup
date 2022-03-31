using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    public Transform scoresParentUI;

    public PlayerScore playerScoreUIPrefab;
    public List<PlayerScore> scoresUI = new List<PlayerScore>();

    private void CreateUI()
    {
        foreach (PlayerCharacter pC in MiniGame.singleton.players)
        {
            PlayerScore instance = Instantiate(playerScoreUIPrefab);
            instance.characterReference = pC;
            // Set icon lol.
            instance.transform.SetParent(scoresParentUI);
            scoresUI.Add(instance);
        }
    }

    private void OnEnable()
    {
        if(scoresUI == null || scoresUI.Count == 0)
        {
            CreateUI();
        }
    }

    private void OnDisable()
    {
        
    }
}
