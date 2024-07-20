using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlamethrower : MonoBehaviour
{
    public GameObject bullet;
    public LayerMask projectileLayerMask;

    public float shootForce, upwardForce;

    public float fireRate, spread, reloadRate; 
    public int magazineSize, shotsInMagazine;

    bool isShooting, readyToShoot, reloading;
    bool allowInvoke = true; //this stops multiple Invokes from being played at the same time

    public Camera playerCam;
    public Transform shootPoint;
    
    void Awake()
    {
        shotsInMagazine = magazineSize;
        readyToShoot = true;
        reloading = false;
    }

    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput() {
        isShooting = Input.GetKey(KeyCode.Mouse0);

        if (readyToShoot && isShooting && !reloading && shotsInMagazine > 0) {
            Shoot();
        } else if (readyToShoot && !isShooting && !reloading && shotsInMagazine <= 0) {
            Reload();
        }
    }

    private void Shoot() {
        readyToShoot = false;
        shotsInMagazine--;

        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen
        RaycastHit hit;

        Vector3 aimPosition;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, projectileLayerMask)) {
            // print("hit a " + hit.transform.gameObject.name);
            aimPosition = hit.point;
        } else {
            aimPosition = ray.GetPoint(100); // if the ray hasnt hit anything, just point if far away from the player
        }

        Vector3 aimDirection = aimPosition - shootPoint.position;


        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);

        currentBullet.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
        currentBullet.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);


        if (allowInvoke) {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }
    }

    private void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload() {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadRate);
    }
    private void ReloadFinished() {
        shotsInMagazine = magazineSize;
        reloading = false;
    }

}
