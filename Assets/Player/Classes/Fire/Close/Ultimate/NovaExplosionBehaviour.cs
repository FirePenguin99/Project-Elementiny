using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaExplosionBehaviour : ShootBehaviour
{
    [SerializeField] private float explosionRadius;
    public int explodeDamage = 25;
    [SerializeField] private LayerMask explosionLayerMask;

    public override void Fire()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name)
            {
                HealthBehaviour healthComponent = enemy.gameObject.GetComponent<HealthBehaviour>();
                if (healthComponent != null)
                {
                    healthComponent.health -= explodeDamage * chargeMultiplier;
                }
            }
        }
    }
}
