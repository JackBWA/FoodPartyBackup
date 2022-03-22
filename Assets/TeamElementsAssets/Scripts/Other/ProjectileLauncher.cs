using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLauncher : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;

    [HideInInspector]
    public int bounces;

    public Vector3 hitPoint;
    public bool hasHit;

    public float lifeTime = 10f;

    private bool isSimulation;

    public List<GameObject> ignore;

    private void Awake()
    {
        TryGetComponent(out rb);
        bounces = 0;
        hasHit = false;
        isSimulation = false;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
    }

    public void Launch(Vector3 force, bool isSimulation)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
        this.isSimulation = isSimulation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSimulation)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player") && !ignore.Contains(other.gameObject))
        {
            hasHit = true;
            hitPoint = other.gameObject.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //hasHit = true;
        hitPoint = collision.contacts[0].point;
        bounces++;
    }
}
