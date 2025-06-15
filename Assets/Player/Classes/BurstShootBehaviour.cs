using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShootBehaviour : ShootBehaviour
{
    [SerializeField] private int shotsInBurst = 3;
    [SerializeField] private float timeBetweenShots = 0.2f;

    public override void Fire() {
        StartCoroutine(burstFire());
    }

    IEnumerator burstFire() {
        for (int i = 0; i < shotsInBurst; i++) {
            Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
            GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
