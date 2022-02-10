using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : MonoBehaviour
{

    public int movesLeft { get; private set; }

    public void ForceStop()
    {
        movesLeft = 0;
    }
}
