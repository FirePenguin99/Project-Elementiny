using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodeOnCollideBehaviour : FireElementClass
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private int addStackAmount = 25;
    [SerializeField] private LayerMask explosionLayerMask;

    void OnTriggerEnter() {
        Explode();
    }

    public virtual void Explode() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange) {
            if (enemy.gameObject.name != this.gameObject.name) {
                ApplyBurn(enemy.gameObject, addStackAmount);
            }
        }
    }
}
