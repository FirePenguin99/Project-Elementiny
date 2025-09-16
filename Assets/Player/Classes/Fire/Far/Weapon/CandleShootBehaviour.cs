using System.Collections.Generic;
using UnityEngine;

public class CandleShootBehaviour : ShootBehaviour
{
    public static List<GameObject> candlesSpawned = new List<GameObject>();

    [SerializeField] private int maxCandles;

    public override void Fire()
    {
        if (candlesSpawned.Count >= maxCandles)
        {
            return;
        }
        GameObject newCandle = Instantiate(bulletPrefab, new Vector3(shootPoint.position.x, shootPoint.position.y + (candlesSpawned.Count * 0f), shootPoint.position.z), Quaternion.identity);
        candlesSpawned.Add(newCandle);
    }

    public override void StopFire()
    {
        foreach (GameObject candle in candlesSpawned)
        {
            if (candle != null)
            {
                Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - candle.transform.position;

                candle.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
                candle.GetComponent<CandleShootingBehaviour>().StartFlying(aimDirection.normalized, shootForce);
            }
        }

        ChargedBulletBehaviour mostRecentChargeBehaviour = candlesSpawned[candlesSpawned.Count - 1].GetComponent<ChargedBulletBehaviour>();
        if (mostRecentChargeBehaviour && chargeMultiplier != 0) mostRecentChargeBehaviour.bulletChargeMultiplier = chargeMultiplier;

        candlesSpawned.Clear();
    }
}
