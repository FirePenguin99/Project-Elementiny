using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleShootingBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    
    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void StartFlying(Vector3 direction, float force) {
        rb.AddForce(direction * force, ForceMode.Impulse);

        ProjectileLifetimeBehaviour lifetimeBehaviour = GetComponent<ProjectileLifetimeBehaviour>();
        if (lifetimeBehaviour) {
            lifetimeBehaviour.InvokeDestroyProjectile();
        }
    }
}
