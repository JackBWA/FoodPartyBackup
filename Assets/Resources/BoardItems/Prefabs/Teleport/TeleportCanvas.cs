using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCanvas : MonoBehaviour
{

    public Teleport item;

    List<BoardEntity> targets = new List<BoardEntity>();

    int index = 0;

    public void SetTargets(BoardEntity owner)
    {
        foreach (BoardEntity bE in GameBoardManager.singleton.boardPlayers)
        {
            if (bE != owner) targets.Add(bE);
        }
        index = 0;
        item.target = targets[index];
        UpdateCamera();
    }

    public void Previous()
    {
        index++;
        if (index >= targets.Count) index = 0;
        item.target = targets[index];
        UpdateCamera();
    }

    public void Next()
    {
        index--;
        if (index < 0) index = targets.Count - 1;
        item.target = targets[index];
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        foreach(BoardEntity bE in targets)
        {
            if (bE == item.target)
            {
                bE.ActivateTPC();
            } else
            {
                bE.DeactivateTPC();
            }
        }
    }

    public void Teleport()
    {
        targets[targets.IndexOf(item.target)].DeactivateTPC();
        item.Use();
    }
}