using UnityEngine;

public class RaycastShootBehaviour : ShootBehaviour
{
    public int damage = 50;
    [SerializeField] private LayerMask rayLayerMask;

    public override void Fire()
    {
        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
        Debug.DrawRay(shootPoint.position, aimDirection, Color.red, 0.1f);

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, aimDirection, out hit, Mathf.Infinity, rayLayerMask))
        {
            GameObject enemy = hit.collider.gameObject;
            if (enemy.gameObject.name != this.gameObject.name)
            {
                HealthBehaviour healthComponent = enemy.gameObject.GetComponent<HealthBehaviour>();
                if (healthComponent != null)
                {
                    print("hitted for " + damage * chargeMultiplier + " damage");
                    healthComponent.health -= damage * chargeMultiplier;
                }
            }
        }
    }
}
