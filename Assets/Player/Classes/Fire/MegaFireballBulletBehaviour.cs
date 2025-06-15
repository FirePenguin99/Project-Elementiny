using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaFireballBulletBehaviour : FireballBulletBehaviour
{
    public int directDamage = 0;

    public override void Explode() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name) {
                ApplyDirectDamage(enemy.gameObject, directDamage);
            }
        }
    }
}
