using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimDirection))]

public class ShootBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float shootForce;

    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected AimDirection straightAimDirection;

    void Awake() {
        if (straightAimDirection == null) {
            straightAimDirection = GetComponent<AimDirection>();
        }
    }
    
    public virtual void Fire() {
        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
        Debug.DrawRay(transform.position, aimDirection, Color.red, 0.1f);

        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));
        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);
    }
    public virtual void Fire(float damage) {
        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));
        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

        if (damage != 0) {
            currentBullet.GetComponent<DamageOnCollideBehaviour>().damage = damage;
        }
    }

    public virtual void StopFire() {
        // does nothing on the default ShootBehaviour
    }
}
