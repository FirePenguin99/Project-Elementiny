using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyShooterWeapon))]

public class ShooterRootedMovementBehaviour : EnemyMovementBehaviour
{
    private EnemyShooterWeapon weapon;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        weapon = GetComponent<EnemyShooterWeapon>();

        defaultSpeed = agent.speed;
        defaultAngularSpeed = agent.angularSpeed;

        SetPlayerTransform();
    }

    public override void Move()
    {
        if (!weapon.isShooting)
        {
            // if in range for lunge. Range is defined as stoppingDistance in the NavMeshAgent Component. the isLunging flag is to prevent calling the Coroutine more than once
            if (Vector3.Distance(transform.position, playerTransform.position) <= agent.stoppingDistance)
            {
                StartCoroutine(enemyShoot());
            }
            else
            {
                MoveToPlayer();
            }
        }
    }

    IEnumerator enemyShoot()
    {
        agent.enabled = false;

        //start shooting until run out of ammo
        weapon.isShooting = true;

        // not perfectly efficient. Instead of checking every frame it should just wait for an "activation" call of a function or event?
        while (weapon.shotsInMagazine > 0) { yield return null; }

        weapon.isShooting = false;
        agent.enabled = true;
    }

}
