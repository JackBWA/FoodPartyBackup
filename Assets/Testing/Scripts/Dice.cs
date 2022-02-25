using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    Rigidbody rb;

    public BoardEntity owner;

    public float throwForce = 10f;
    public float sideThrowSpread = 5f;
    public float minRndRotation = 120f;
    public float maxRndRotation = -60f;
    public LayerMask detectionMask;
    public List<Transform> sides = new List<Transform>();

    public bool used { get; private set; }

    private void Awake()
    {
        if (!TryGetComponent(out rb))
        {
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        Quaternion aux = transform.rotation;
        aux.eulerAngles = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
        transform.rotation = aux;
    }

    public void Throw()
    {
        rb.useGravity = true;
        rb.AddForce((Vector3.up * throwForce)
            + (new Vector3(
                Random.Range(-sideThrowSpread, sideThrowSpread), 
                0f, 
                Random.Range(-sideThrowSpread, sideThrowSpread))), 
            ForceMode.VelocityChange);
        rb.angularVelocity = new Vector3(
            Random.Range(minRndRotation, maxRndRotation),
            Random.Range(minRndRotation, maxRndRotation),
            Random.Range(minRndRotation, maxRndRotation)
        );

        used = true;

        StartCoroutine(WaitUntilDiceStops());
    }

    private IEnumerator WaitUntilDiceStops()
    {
        while (rb.angularVelocity.magnitude > 0f)
        {
            yield return new WaitForSeconds(0.25f);
        }

        int result = CheckResult();
        Debug.Log(result);
        if (result > 0)
        {
            owner.SetMoves(result);
            Destroy(gameObject);
        }
        else
        {
            Throw();
        }
    }

    private int CheckResult()
    {
        int result = -1;
        int i = 0;
        while (i < sides.Count)
        {
            Collider[] objects = Physics.OverlapSphere(sides[i].position, 0.1f, detectionMask);
            if (objects.Length > 0)
            {
                result = sides.Count - i;
                i = sides.Count;
            }
            else
            {
                i++;
            }
        }
        return result;
    }
}