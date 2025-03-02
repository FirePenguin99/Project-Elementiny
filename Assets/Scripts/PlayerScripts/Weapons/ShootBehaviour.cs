using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected LayerMask projectileLayerMask;
    [SerializeField] protected float shootForce;

    [SerializeField] protected Transform shootPoint;
    
    public virtual void Fire() {
        Vector3 aimDirection = CalculateAimDirection() - shootPoint.position;
        Debug.DrawRay(transform.position, aimDirection, Color.red, 0.1f);

        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));
        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);
    }
    public virtual void Fire(float damage) {
        Vector3 aimDirection = CalculateAimDirection() - shootPoint.position;
        GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));
        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

        if (damage != 0) {
            currentBullet.GetComponent<DamageOnCollideBehaviour>().damage = damage;
        }
    }

    public virtual void StopFire() {
        // does nothing on the default ShootBehaviour
    }

    public virtual Vector3 CalculateAimDirection() {
        Ray ray = GameStateHandler.instance.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen

        Vector3 aimPosition;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectileLayerMask)) {
            aimPosition = hit.point;
        } else {
            aimPosition = ray.GetPoint(100); // if the ray hasnt hit anything, just point if far away from the player
        }

        return aimPosition;
    }
}
