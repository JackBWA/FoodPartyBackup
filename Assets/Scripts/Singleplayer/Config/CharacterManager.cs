using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager singleton;

    public List<GameObject> playableCharacters = new List<GameObject>();

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
        Object[] characters = Resources.LoadAll("Characters/");
        if (characters.Length > 0)
        {
            foreach (Object o in characters)
            {
                GameObject obj = (GameObject)Instantiate(o);
                obj.SetActive(false);
                playableCharacters.Add(obj);
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
                playableCharacters[i].SetActive(true);
            } else
            {
                playableCharacters[i].SetActive(false);
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
}