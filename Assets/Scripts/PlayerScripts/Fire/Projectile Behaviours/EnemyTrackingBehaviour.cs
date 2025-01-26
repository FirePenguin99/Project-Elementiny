using UnityEngine;

public class EnemyTrackingBehaviour : MonoBehaviour
{
    private float rotationSpeed;
    [SerializeField] float baseRotationSpeed;
    [SerializeField] float bonusProximityRotationSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] float enemyMaxDistance;

    private LayerMask detectionLayerMask;
    
    [SerializeField] GameObject closestEnemy;
    [SerializeField] float enemyDistanceCheckInterval = 0.2f;
    private Vector3 desiredAimPosition;
    private Quaternion desiredRotation;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        InvokeRepeating(nameof(EnemyCheck), 0, enemyDistanceCheckInterval);
        detectionLayerMask = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        if (closestEnemy) {
            desiredAimPosition = closestEnemy.transform.position - transform.position;
            desiredRotation = Quaternion.LookRotation(desiredAimPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

            rotationSpeed = baseRotationSpeed + ((Vector3.Distance(transform.position, closestEnemy.transform.position) / enemyMaxDistance) * bonusProximityRotationSpeed);
        }

        rb.velocity = Vector3.zero; // rb shi mucks up me movement
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        // Debug.DrawRay(transform.position, desiredAimPosition, Color.red);
    }

    void EnemyCheck() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, enemyMaxDistance, detectionLayerMask);

        closestEnemy = EnemyMostInFront(enemiesInRange);
    }

    GameObject EnemyMostInFront(Collider[] enemies) {
        GameObject bestFacingEnemy = null;
        float bestFacingEnemyDotValue = Mathf.Infinity;

        foreach (Collider enemy in enemies) {
            Vector3 EnemyToProjectileDirection = Vector3.Normalize(transform.position - enemy.transform.position);
            float dotValue = Vector3.Dot(EnemyToProjectileDirection, transform.forward);

            print(dotValue);
            
            if (dotValue < bestFacingEnemyDotValue) { // if its less than the previous smallest, it means the enemy and projectile are looking at each other more
                bestFacingEnemy = enemy.gameObject;
                bestFacingEnemyDotValue = dotValue;
            }
        }

        return bestFacingEnemy;
    }
}