using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public new string name
    {
        get
        {
            return GetComponentInChildren<NickBillboard>().nickField.text;
        }
        set
        {
            GetComponentInChildren<NickBillboard>().nickField.text = value;
        }
    }

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

    public static string[] aiNames = new string[]
    {
        "Notlaw",
        "delus3r",
        "Nowy",
        "Sans",
        "Feniix",
        "Itzi",
        "Pika",
        "Sea",
        "Beevan",
        "Buckfro",
        "Milasus",
        "Artemisa",
        "Peppy"
    };

    private void LoadDifficulty(AIDifficulty difficulty)
    {
        // Do stuff.
    }
    #endregion
}