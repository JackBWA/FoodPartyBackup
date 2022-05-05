using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoardRankingResults : MonoBehaviour
{
    public PlayerResult playerResultUIPrefab;

    public string winnerText
    {
        get
        {
            return _winnerText.text;
        }
        set
        {
            _winnerText.text = value;
        }
    }

    public Transform scoresListParent
    {
        get
        {
            return _scoresListParent;
        }
        set
        {
            _scoresListParent = value;
        }
    }

    [SerializeField]
    private TextMeshProUGUI _winnerText;
    [SerializeField]
    private Transform _scoresListParent;

    [SerializeField]
    private List<PositionRotation> podiumPositions = new List<PositionRotation>();

    [SerializeField]
    private bool debug;

    [SerializeField]
    private bool localTransform;

    public List<BoardEntity> players = new List<BoardEntity>();

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Gizmos.color = Color.magenta;
        foreach(PositionRotation posRot in podiumPositions)
        {
            PositionRotation auxPosRot = posRot;
            if (localTransform)
            {
                auxPosRot.position += transform.position;
                auxPosRot.rotation += transform.rotation.eulerAngles;
            }
            Gizmos.DrawWireCube(auxPosRot.position, new Vector3(1f, 1f, 1f));
        }
    }

    private void Awake()
    {
        PauseManager.singleton.canToggle = false;
        LoadResults();
    }

    private void LoadResults()
    {
        players = GameBoardManager.singleton.boardPlayers;
        int i = 0;
        foreach(KeyValuePair<BoardEntity, Recipe> kV in GameBoardManager.singleton.recipeStates.OrderBy(r => r.Value.progress).Reverse())
        {
            PlayerResult pRInstance = Instantiate(playerResultUIPrefab);
            pRInstance.position = i + 1;
            pRInstance.playerName = kV.Key.GetComponent<PlayerCharacter>().name;
            pRInstance.resultScore = kV.Value.progress;
            pRInstance.transform.SetParent(scoresListParent);

            if (i == 0)
            {
                winnerText = $"{pRInstance.playerName} es el ganador!";
            }

            players[players.IndexOf(kV.Key)].gameObject.transform.position = podiumPositions[i].position;

            players[players.IndexOf(kV.Key)].gameObject.transform.rotation = Quaternion.Euler(podiumPositions[i].rotation);
            i++;
        }
    }

    public void ExitGame()
    {
        Destroy(GameBoardManager.singleton.persistentBoardObjects);
        Destroy(CameraBoardManager.singleton.gameObject);
        Destroy(GameBoardManager.singleton.gameObject);
        CharacterManager.aiCharacters.Clear();
        //CharacterManager.selectedCharacter = null; // I don't know xd.
        SceneManager.LoadSceneAsync("MainMenu");
    }
}