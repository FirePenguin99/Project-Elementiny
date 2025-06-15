using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string weaponName = "weapon";

    public float fireRate, spread, reloadRate; 
    public int magazineSize;
    [HideInInspector]   public int shotsInMagazine;
    [SerializeField]    protected int noOfShots = 1;

    public bool reloading, isShooting = false;
    
    protected bool readyToShoot;
    protected bool allowInvoke = true; //this stops multiple Invokes from being played at the same time

    [SerializeField] protected ShootBehaviour shootBehaviour;

    void Awake() {
        shotsInMagazine = magazineSize;
        readyToShoot = true;
        reloading = false;
    }

    void Update() {
        PlayerInput();
    }

    protected virtual void PlayerInput() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            isShooting = true;
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            isShooting = false;
            StopShoot();
        }

        if (readyToShoot && isShooting && !reloading && shotsInMagazine > 0) {
            Shoot();
        } else if (readyToShoot && !reloading && shotsInMagazine <= 0) {
            Reload();
        }
    }

    public virtual void Shoot() {
        readyToShoot = false;
        shotsInMagazine--;

        for (int i = 0; i < noOfShots; i++) {
            shootBehaviour.Fire();
        }

        if (allowInvoke) {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }
    }

    public virtual void StopShoot() {
        shootBehaviour.StopFire();
    }

    protected void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }

    protected void Reload() {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadRate);
    }
    protected void ReloadFinished() {
        shotsInMagazine = magazineSize;
        reloading = false;
    }
}

