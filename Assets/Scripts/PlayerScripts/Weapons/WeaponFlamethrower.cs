using UnityEngine;

public class WeaponFlamethrower : WeaponClass
{
    public override void Shoot() {
        readyToShoot = false;
        shotsInMagazine--;

        Vector3 aimDirection = CalculateAimDirection() - shootPoint.position;

        for (int i = 0; i < noOfShots; i++)
        {
            GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);

            currentBullet.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
            currentBullet.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);

            HelixMovementBehaviour helixBehaviour = currentBullet.GetComponent<HelixMovementBehaviour>();
            if (helixBehaviour) {
                helixBehaviour.orbNumber = i;
                helixBehaviour.totalOrbsInSystem = noOfShots;

                helixBehaviour.movementSpeed = shootForce / 7.5f;
            }
        }
        
        if (allowInvoke) {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }
    }
}
