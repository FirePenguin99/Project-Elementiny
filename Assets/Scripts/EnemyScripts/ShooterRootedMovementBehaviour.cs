using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShooterRootedMovementBehaviour : EnemyMovementBehaviour
{
    private ShooterWeapon weapon;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        weapon = GetComponent<ShooterWeapon>();

        defaultSpeed = agent.speed;
        defaultAngularSpeed = agent.angularSpeed;

        SetPlayerTransform();
    }

    public override void Move() {
        if (!weapon.isShooting && !weapon.reloading){
                // if in range for lunge. Range is defined as stoppingDistance in the NavMeshAgent Component. the isLunging flag is to prevent calling the Coroutine more than once
                if (Vector3.Distance(transform.position, playerTransform.position) <= agent.stoppingDistance) {
                StartCoroutine("enemyShoot");
            } else {
                MoveToPlayer();
            }
        }
    }

    IEnumerator enemyShoot() {
        agent.enabled = false;
        print("started to shoot");
        
        //start shooting until run out of ammo
        weapon.isShooting = true;
        
        while (!weapon.reloading) { // not perfectly efficient. Instead of checking every frame it should just wait for an "activation" call of a function or event?
            yield return null; // wait a frame
        }

        weapon.isShooting = false;
        agent.enabled = true;
    }
    
}
