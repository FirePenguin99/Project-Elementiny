using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BurnWhilstCollideBehaviour : FireElementClass
{
    [SerializeField] private int addedBurnStack = 2;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float burnInterval = 0.5f;

    List<Collider> enemiesInRange = new List<Collider>();

    private float timeSinceLastBurn = 0;

    void OnTriggerEnter(Collider col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            if (col.gameObject.GetComponent<HealthBehaviour>() != null)
            {
                enemiesInRange.Add(col);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            if (col.gameObject.GetComponent<HealthBehaviour>() != null)
            {
                enemiesInRange.Remove(col);
            }
        }
    }

    void Update()
    {
        timeSinceLastBurn += Time.deltaTime;
        if (timeSinceLastBurn >= burnInterval)
        {
            enemiesInRange = enemiesInRange.Where(enemy => enemy != null).ToList();

            foreach (Collider enemy in enemiesInRange)
            {
                ApplyBurn(enemy.gameObject, addedBurnStack);
            }

            timeSinceLastBurn = 0;
        }
    }
}
