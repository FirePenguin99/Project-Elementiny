using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleShootingBehaviour : MonoBehaviour
{
    protected Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartFlying(Vector3 direction, float force)
    {
        FollowPlayerBehaviour followPlayer = GetComponent<FollowPlayerBehaviour>(); // Used for the Fire Ultimate Comet to move with the player whilst charging
        if (followPlayer)
        {
            Destroy(followPlayer);
        }

        rb.AddForce(direction * force, ForceMode.Impulse);

        ProjectileLifetimeBehaviour lifetimeBehaviour = GetComponent<ProjectileLifetimeBehaviour>();
        if (lifetimeBehaviour)
        {
            lifetimeBehaviour.InvokeDestroyProjectile();
        }
    }
}
