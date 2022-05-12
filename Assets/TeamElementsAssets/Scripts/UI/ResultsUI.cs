using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ResultsUI : MonoBehaviour
{

    public Sprite coinRewardIcon;

    public List<Reward> rewards = new List<Reward>();

    public PlayerResult playerResultUIPrefab;

    public RewardResult playerRewardUIPrefab;

    public float resultsDisplayTime = 10f;

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

    public Transform rewardsListParent
    {
        get
        {
            return _rewardsListParent;
        }
        set
        {
            _rewardsListParent = value;
        }
    }

    public string leavingText
    {
        get
        {
            return _leavingText.text;
        }
        set
        {
            _leavingText.text = value;
        }
    }

    [SerializeField]
    private TextMeshProUGUI _winnerText;
    [SerializeField]
    private Transform _scoresListParent;
    [SerializeField]
    private Transform _rewardsListParent;
    [SerializeField]
    private TextMeshProUGUI _leavingText;

    private void LoadResults()
    {
        Dictionary<PlayerCharacter, int> sortedResults = new Dictionary<PlayerCharacter, int>();
        foreach (KeyValuePair<PlayerCharacter, int> kV in MiniGame.singleton.playerScores.OrderByDescending(ctx => ctx.Value))
        {
            sortedResults.Add(kV.Key, kV.Value);
        }

        winnerText = $"{sortedResults.ElementAt(0).Key.name} es el ganador!";

        int i = 1;
        foreach (KeyValuePair<PlayerCharacter, int> kV in MiniGame.singleton.playerScores.OrderByDescending(ctx => ctx.Value))
        {
            PlayerResult instance = Instantiate(playerResultUIPrefab);
            instance.position = i;
            instance.icon = kV.Key.avatar;
            instance.playerName = kV.Key.name;
            instance.resultScore = kV.Value;

            instance.transform.SetParent(scoresListParent);

            BoardEntity bE = GameBoardManager.singleton.saveLoadGameObjectsParent.GetComponentsInChildren<PlayerCharacter>().First(playerChar => playerChar.name == kV.Key.name).GetComponent<BoardEntity>();

            Reward reward = rewards[i - 1];

            reward.GetReward(bE);

            RewardResult rewardsDisplay = Instantiate(playerRewardUIPrefab);
            rewardsDisplay.transform.SetParent(rewardsListParent);

            RewardUIElement coinsRewardUI = Instantiate(rewardsDisplay.prefab);
            coinsRewardUI.amount = reward.coinsAmount;
            coinsRewardUI.icon = coinRewardIcon;
            coinsRewardUI.transform.SetParent(rewardsDisplay.contentParent);

            foreach(KeyValuePair<Ingredient, int> iV in reward.obtainedIngredients)
            {
                RewardUIElement ingredientsRewardUI = Instantiate(rewardsDisplay.prefab);
                ingredientsRewardUI.amount = iV.Value;
                ingredientsRewardUI.icon = iV.Key.icon;
                ingredientsRewardUI.transform.SetParent(rewardsDisplay.contentParent);
            }

            foreach (KeyValuePair<BoardItem_Base, int> iV in reward.obtainedItems)
            {
                RewardUIElement itemsRewardUI = Instantiate(rewardsDisplay.prefab);
                itemsRewardUI.amount = iV.Value;
                itemsRewardUI.icon = iV.Key.icon;
                itemsRewardUI.transform.SetParent(rewardsDisplay.contentParent);
            }

            i++;
        }
        StartCoroutine(ExitCountdown());
    }
    
    private IEnumerator ExitCountdown()
    {
        float timeLeft = resultsDisplayTime;
        while(timeLeft >= 0f)
        {
            timeLeft -= Time.deltaTime;
            leavingText = $"Volverás al tablero en {((int) timeLeft) + 1} segundos...";
            yield return new WaitForEndOfFrame();
        }
        MiniGame.singleton.MinigameExit();
    }

    private void OnEnable()
    {
        LoadResults();
    }

    private void OnDisable()
    {
        
    }
}