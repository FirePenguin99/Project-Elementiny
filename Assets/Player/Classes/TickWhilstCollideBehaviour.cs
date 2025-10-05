using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TickWhilstCollideBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float tickInterval = 0.5f;

    public float chargeMultiplier = 1; // may not always be used

    List<HealthBehaviour> enemiesInRange = new List<HealthBehaviour>();

    private float timeSinceLastTick = 0;

    void OnTriggerEnter(Collider col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            HealthBehaviour enemyHealth = col.gameObject.GetComponent<HealthBehaviour>();
            if (enemyHealth != null)
            {
                enemiesInRange.Add(enemyHealth);
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            HealthBehaviour enemyHealth = col.gameObject.GetComponent<HealthBehaviour>();
            if (enemyHealth != null)
            {
                enemiesInRange.Add(enemyHealth);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            HealthBehaviour enemyHealth = col.gameObject.GetComponent<HealthBehaviour>();
            if (enemyHealth != null)
            {
                enemiesInRange.Remove(enemyHealth);
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            HealthBehaviour enemyHealth = col.gameObject.GetComponent<HealthBehaviour>();
            if (enemyHealth != null)
            {
                enemiesInRange.Remove(enemyHealth);
            }
        }
    }

    void Update()
    {
        timeSinceLastTick += Time.deltaTime;
        if (timeSinceLastTick >= tickInterval)
        {
            enemiesInRange = enemiesInRange.Where(enemy => enemy != null).ToList();

            foreach (HealthBehaviour enemy in enemiesInRange)
            {
                tickFunction(enemy.gameObject);
            }

            timeSinceLastTick = 0;
        }
    }

    public virtual void tickFunction(GameObject enemy)
    {
        print(enemy.name + " is inside the hitbox");
    }
}

