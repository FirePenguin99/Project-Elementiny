using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementClass : MonoBehaviour
{
    
    public void SpreadBurn(float spreadRadius, LayerMask spreadLayerMask) {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spreadRadius, spreadLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name) {
                ApplyBurn(enemy.gameObject);
                // print("applied spread to " + enemy.gameObject.name + " from " + this.gameObject.name);
            }
        }
    }

    public void ApplyBurn(GameObject targetGameObject) {
        if (targetGameObject.GetComponent<BurnDebuffBehaviour>() == null) // if there is no BurnDebuffBehaviour, add one
        {
            BurnDebuffBehaviour burnDebuff = targetGameObject.AddComponent<BurnDebuffBehaviour>();
            burnDebuff.burnStackCount = 1;
            burnDebuff.burnTimer = 20;
        } else 
        {
            BurnDebuffBehaviour burnDebuff = targetGameObject.GetComponent<BurnDebuffBehaviour>();
            burnDebuff.burnStackCount += 1;
            burnDebuff.burnTimer = 20;
        }
        
    }
}
