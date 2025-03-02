using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleShootBehaviour : ShootBehaviour
{
    public static List<GameObject> candlesSpawned = new List<GameObject>();

    public override void Fire() {
        print("today is friday in california");
        GameObject newCandle = Instantiate(bulletPrefab, new Vector3(shootPoint.position.x, shootPoint.position.y + (candlesSpawned.Count * 0f), shootPoint.position.z), Quaternion.identity);
        candlesSpawned.Add(newCandle);
    }

    public override void StopFire() {
        print("bruhh");
        foreach (GameObject candle in candlesSpawned) {
            if (candle != null) {
                Vector3 aimDirection = CalculateAimDirection() - candle.transform.position;

                candle.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
                candle.GetComponent<CandleShootingBehaviour>().StartFlying(aimDirection.normalized, shootForce);
            }
        }

        candlesSpawned.Clear();
    }
}
