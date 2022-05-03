using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoRain : BoardItem_Base
{

    BoardItemControls inputActions;

    public C_TomatoRain prefab;

    public int maxDistance = 5;

    public float speed = .15f;

    public float damage = 8f;

    public List<C_TomatoRain> instances = new List<C_TomatoRain>();

    protected override void Awake()
    {
        base.Awake();
        if(prefab == null) prefab = Resources.Load<C_TomatoRain>("BoardItems/Prefabs/TomatoRain");
        inputActions = new BoardItemControls();
        InitializeControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void InitializeControls()
    {
        inputActions.General.Use.performed += _ => Use();
        inputActions.General.Cancel.performed += _ => Cancel();
    }

    public override void Use()
    {
        base.Use();
        StartCoroutine(InstantiateTomatoes());
    }

    private IEnumerator InstantiateTomatoes()
    {
        foreach(Coaster c in owner.currentCoaster.next)
        {
            StartCoroutine(SpawnAtCoaster(c, maxDistance));
        }
        yield return new WaitForSeconds(speed * maxDistance + 1f);

        foreach(C_TomatoRain tomato in instances)
        {
            tomato.owner = owner;
            tomato.damage = damage;
            tomato.Drop();
        }

        yield return new WaitForSeconds(4f);

        foreach(C_TomatoRain tomato in instances)
        {
            Destroy(tomato.gameObject);
        }
        EndUse();
    }

    private IEnumerator SpawnAtCoaster(Coaster coaster, int left)
    {
        C_TomatoRain instance = Instantiate(prefab);
        instance.transform.position = coaster.transform.position + Vector3.up * 5.5f;
        instances.Add(instance);
        yield return new WaitForSeconds(speed);
        if (left > 1)
        {
            foreach (Coaster c in coaster.next)
            {
                StartCoroutine(SpawnAtCoaster(c, left - 1));
            }
        }
    }
}