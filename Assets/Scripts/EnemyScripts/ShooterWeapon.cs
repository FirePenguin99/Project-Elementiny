using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterWeapon : WeaponClass
{
    // [SerializeField] private GameObject bullet;

    // [SerializeField] private float shootForce, upwardForce;

    // [SerializeField] private float fireRate, spread, reloadRate; 
    // [SerializeField] private int magazineSize, shotsInMagazine;
    
    // [SerializeField] private bool readyToShoot, reloading;
    // [SerializeField] private bool allowInvoke = true; //this stops multiple Invokes from being played at the same time

    // public new bool isShooting;

    // public Camera playerCam;
    // public Transform shootPoint;
    
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
