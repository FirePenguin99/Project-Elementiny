using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollideBehaviour : MonoBehaviour
{
    public float damage = 10;
    [SerializeField] private LayerMask collisionLayers;

    void OnTriggerEnter(Collider col)
    {
        ChargedBulletBehaviour chargedBulletBehaviour = GetComponent<ChargedBulletBehaviour>();
        float chargeMultiplier = chargedBulletBehaviour != null ? chargedBulletBehaviour.bulletChargeMultiplier : 1;

        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            HealthBehaviour healthComponent = col.gameObject.GetComponent<HealthBehaviour>();

            if (healthComponent != null)
            {
                healthComponent.health -= damage;
            }
        }
    }
}
