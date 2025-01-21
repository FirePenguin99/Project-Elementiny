using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class NewTargetOnHitBehaviour : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float targetMaxRange;
    public LayerMask explosionLayerMask;
    public List<GameObject> visitedEnemiesList = new List<GameObject>();
    public GameObject currentEnemy;

    void Start()
    {
        explosionLayerMask = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        currentEnemy = col.gameObject;
        BounceToNewTarget(col);
    }

    GameObject FindNewTarget(Collision col) {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, targetMaxRange, explosionLayerMask);

        List<GameObject> possibleEnemyBounces = new List<GameObject>();

        visitedEnemiesList.Add(currentEnemy);


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
            float distance = Vector3.Distance(enemy.transform.position, currentEnemy.transform.position);
            if (distance < closestEnemyDistance) {
                closestEnemy = enemy;
                closestEnemyDistance = distance;
            }
        }

        possibleEnemyBounces.Clear();
        return closestEnemy;
    }

    void BounceToNewTarget(Collision col) {
        GameObject target = FindNewTarget(col);
        if (target == null) {
            return;
        }

        StartCoroutine(moveToNewTarget(target.transform.position));
    }

    IEnumerator moveToNewTarget(Vector3 endPostion) {    
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        // float interpolationRatio = 0;

        // Vector3 startingPos = transform.position;

        while (Vector3.Distance(transform.position, endPostion) > 0.001f) {
            // interpolationRatio += Time.deltaTime * movementSpeed;
            transform.position = Vector3.MoveTowards(transform.position, endPostion, Time.deltaTime * movementSpeed);

            yield return null; // wait a frame
        }

        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
