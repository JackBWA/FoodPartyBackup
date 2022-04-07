using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MiniGame_RaceToTheTop : MiniGame
{

    public Vector3 obstacleSpawnAreaPosition;
    public Vector3 obstacleSpawnAreaSize;

    public List<GameObject> obstaclesList = new List<GameObject>();
    public float spawnRate = 0.4f;
    public Coroutine spawnCo;
    private bool canSpawnObstacles;

    public RandomVector launchRandomForce;

    [SerializeField]
    private bool debug = true;

    private void OnDrawGizmosSelected()
    {
        if (!debug) return;
        Gizmos.color = Color.red - new Color(0f, 0f, 0f, 201f/255f);
        Gizmos.DrawCube(obstacleSpawnAreaPosition, obstacleSpawnAreaSize);
    }

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

    protected override void InitializePlayers()
    {
        base.InitializePlayers();
        CinemachineFreeLook camera = Resources.Load<CinemachineFreeLook>("Minigames/RaceToTheTop/TPC_RaceToTheTop");
        foreach (PlayerCharacter pC in players)
        {
            CinemachineFreeLook cameraIns = Instantiate(camera);
            cameraIns.transform.SetParent(pC.transform);
            cameraIns.Follow = pC.transform;
            cameraIns.LookAt = pC.transform;
            cameraIns.enabled = false;
            cameraIns.gameObject.SetActive(false);
            switch (pC.characterType)
            {
                case PlayerCharacter.CharacterType.AI:
                    RaceToTheTopAIController aiController = pC.gameObject.AddComponent<RaceToTheTopAIController>();
                    aiController.Initialize();
                    aiController.enabled = false;
                    break;

                case PlayerCharacter.CharacterType.Player:
                    RaceToTheTopPlayerController playerController = pC.gameObject.AddComponent<RaceToTheTopPlayerController>();
                    playerController.Initialize();
                    playerController.enabled = false;
                    cameraIns.enabled = true;
                    cameraIns.gameObject.SetActive(true);
                    break;
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        onMinigameStart += EnableControllers;
        onMinigameStart += StartObstacleSpawning;
        onMinigameFinish += StopObstacleSpawning;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onMinigameStart -= EnableControllers;
        onMinigameStart -= StartObstacleSpawning;
        onMinigameFinish -= StopObstacleSpawning;
    }

    public void EnableControllers()
    {
        foreach (PlayerCharacter pC in players)
        {
            RaceToTheTopController controller = pC.GetComponent<RaceToTheTopController>();
            controller.enabled = true;
        }
    }

    public void StartObstacleSpawning()
    {
        canSpawnObstacles = true;
        StartCoroutine(SpawnObstacles());
    }

    public void StopObstacleSpawning()
    {
        canSpawnObstacles = false;
    }

    private IEnumerator SpawnObstacles()
    {
        while (canSpawnObstacles)
        {
            yield return new WaitForSeconds(spawnRate);
            GameObject obstacle = Instantiate(obstaclesList[Random.Range(0, obstaclesList.Count)]);
            obstacle.transform.position = obstacleSpawnAreaPosition + new Vector3(
                Random.Range(-obstacleSpawnAreaSize.x, obstacleSpawnAreaSize.x) / 2,
                Random.Range(-obstacleSpawnAreaSize.y, obstacleSpawnAreaSize.y) / 2,
                Random.Range(-obstacleSpawnAreaSize.z, obstacleSpawnAreaSize.z) / 2
            );
            obstacle.transform.rotation = transform.rotation;
            Rigidbody rB;
            if (!obstacle.TryGetComponent(out rB)) obstacle.AddComponent<Rigidbody>();
            rB.AddForce(-launchRandomForce.GetRandomVector(), ForceMode.VelocityChange);
        }
        yield return null;
    }
}