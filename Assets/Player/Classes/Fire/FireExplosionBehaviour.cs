using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] int addStackAmount = 25;
    private LayerMask explosionLayerMask;

    void Start()
    {
        explosionLayerMask = LayerMask.GetMask("Enemy");
    }

    void OnCollisionEnter(Collision col)
    {
        Explode(col);

        DestroyProjectile();
    }

    public virtual void Explode(Collision col)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            // if (enemy.gameObject.name != this.gameObject.name) { 
            if (enemy.gameObject != col.gameObject)
            { // HAVENT TESTED THE NEW COL. INSTEAD OF THIS.
                FireElementClass.ApplyBurn(enemy.gameObject, addStackAmount);
            }
        }
    }

    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
