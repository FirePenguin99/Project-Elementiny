using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class NewTargetOnHitBehaviour : MonoBehaviour
{
    [SerializeField] private float movementSpeed; // can be set intentially with any number, but if set to 0 the bullet will inherit it's RigidBody velocity 
    [SerializeField] private bool inheritRbVelocity;
    [SerializeField] private float targetMaxRange;
    [SerializeField] private LayerMask detectionLayerMask;
    private List<GameObject> visitedEnemiesList = new List<GameObject>();
    private List<GameObject> possibleEnemyBounces = new List<GameObject>();
    
    private Rigidbody rb;

    void Start() {
        detectionLayerMask = LayerMask.GetMask("Enemy");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col) {
        if (inheritRbVelocity) {
            movementSpeed = rb.velocity.magnitude;
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            BounceToNewTarget(col);
        }
    }

    void BounceToNewTarget(Collider col) {
        GameObject target = FindNewTarget(col);
        if (target == null) {
            return;
        }

        // StartCoroutine(moveToNewTarget(target.transform.position));
        FireAtTarget(target.transform.position);
    }

    GameObject FindNewTarget(Collider col) {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, targetMaxRange, detectionLayerMask);

        visitedEnemiesList.Add(col.gameObject);

        foreach (Collider enemy_i in enemiesInRange) { // for every enemy
            if (enemy_i.gameObject != col.gameObject) { // if the enemy the loop is on, is NOT the same as the collided with enemy
                possibleEnemyBounces.Add(enemy_i.gameObject); // add enemy to the list of possible bounces
                foreach (GameObject enemy_j in visitedEnemiesList){ // for all the enemies we've bounced off of before,
                    if (enemy_i.gameObject == enemy_j) { // if this enemy is one we've bounced off before
                        possibleEnemyBounces.Remove(enemy_i.gameObject); // take him out of the list of possible bounces.
                    }
                }
            }
        }

        GameObject closestEnemy = null;
        float closestEnemyDistance = Mathf.Infinity;
        foreach (GameObject enemy in possibleEnemyBounces) {
            float distance = Vector3.Distance(enemy.transform.position, col.gameObject.transform.position);
            if (distance < closestEnemyDistance) {
                closestEnemy = enemy;
                closestEnemyDistance = distance;
            }
        }

        possibleEnemyBounces.Clear();
        return closestEnemy;
    }

    void FireAtTarget(Vector3 endPostion) {
        Debug.DrawRay(transform.position, endPostion -transform.position, Color.red, 0.5f);
        Vector3 aimDirection = endPostion - transform.position;
        rb.velocity = aimDirection.normalized * movementSpeed;
    }

    // private Queue<Collider> bounceQueue = new Queue<Collider>();
    // public bool firstBounce = true;

    // void OnTriggerEnter(Collider col) {
    //     if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
    //         if (!bounceQueue.Contains(col)) {
    //             if (firstBounce) {
    //                 firstBounce = false;
    //                 bounceQueue.Enqueue(col);
    //                 BounceToNewTarget(col);
    //             } else {
    //                 bounceQueue.Enqueue(col);
    //             }
    //         }
    //     }
    // }

    // IEnumerator moveToNewTarget(Vector3 endPostion) {  
    //     this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

    //     while (Vector3.Distance(transform.position, endPostion) > 0.001f) {
    //         Debug.DrawRay(transform.position, endPostion -transform.position, Color.red, 0.01f);
    //         transform.position = Vector3.MoveTowards(transform.position, endPostion, Time.deltaTime * movementSpeed);
    //         yield return null; // wait a frame
    //     }

    //     this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

    //     if (bounceQueue.Count > 0) { // if aint then it means we've done all the bounces
    //         BounceToNewTarget(bounceQueue.Dequeue());
    //     } else {
    //         print("hullo world");
    //     }
    // }
}
