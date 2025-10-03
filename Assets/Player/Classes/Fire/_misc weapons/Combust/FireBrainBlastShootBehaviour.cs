using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBrainBlastShootBehaviour : ShootBehaviour
{
    [SerializeField] private LayerMask rayLayerMask;
    [SerializeField] private GameObject targetEnemy;

    [SerializeField] private float heatingTime;
    [SerializeField] private float maxHeatDamage = 200; // y in an exponential graph
    [SerializeField] private float timeToMaxHeat; // x in an exponential graph
    private double exponentialHeatCoefficient; // a = Sqrt(x^2 / y) where a is in exponential expression: y = (x/a)^2

    private WeaponClass weapon;

    private FireElementClass fireClass;

    void Start() {
        fireClass = gameObject.AddComponent<FireElementClass>();
        weapon = GetComponent<WeaponClass>();

        timeToMaxHeat = weapon.magazineSize * weapon.fireRate;

        exponentialHeatCoefficient = Math.Sqrt((timeToMaxHeat * timeToMaxHeat) / maxHeatDamage);
    }

    public override void Fire() {
        if (!targetEnemy) {
            FireRayForTargetEnemy();
        } else {
            BurnTargetEnemy();
        }
    }


    public override void StopFire() {
        targetEnemy = null;
        heatingTime = 0;
    }

    void FireRayForTargetEnemy() {
        weapon.shotsInMagazine += 1; // refund missed shot

        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
        Debug.DrawRay(transform.position, aimDirection, Color.red, 0.1f);

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, aimDirection, out hit, Mathf.Infinity, rayLayerMask)) {
            targetEnemy = hit.collider.gameObject;
        }
    }

    void BurnTargetEnemy() {
        heatingTime += Time.deltaTime;

        if (heatingTime >= timeToMaxHeat) {
            StopFire();
            return;
        }
        double burnStacks = Math.Pow((heatingTime/exponentialHeatCoefficient), 2); // exponential expression: y = (x/a)^2
        int roundedBurnStacks = (int)Math.Round(burnStacks);
        fireClass.ApplyBurn(targetEnemy, roundedBurnStacks);
    }
}
