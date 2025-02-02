using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurnOnCollideBehaviour : FireElementClass
{
    [SerializeField] private int addedBurnStack = 10;
    [SerializeField] private LayerMask collisionLayers;

    void OnTriggerEnter(Collider col) {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0) {
            if (col.gameObject.GetComponent<HealthBehaviour>() != null) {
                ApplyBurn(col.gameObject, addedBurnStack);
            } 
        }        
    }
}
