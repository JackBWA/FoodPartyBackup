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

    public Sprite avatar;

    public enum CharacterType
    {
        Player,
        AI
    }
    public CharacterType characterType;

    public Transform trackPoint;

    public CharacterAnimationManager animManager;

    private void Awake()
    {
        if (animManager == null) animManager = GetComponentInChildren<CharacterAnimationManager>();
    }

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