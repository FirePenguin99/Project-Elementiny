using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string weaponName = "weapon";

    [SerializeField] protected GameObject bullet;
    [SerializeField] protected LayerMask projectileLayerMask;

    [SerializeField] protected float shootForce, upwardForce;

    [SerializeField] protected float fireRate, spread, reloadRate; 
                     public int magazineSize, shotsInMagazine;
    [SerializeField] protected int noOfShots = 1;

                     public bool reloading, isShooting;
    
    protected bool readyToShoot;
    protected bool allowInvoke = true; //this stops multiple Invokes from being played at the same time

    [SerializeField] protected Transform shootPoint;

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

    protected void PlayerInput() {
        isShooting = Input.GetKey(KeyCode.Mouse0);

        if (readyToShoot && isShooting && !reloading && shotsInMagazine > 0) {
            Shoot();
        } else if (readyToShoot && !reloading && shotsInMagazine <= 0) {
            Reload();
        }
    }

    public virtual void Shoot() {
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

    public virtual Vector3 CalculateAimDirection() {
        Ray ray = GameStateHandler.instance.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen

        Vector3 aimPosition;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectileLayerMask)) {
            aimPosition = hit.point;
        } else {
            aimPosition = ray.GetPoint(100); // if the ray hasnt hit anything, just point if far away from the player
        }

        return aimPosition;
    }
}

