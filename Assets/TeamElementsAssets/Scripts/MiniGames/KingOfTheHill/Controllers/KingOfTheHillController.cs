using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 8f;
    public float jumpForce = 12f;
    public float rotationSpeed = 10f;

    public float punchCooldown = .75f;
    public bool canPunch
    {
        get
        {
            return timer >= punchCooldown;
        }
    }

    private float timer;
    private Coroutine scoreCo;

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
        Debug.Log("AH");
        if (other.Equals(((MiniGame_KingOfTheHill)MiniGame.singleton).stayArea)) scoreCo = StartCoroutine(IncreasePoints());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(((MiniGame_KingOfTheHill)MiniGame.singleton).stayArea)) StopCoroutine(scoreCo);
    }

    public IEnumerator PunchTimer()
    {
        while(timer < punchCooldown)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator IncreasePoints()
    {
        PlayerCharacter pC = GetComponent<PlayerCharacter>();
        while (enabled)
        {
            MiniGame.singleton.AddScore(pC);
            MiniGame.singleton.UpdateScores();
            yield return new WaitForSeconds(.125f);
        }
        yield return null;
    }

    public void Punch()
    {
        timer = 0f;

        // Do stuff.


        StartCoroutine(PunchTimer());
    }
}