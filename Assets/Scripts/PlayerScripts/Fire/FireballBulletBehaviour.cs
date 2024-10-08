using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBulletBehaviour : FireElementClass
{
    public float projectileLifetime;
    public float explosionRadius;
    private LayerMask explosionLayerMask;
    public int addStackAmount = 25;
    
    void Start()
    {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
        explosionLayerMask = LayerMask.GetMask("Enemy");

    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1f,0f,0f,0.1f);
        
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }

    void OnCollisionEnter(Collision col) {
        Explode();

        DestroyProjectile();
    }

    private void Explode() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name) {
                ApplyBurn(enemy.gameObject, addStackAmount);
                // print("applied spread to " + enemy.gameObject.name + " from " + this.gameObject.name);
            }
        }
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
