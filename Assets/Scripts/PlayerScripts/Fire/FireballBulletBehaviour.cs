using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBulletBehaviour : FireElementClass
{
    [SerializeField] private float projectileLifetime;
    [SerializeField] protected float explosionRadius;
    protected LayerMask explosionLayerMask;
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
