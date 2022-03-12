using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarsUIManager : MonoBehaviour
{
    public GameObject elementPrefab;

    public List<HealthBarUI> healthBars = new List<HealthBarUI>();

    private void Start()
    {
        CreateHealthBars();
    }

    private void CreateHealthBars()
    {
        foreach(BoardEntity bE in GameBoardManager.singleton.boardPlayers)
        {
            GameObject hbObj = Instantiate(elementPrefab);
            hbObj.transform.parent = transform;
            HealthBarUI healthBar = hbObj.GetComponent<HealthBarUI>();
            healthBar.Initialize(bE);
            healthBars.Add(healthBar);
        }
    }
}
