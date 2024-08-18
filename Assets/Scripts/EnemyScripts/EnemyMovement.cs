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

    [SerializeField] private float pathUpdateRate = 0.2f;
    private float pathUpdateTimer = 0;

    public bool isFollowing = true;

    public float lungeMeleeDamage = 20;
    [SerializeField] private float lungeWaitTime = 1;
    [SerializeField] private float lungeForce = 1;
    [SerializeField] private float lungeHeightOffset = 1;
    public bool isLunging = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
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
        
        // assume it takes a second/s to land. It would be better to raycast the floor to check if its landed
        yield return new WaitForSeconds(lungeWaitTime * 2);

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
}
