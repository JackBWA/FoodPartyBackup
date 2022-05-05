using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using System;
using System.Linq;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager singleton;

    public static PlayerCharacter selectedCharacter;
    public static List<PlayerCharacter> aiCharacters = new List<PlayerCharacter>();

    public TMP_InputField nickInput;

    public List<PlayerCharacter> playableCharacters = new List<PlayerCharacter>();
    private List<PlayerCharacter> cachedCharacters = new List<PlayerCharacter>();

    private int index = 0;

    private Dictionary<PlayerCharacter, CinemachineVirtualCamera> camRefs = new Dictionary<PlayerCharacter, CinemachineVirtualCamera>();

    public List<Vector3> spawnPositions = new List<Vector3>();

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i < spawnPositions.Count; i++)
        {
            Gizmos.DrawWireSphere(transform.position + spawnPositions[i], 1f);
        }
    }
    */

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
        //LoadPlayableCharacters();
    }

    List<CharacterExpression.CharExpression> characterExpressions = new List<CharacterExpression.CharExpression>();
    private void Start()
    {
        foreach(string s in Enum.GetNames(typeof(CharacterExpression.CharExpression)))
        {
            CharacterExpression.CharExpression cE = (CharacterExpression.CharExpression) Enum.Parse(typeof(CharacterExpression.CharExpression), s);
            if(Enum.IsDefined(typeof(CharacterExpression.CharExpression), cE))
            {
                characterExpressions.Add(cE);
            }
        }
    }

    public Coroutine faceChangeCo;
    private IEnumerator FaceIndexCo()
    {
        int faceIndex = 0;
        while (true)
        {
            if (faceIndex >= characterExpressions.Count) faceIndex = 0;
            if (playableCharacters != null /*&& playableCharacters.Count > 0 */&& index < playableCharacters.Count) playableCharacters[index].animManager.currentFaceState = characterExpressions[faceIndex];
            faceIndex++;
            yield return new WaitForSeconds(1f);
            /*
            List<string> faceExpressions = Enum.GetNames(typeof(CharacterExpression.CharExpression)).ToList();
            faceExpressions.Remove(playableCharacters[index].animManager.currentFaceState.ToString());
            playableCharacters[index].animManager.currentFaceState = (CharacterExpression.CharExpression) Enum.Parse(typeof(CharacterExpression.CharExpression), faceExpressions[(UnityEngine.Random.Range(0, faceExpressions.Count))]);
            */
        }
    }

    public void LoadPlayableCharacters()
    {
        PlayerCharacter[] characters = Resources.LoadAll<PlayerCharacter>("Characters/");
        CinemachineVirtualCamera cameraRef = Resources.Load<CinemachineVirtualCamera>("Cameras/FrontalVirtualCamera");
        if (characters.Length > 0)
        {
            for(int i = 0; i < characters.Length; i++)
            {
                PlayerCharacter pChar = characters[i];
                PlayerCharacter _character = Instantiate(pChar).GetComponent<PlayerCharacter>();
                _character.GetComponentInChildren<NickBillboard>().gameObject.SetActive(false);
                //_character.gameObject.SetActive(false);
                _character.transform.position = transform.position + spawnPositions[i];
                playableCharacters.Add(_character);
                cachedCharacters.Add(pChar);
                CinemachineVirtualCamera frontalCam = Instantiate(cameraRef);
                frontalCam.Follow = _character.trackPoint;
                frontalCam.LookAt = _character.trackPoint;
                camRefs.Add(_character, frontalCam);
            }
        }
        faceChangeCo = StartCoroutine(FaceIndexCo());
    }

    public void UnloadCharacterManager()
    {
        for(int i = 0; i < playableCharacters.Count; i++)
        {
            Destroy(camRefs[playableCharacters[i]].gameObject);
            Destroy(playableCharacters[i].gameObject);
        }
        cachedCharacters.Clear();
        playableCharacters.Clear();
        camRefs.Clear();
        selectedCharacter = null;
        StopCoroutine(faceChangeCo);
    }

    public void UpdateCharacter()
    {
        for (int i = 0; i < playableCharacters.Count; i++)
        {
            if(i == index)
            {
                camRefs[playableCharacters[i]].enabled = true;
                selectedCharacter = cachedCharacters[i];
                /*
                playableCharacters[i].gameObject.SetActive(true);
                selectedCharacter = cachedCharacters[i];
                */
            } else
            {
                camRefs[playableCharacters[i]].enabled = false;
                playableCharacters[i].animManager.defaultFaceState = CharacterExpression.CharExpression.IDLE;
                /*
                playableCharacters[i].gameObject.SetActive(false);
                */
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