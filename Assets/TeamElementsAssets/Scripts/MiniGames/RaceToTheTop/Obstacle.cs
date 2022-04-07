using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public Rigidbody rB;

    public float force = 5f;
    public float knockbackDuration = 1f;

    private void Awake()
    {
        if (!TryGetComponent(out rB)) rB = gameObject.AddComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceToTheTopController controller = other.gameObject.GetComponent<RaceToTheTopController>();
            controller.moveVector = transform.forward * force * rB.velocity.magnitude;
            controller.ySpeed = 20f;
            controller.KnockbackEffect(knockbackDuration);
            Destroy(gameObject);
        }
    }
}
