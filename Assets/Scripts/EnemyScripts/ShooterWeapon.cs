using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterWeapon : WeaponClass
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

    public void EnemyInput() {

    }

    public override Vector3 CalculateAimDirection() {
        print("shooter shootin");

        Vector3 aimPosition;

        aimPosition = GameStateHandler.instance.player.transform.position;

        return aimPosition;
    }
}
