using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject lungeHitbox;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public LayerMask groundLayer;
    public float enemyHeight = 0.5f;

    [SerializeField] private float pathUpdateRate = 0.2f;
    private float pathUpdateTimer = 0;

    public bool isFollowing = true;

    public float lungeMeleeDamage = 20;
    [SerializeField] private float lungeWaitTime = 1;
    [SerializeField] private float lungeForce = 1;
    [SerializeField] private float lungeHeightOffset = 1;
    public bool isLunging = false;

    public float defaultSpeed;
    public float defaultAngularSpeed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        defaultSpeed = agent.speed;
        defaultAngularSpeed = agent.angularSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLunging){
            // if in range for lunge. Range is defined as stoppingDistance in the NavMeshAgent Component. the isLunging flag is to prevent calling the Coroutine more than once
            if (Vector3.Distance(transform.position, playerTransform.position) <= agent.stoppingDistance) {
                StartCoroutine("enemyLunge");
            } else {
                MoveToPlayer();
            }
        }
        
    }

    void MoveToPlayer() {
        if (pathUpdateTimer >= pathUpdateRate) {
            agent.destination = playerTransform.position;
            pathUpdateTimer = 0;
        } else {
            pathUpdateTimer += Time.deltaTime;
        }
    }

    IEnumerator enemyLunge() {
        isLunging = true;
        agent.enabled = false;
        print("started to lunge");
        
        // stop moving for few seconds
        yield return new WaitForSeconds(lungeWaitTime * 0.75f);
        
        // aim at player
        Vector3 aimPosition = playerTransform.position;
        Vector3 aimDirection = aimPosition - transform.position;
        aimDirection.y = aimDirection.y + lungeHeightOffset;

        // stop aiming for a bit
        yield return new WaitForSeconds(lungeWaitTime * 0.25f);
        
        // lunge at player
        EnableLungeHitbox();
        rb.isKinematic = false;
        rb.AddForce(aimDirection * lungeForce, ForceMode.Impulse);
        
        // wait for lunge to finish
        yield return new WaitForSeconds(lungeWaitTime * 2);

        // continue with re-enabling NPC movement only if the NPC is on the floor. while loop is a bit dangerous, but so is everything if you use it wrong. This one is just more likely to be used wrong
        while (!Physics.Raycast(transform.position, Vector3.down, enemyHeight * 0.5f + 0.2f, groundLayer)) {
            yield return null; // wait a frame
        }

        DisableLungeHitbox();
        rb.isKinematic = true;
        isLunging = false;
        agent.enabled = true;
    }

    void EnableLungeHitbox() {
        lungeHitbox.SetActive(true);
        lungeHitbox.GetComponent<MeleeHitbox>().meleeDamage = lungeMeleeDamage;
    }

    void DisableLungeHitbox() {
        lungeHitbox.SetActive(false);
    }

    public void UpdateSpeeds(float multiplier) {
        agent.speed = defaultSpeed * multiplier;
        agent.angularSpeed = defaultAngularSpeed * multiplier;
    }
}
