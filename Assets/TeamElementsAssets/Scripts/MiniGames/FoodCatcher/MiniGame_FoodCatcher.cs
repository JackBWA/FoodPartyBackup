using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_FoodCatcher : MiniGame
{

    public List<PointCollect> foodPrefabs = new List<PointCollect>();

    public float spawnRate = 0.33f;

    // Tabla de indicios de spawnrate;

    public Vector3 areaPosition;
    public Vector3 areaSize;

    private bool canSpawnFood = true;

    private Coroutine foodThrowCo;

    #region Awake/Start/Update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255f/255f, 127f/255f, 1f/255f, 255f/255f);
        Gizmos.DrawWireCube(areaPosition, areaSize);
    }

    protected override void InitializePlayers()
    {
        base.InitializePlayers();
        foreach (PlayerCharacter pC in players)
        {
            switch (pC.characterType)
            {
                case PlayerCharacter.CharacterType.AI:
                    FoodCatcherAIController aiController = pC.gameObject.AddComponent<FoodCatcherAIController>();
                    aiController.Initialize();
                    aiController.enabled = false;
                    break;

                case PlayerCharacter.CharacterType.Player:
                    FoodCatcherPlayerController playerController = pC.gameObject.AddComponent<FoodCatcherPlayerController>();
                    playerController.Initialize();
                    playerController.enabled = false;
                    break;
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        onMinigameStart += EnableControllers;
        onMinigameStart += StartFoodThrow;
        onMinigameFinish += StopFoodThrow;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onMinigameStart -= EnableControllers;
        onMinigameStart -= StartFoodThrow;
        onMinigameFinish -= StopFoodThrow;
    }

    public void EnableControllers()
    {
        foreach (PlayerCharacter pC in players)
        {
            FoodCatcherController controller = pC.GetComponent<FoodCatcherController>();
            controller.enabled = true;
        }
    }

    public void StartFoodThrow()
    {
        foodThrowCo = StartCoroutine(FoodThrow());
    }

    public void StopFoodThrow()
    {
        canSpawnFood = false;
        StopCoroutine(foodThrowCo);
    }

    private IEnumerator FoodThrow()
    {
        canSpawnFood = true;
        while(canSpawnFood)
        {
            Vector3 randomPointInArea = new Vector3(
                areaPosition.x + Random.Range(-areaSize.x / 2, areaSize.x / 2),
                areaPosition.y + Random.Range(-areaSize.y / 2, areaSize.y / 2),
                areaPosition.z);
                //areaPosition.z + Random.Range(-areaSize.z / 2, areaSize.z / 2));
            PointCollect collectable = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Count)], randomPointInArea, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return null;
    }
}
