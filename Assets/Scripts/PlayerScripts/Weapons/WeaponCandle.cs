using System.Collections.Generic;
using UnityEngine;

public class WeaponCandle : WeaponClass
{
    public static List<GameObject> candlesSpawned = new List<GameObject>();
    public bool isCharging = false;
    public bool isReadyToSpawnNext = true;

    private float charge = 0;

    public override void Shoot() {
        if (magazineSize > candlesSpawned.Count && !reloading) {
            charge += Time.deltaTime; // didnt use Invoke as if I stop holding the trigger but an Invoke just got called and executes later, it would run even if I wasnt holding the trigger
            if (charge >= fireRate) {
                charge = 0;

                GameObject newCandle = Instantiate(bullet, new Vector3(shootPoint.position.x, shootPoint.position.y + (candlesSpawned.Count * 0f), shootPoint.position.z), Quaternion.identity);
                candlesSpawned.Add(newCandle);

                shotsInMagazine -= 1;
            }
        }
    }

    public override void StopShoot() {
        charge = 0;
        foreach (GameObject candle in candlesSpawned) {
            if (candle != null) {
                Vector3 aimDirection = CalculateAimDirection() - candle.transform.position;

                candle.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
                candle.GetComponent<FireCandleBulletBehaviour>().StartFlying(aimDirection.normalized, shootForce);
            }
        }

        candlesSpawned.Clear();
        isCharging = false;

        Reload();
    }
}
