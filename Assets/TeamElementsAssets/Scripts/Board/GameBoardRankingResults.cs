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
    private bool localTransform;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach(PositionRotation posRot in podiumPositions)
        {
            PositionRotation auxPosRot = posRot;
            if (localTransform)
            {
                auxPosRot.position += transform.position;
                auxPosRot.rotation += transform.rotation.eulerAngles;
            }
            Gizmos.DrawWireCube(auxPosRot.position, new Vector3(1f, 3f, 1f));
        }
    }

    private void Awake()
    {
        PauseManager.singleton.canToggle = false;
        LoadResults();
    }

    private void LoadResults()
    {
        int i = 1;
        foreach(KeyValuePair<BoardEntity, Recipe> kV in GameBoardManager.singleton.recipeStates.OrderBy(r => r.Value.progress).Reverse())
        {
            PlayerResult pRInstance = Instantiate(playerResultUIPrefab);
            pRInstance.position = i;
            pRInstance.playerName = kV.Key.GetComponent<PlayerCharacter>().name;
            pRInstance.resultScore = kV.Value.progress;
            i++;
            pRInstance.transform.SetParent(scoresListParent);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}