using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterWeapon : WeaponClass
{
    // Update is called once per frame
    void Update()
    {
        if (readyToShoot && isShooting && !reloading && shotsInMagazine > 0) {
            Shoot();
        } else if (readyToShoot && !reloading && shotsInMagazine <= 0) {
            Reload();
        }
    }
}
