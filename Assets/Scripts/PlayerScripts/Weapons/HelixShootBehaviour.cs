using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixShootBehaviour : ShootBehaviour
{
    [SerializeField] private int noOfShots = 2;

    public override void Fire() {
        for (int i = 0; i < noOfShots; i++) {
            Vector3 aimDirection = CalculateAimDirection() - shootPoint.position;
            GameObject currentBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(aimDirection));
            currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);

            HelixMovementBehaviour helixBehaviour = currentBullet.GetComponent<HelixMovementBehaviour>();
            helixBehaviour.orbNumber = i;
            helixBehaviour.totalOrbsInSystem = noOfShots;

            helixBehaviour.movementSpeed = shootForce / 7.5f;
        }
    }
}
