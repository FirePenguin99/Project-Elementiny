using UnityEngine;

public class DamageExplodeOnCollideBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    public int explodeDamage = 25;
    [SerializeField] private LayerMask explosionLayerMask;

    void OnTriggerEnter()
    {
        Explode();
    }

    public void Explode()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        ChargedBulletBehaviour chargedBulletBehaviour = GetComponent<ChargedBulletBehaviour>();
        float chargeMultiplier = chargedBulletBehaviour != null ? chargedBulletBehaviour.bulletChargeMultiplier : 1;

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
