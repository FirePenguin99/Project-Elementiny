using UnityEngine;

public class WeaponChargeBehaviour : WeaponClass
{
    [SerializeField] private float chargeTime = 0.75f; // charge time in seconds
    [SerializeField] private float decayRate = 0.75f; // decay rate per second
    [SerializeField] private bool isCharging = false;

    [SerializeField] protected float timeCharged = 0;
    [SerializeField] private bool partialCharge = true;

    // private float chargeMultiplierScalar = 1; // Used to multiply the damage multiplier of timeCharged. Or used to reduce a charge number that goes above 1 e.g. FireUltimateChargeWeaponBehaviour, where timeCharged goes up on

    public override void Shoot()
    {
        isCharging = true;

        IncrementCharge();
        shootBehaviour.chargeMultiplier = Mathf.Clamp(timeCharged, 0, chargeTime) / chargeTime;
        if (timeCharged >= chargeTime)
        {
            Fire();
        }
    }

    public override void StopShoot()
    {
        isCharging = false;

        if (timeCharged > 0 && partialCharge)
        {
            Fire();
        }

        shootBehaviour.StopFire();
    }

    private void Fire()
    {
        readyToShoot = false;

        for (int i = 0; i < noOfShots; i++)
        {
            shootBehaviour.Fire();
        }

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }

        timeCharged = 0;
        shotsInMagazine--;
    }

    void Update()
    {
        PlayerInput();

        if (!isCharging)
        {
            timeCharged -= decayRate * Time.deltaTime;

            if (timeCharged < 0)
            {
                timeCharged = 0;
            }
        }
    }

    public virtual void IncrementCharge()
    {
        timeCharged += Time.deltaTime;
    }

}
