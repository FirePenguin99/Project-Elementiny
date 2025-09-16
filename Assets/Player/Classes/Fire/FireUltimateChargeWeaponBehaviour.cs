using UnityEngine;

public class FireUltimateChargeWeaponBehaviour : WeaponClass
{
    [SerializeField] private float burnAbsorbTick = 0.2f;
    private float burnAbsorbTimer;
    [SerializeField] private int burnAbsorbIncrement = 100;

    private int totalBurnAbsorbed = 0;
    // private float burnAmountScalar = 50;

    [SerializeField] private bool shootAtStart = false;

    public void IncrementCharge()
    {
        burnAbsorbTimer += Time.deltaTime;
        if (burnAbsorbTimer >= burnAbsorbTick)
        {
            burnAbsorbTimer = 0;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                BurnDebuffBehaviour enemyBurn = enemy.GetComponent<BurnDebuffBehaviour>();
                if (enemyBurn != null)
                {
                    int burnRemoved = enemyBurn.burnStackCount >= burnAbsorbIncrement ? burnAbsorbIncrement : enemyBurn.burnStackCount;
                    totalBurnAbsorbed += burnRemoved;
                    enemyBurn.burnStackCount -= burnRemoved;

                    print(totalBurnAbsorbed);
                }
            }
        }

    }

    public override void Shoot()
    {
        if (shootAtStart)
        {
            shootBehaviour.Fire();
        }

        IncrementCharge();
        shootBehaviour.chargeMultiplier = totalBurnAbsorbed;
    }

    public override void StopShoot()
    {
        if (!shootAtStart)
        {
            shootBehaviour.Fire();
        }

        shotsInMagazine--;
        totalBurnAbsorbed = 0;

        shootBehaviour.StopFire();
    }
}
