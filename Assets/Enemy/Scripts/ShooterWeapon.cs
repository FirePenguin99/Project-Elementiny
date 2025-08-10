using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterWeapon : WeaponClass
{
    // Update is called once per frame
    void Update()
    {
        if (readyToShoot && isShooting && shotsInMagazine > 0)
        {
            Shoot();
        }
        else if (readyToShoot && shotsInMagazine <= 0)
        {
            StartReload();
        }
    }
}
