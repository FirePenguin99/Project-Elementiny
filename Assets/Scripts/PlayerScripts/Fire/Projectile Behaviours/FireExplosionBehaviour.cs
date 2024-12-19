using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosionBehaviour : FireElementClass
{
    [SerializeField] private float explosionRadius;
    [SerializeField] int addStackAmount = 25;
    private LayerMask explosionLayerMask;

    void Start() {
        explosionLayerMask = LayerMask.GetMask("Enemy");
    }

    void OnCollisionEnter(Collision col) {
        Explode();

        DestroyProjectile();
    }

    public virtual void Explode() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name) {
                ApplyBurn(enemy.gameObject, addStackAmount);
            }
        }
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
