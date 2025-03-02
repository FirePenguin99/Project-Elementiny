using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChargeBehaviour : WeaponClass
{
    [SerializeField] private float chargeTime = 0.75f; // charge time in seconds
    [SerializeField] private float decayRate = 0.75f; // decay rate per second
    [SerializeField] private bool isCharging = false;

    [SerializeField] private float timeCharged = 0;

    public override void Shoot() {
        isCharging = true;

        timeCharged += Time.deltaTime;
        if (timeCharged >= chargeTime) {
            readyToShoot = false;
            shotsInMagazine--;
            timeCharged = 0;

            for (int i = 0; i < noOfShots; i++) {
                shootBehaviour.Fire();
            }

            if (allowInvoke) {
                Invoke(nameof(ResetShot), fireRate);
                allowInvoke = false;
            }
        }
    }

    public override void StopShoot() {
        isCharging = false;
        shootBehaviour.StopFire();
    }

    void Update() {
        PlayerInput();

        if (!isCharging) {
            timeCharged -= decayRate * Time.deltaTime;

            if (timeCharged < 0) {
                timeCharged = 0;
            }
        }
    }

}
