using UnityEngine;

public class WeaponFlamethrower : WeaponClass
{
    public override void Shoot() {
        readyToShoot = false;
        shotsInMagazine--;

        Vector3 aimDirection = CalculateAimDirection() - shootPoint.position;

        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);

        currentBullet.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
        currentBullet.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);

        if (allowInvoke) {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }
    }
}
