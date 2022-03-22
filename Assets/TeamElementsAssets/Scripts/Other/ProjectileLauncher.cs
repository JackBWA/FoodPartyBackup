using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLauncher<T> : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;

    [HideInInspector]
    public int bounces;

    public T target;

    public float lifeTime = 10f;

    private void Awake()
    {
        TryGetComponent(out rb);
        bounces = 0;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
    }

    public void Launch(Vector3 force)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject.GetComponent<T>();
            return;
        }
        bounces++;
    }
}