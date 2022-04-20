using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager singleton;

    public static PlayerCharacter selectedCharacter;
    public static List<PlayerCharacter> aiCharacters = new List<PlayerCharacter>();

    public TMP_InputField nickInput;

    public List<PlayerCharacter> playableCharacters = new List<PlayerCharacter>();
    private List<PlayerCharacter> cachedCharacters = new List<PlayerCharacter>();

    private int index = 0;

    private void Awake()
    {
        #region Singleton
        if (singleton != null)
        {
            Debug.LogWarning($"CharacterManager multiple instances.");
            enabled = false;
            return;
        }
        singleton = this;
        #endregion
        LoadPlayableCharacters();
    }

    private void LoadPlayableCharacters()
    {
        PlayerCharacter[] characters = Resources.LoadAll<PlayerCharacter>("Characters/");
        if (characters.Length > 0)
        {
            foreach (PlayerCharacter pChar in characters)
            {
                PlayerCharacter _character = Instantiate(pChar).GetComponent<PlayerCharacter>();
                _character.GetComponentInChildren<NickBillboard>().gameObject.SetActive(false);
                _character.gameObject.SetActive(false);
                playableCharacters.Add(_character);
                cachedCharacters.Add(pChar);
            }
            UpdateCharacter();
        }
    }

    private void UpdateCharacter()
    {
        for (int i = 0; i < playableCharacters.Count; i++)
        {
            if(i == index)
            {
                playableCharacters[i].gameObject.SetActive(true);
                selectedCharacter = cachedCharacters[i];
            } else
            {
                playableCharacters[i].gameObject.SetActive(false);
            }
        }
    }

    public void PreviousCharacter()
    {
        index--;
        if (index < 0) index = playableCharacters.Count - 1;
        UpdateCharacter();
    }

    public void NextCharacter()
    {
        index++;
        if (index >= playableCharacters.Count) index = 0;
        UpdateCharacter();
    }

    public void StartGame()
    {
        List<string> auxNamesAI = new List<string>();
        foreach(string name in PlayerCharacter.aiNames)
        {
            auxNamesAI.Add(name);
        }

        for(int i = 0; i < playableCharacters.Count; i++)
        {
            if(i != index)
            {
                PlayerCharacter aiChar = cachedCharacters[i];
                string randomName = auxNamesAI[UnityEngine.Random.Range(0, auxNamesAI.Count)];
                aiChar.name = $"BOT {randomName}";
                auxNamesAI.Remove(randomName);
                aiChar.characterType = PlayerCharacter.CharacterType.AI;
                aiCharacters.Add(aiChar);
            } else
            {
                selectedCharacter.name = nickInput.text;
                if(string.IsNullOrEmpty(selectedCharacter.name)) selectedCharacter.name = "Player"; // Temporal.
                selectedCharacter.characterType = PlayerCharacter.CharacterType.Player;
            }
        }
        PauseManager.singleton.canToggle = true;
        SceneManager.LoadScene("Mapa");
    }
}