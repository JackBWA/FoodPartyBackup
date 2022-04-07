using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCatcherController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    #region Awake/Start/Update
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
    #endregion

    public void Initialize()
    {
        if (!gameObject.TryGetComponent(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
            controller.center = Vector3.up;
        }
    }

    protected virtual void OnEnable()
    {
        MiniGame.singleton.onMinigameFinish += StopMovement;
    }

    protected virtual void OnDisable()
    {
        MiniGame.singleton.onMinigameFinish -= StopMovement;
    }

    public void StopMovement()
    {
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PointCollect points;
        if (!other.TryGetComponent(out points)) return;
        MiniGame.singleton.AddScore(GetComponent<PlayerCharacter>(), points.value);
        MiniGame.singleton.UpdateScores();
        Destroy(points.gameObject);
    }
}
