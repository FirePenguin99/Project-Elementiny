using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string weaponName = "weapon";

    public KeyCode attackKeycode;

    public float fireRate, spread, reloadRate;
    public int magazineSize;
    public int shotsInMagazine;
    [SerializeField] protected int noOfShots = 1;

    public bool isShooting = false; // is needed as scripts that inherit from this one find other ways to start shooting, other than player input. Therefore a bool must be used as a flag

    protected bool readyToShoot;
    protected bool allowInvoke = true; // this stops multiple Invokes from being played at the same time

    [SerializeField] protected ShootBehaviour shootBehaviour;

    private Coroutine reloadCoroutine;

    void Awake()
    {
        shotsInMagazine = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        PlayerInput();
    }

    protected virtual void PlayerInput()
    {
        StartReload();

        if (Input.GetKeyDown(attackKeycode))
        {
            isShooting = true;
        }
        else if (Input.GetKeyUp(attackKeycode))
        {
            isShooting = false;
            StopShoot();
        }

        if (readyToShoot && isShooting && shotsInMagazine > 0)
        {
            if (reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine);
                reloadCoroutine = null;
            }
            Shoot();
        }
    }

    public virtual void Shoot()
    {
        readyToShoot = false;
        shotsInMagazine--;

        for (int i = 0; i < noOfShots; i++)
        {
            shootBehaviour.Fire();
        }

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }
    }

    public virtual void StopShoot()
    {
        shootBehaviour.StopFire();
    }

    protected void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    protected void StartReload()
    {
        if (reloadCoroutine == null) reloadCoroutine = StartCoroutine(nameof(Reload));
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadRate);
        shotsInMagazine = magazineSize;
        reloadCoroutine = null;
    }

    void OnDisable()
    {
        isShooting = false;
    }
}

