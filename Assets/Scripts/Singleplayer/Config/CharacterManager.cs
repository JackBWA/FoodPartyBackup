using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager singleton;

    public static PlayerCharacter selectedCharacter;

    public List<PlayerCharacter> playableCharacters = new List<PlayerCharacter>();

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
                _character.gameObject.SetActive(false);
                playableCharacters.Add(_character);
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
                selectedCharacter = playableCharacters[i];
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

    private void Update()
    {
        Debug.Log(selectedCharacter.name);
    }
}