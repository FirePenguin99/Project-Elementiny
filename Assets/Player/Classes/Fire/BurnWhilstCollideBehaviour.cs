using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BurnWhilstCollideBehaviour : FireElementClass
{
    [SerializeField] private int addedBurnStack = 2;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float burnInterval = 0.5f;

    List<HealthBehaviour> enemiesInRange = new List<HealthBehaviour>();

    private float timeSinceLastBurn = 0;

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
        timeSinceLastBurn += Time.deltaTime;
        if (timeSinceLastBurn >= burnInterval)
        {
            enemiesInRange = enemiesInRange.Where(enemy => enemy != null).ToList();

            foreach (HealthBehaviour enemy in enemiesInRange)
            {
                ApplyBurn(enemy.gameObject, addedBurnStack);
            }

            timeSinceLastBurn = 0;
        }
    }
}
