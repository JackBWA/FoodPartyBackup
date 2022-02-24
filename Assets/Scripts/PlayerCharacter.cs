using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public new string name;

    public enum CharacterType
    {
        Player,
        AI
    }
    public CharacterType characterType;

    // Probablemente no se use de forma compleja.
    #region I.A Difficulty
    public enum AIDifficulty
    {
        Easy,
        Normal,
        Hard
    }
    public AIDifficulty aiDifficulty;

    private void LoadDifficulty(AIDifficulty difficulty)
    {
        // Do stuff.
    }
    #endregion


}
