using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class EnemyTrackingBehaviour : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] float enemyMaxDistance;

    private LayerMask explosionLayerMask;
    
    [SerializeField] GameObject closestEnemy;
    [SerializeField] float enemyDistanceCheckInterval = 0.2f;
    private Vector3 desiredAimPosition;
    private Quaternion desiredRotation;

    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        InvokeRepeating(nameof(EnemyCheck), 0, enemyDistanceCheckInterval);
        explosionLayerMask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (closestEnemy) {
            desiredAimPosition = closestEnemy.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(desiredAimPosition);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime));
        }

        rb.velocity = Vector3.forward * movementSpeed;

        /*
        if (closestEnemy) {
            desiredAimPosition = closestEnemy.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(desiredAimPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime); */
    }

    void EnemyCheck() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, enemyMaxDistance, explosionLayerMask);

        closestEnemy = null;
        float closestEnemyDistance = Mathf.Infinity;
        foreach (Collider enemy in enemiesInRange) {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < closestEnemyDistance) {
                closestEnemy = enemy.gameObject;
                closestEnemyDistance = distance;
            }
        }
    }

}



// check enemies around it (at an interval?)
// whatever enemy is closest is _target

// Update
// desiredAimPosition = Enemy - transform.position
// rotation = Quaternion.LookRotation(desiredAimPosition)
// transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime)

// translate transform.forward * movementSpeed

// https://www.youtube.com/watch?v=Z6qBeuN-H1M&t=23s