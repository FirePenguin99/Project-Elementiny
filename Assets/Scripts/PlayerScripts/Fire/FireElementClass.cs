using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementClass : MonoBehaviour
{
    private int defaultAddStackCount = 1;

    public void SpreadBurn(float spreadRadius, LayerMask spreadLayerMask) {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spreadRadius, spreadLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name) { // dont apply to itself
                ApplyBurn(enemy.gameObject, defaultAddStackCount);
                // print("applied spread to " + enemy.gameObject.name + " from " + this.gameObject.name);
            }
        }
    }

    public void ApplyBurn(GameObject targetGameObject, int addedStackCount) {
        if (targetGameObject.GetComponent<BurnDebuffBehaviour>() == null && targetGameObject.GetComponent<HealthBehaviour>() != null) // if there is no BurnDebuffBehaviour, add one
        {
            BurnDebuffBehaviour burnDebuff = targetGameObject.AddComponent<BurnDebuffBehaviour>();
            burnDebuff.burnStackCount = addedStackCount;
            burnDebuff.burnTimer = 20;
        } else 
        {
            BurnDebuffBehaviour burnDebuff = targetGameObject.GetComponent<BurnDebuffBehaviour>();
            burnDebuff.burnStackCount += addedStackCount;
            burnDebuff.burnTimer = 20;
        }
        
    }
}
