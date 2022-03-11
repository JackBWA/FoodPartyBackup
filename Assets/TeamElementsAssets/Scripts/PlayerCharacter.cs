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
    #region AI Variables
    public enum AIDifficulty
    {
        Easy,
        Normal,
        Hard
    }
    public AIDifficulty aiDifficulty;

    [HideInInspector]
    public static string[] aiNames = new string[]
    {
        "Notlaw",
        "delus3r",
        "Nowy",
        "kazamabc",
        "V I C E N T E",
        "-Hideki-",
        "G U R A S I M P",
        "Frosiito",
        "B of yassin",
        "NHarmonia",
        "Estopa",
        "Fichi",
        "Damn DanyL"
    };

    private void LoadDifficulty(AIDifficulty difficulty)
    {
        // Do stuff.
    }
    #endregion
}