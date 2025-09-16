using UnityEngine;

[RequireComponent(typeof(AimDirection))]

public class ShootBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float shootForce;

    [SerializeField] public Transform shootPoint;
    [SerializeField] protected AimDirection straightAimDirection;
    [SerializeField] public float chargeMultiplier = 0;

    void Awake()
    {
        if (straightAimDirection == null)
        {
            straightAimDirection = GetComponent<AimDirection>();
        }
    }

    public virtual void Fire()
    {
        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
        Debug.DrawRay(transform.position, aimDirection, Color.red, 0.1f);

        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));

        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
        if (bulletRb) bulletRb.AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

        ChargedBulletBehaviour chargeBehaviour = currentBullet.GetComponent<ChargedBulletBehaviour>();
        if (chargeBehaviour && chargeMultiplier != 0) chargeBehaviour.bulletChargeMultiplier = chargeMultiplier;
    }
    public virtual void Fire(float damage)
    {
        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;

        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));

        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
        if (bulletRb) bulletRb.AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

        ChargedBulletBehaviour chargeBehaviour = currentBullet.GetComponent<ChargedBulletBehaviour>();
        if (chargeBehaviour) chargeBehaviour.bulletChargeMultiplier = chargeMultiplier;

        if (damage != 0)
        {
            currentBullet.GetComponent<DamageOnCollideBehaviour>().damage = damage;
        }
    }

    public virtual void StopFire()
    {
        // does nothing on the default ShootBehaviour
    }
}
