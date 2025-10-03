using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FireElementClass
{
    private static int defaultAddStackCount = 1;

    public static void SpreadBurn(GameObject targetGameObject, float spreadRadius, LayerMask spreadLayerMask)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(targetGameObject.transform.position, spreadRadius, spreadLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != targetGameObject.name)
            { // dont apply to itself
                ApplyBurn(enemy.gameObject, defaultAddStackCount);
                // print("applied spread to " + enemy.gameObject.name + " from " + this.gameObject.name);
            }
        }
    }

    public static void ApplyBurn(GameObject targetGameObject, int addedStackCount)
    {
        if (targetGameObject.GetComponent<BurnDebuffBehaviour>() == null && targetGameObject.GetComponent<HealthBehaviour>() != null) // if there is no BurnDebuffBehaviour, add one
        {
            BurnDebuffBehaviour burnDebuff = targetGameObject.AddComponent<BurnDebuffBehaviour>();
            burnDebuff.burnStackCount = addedStackCount;
            burnDebuff.burnTimer = 20;
        }
        else
        {
            BurnDebuffBehaviour burnDebuff = targetGameObject.GetComponent<BurnDebuffBehaviour>();
            burnDebuff.burnStackCount += addedStackCount;
            burnDebuff.burnTimer = 20;
        }

    }

    public static void ApplyDirectDamage(GameObject targetGameObject, int directDamage)
    {
        HealthBehaviour enemyHealth = targetGameObject.GetComponent<HealthBehaviour>();
        if (enemyHealth != null)
        {
            enemyHealth.health -= directDamage;
        }
    }
}
