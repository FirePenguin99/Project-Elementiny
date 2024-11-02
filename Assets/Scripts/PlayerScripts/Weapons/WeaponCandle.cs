using System.Collections.Generic;
using UnityEngine;

public class WeaponCandle : WeaponClass
{
    public static List<GameObject> candlesSpawned = new List<GameObject>();
    public bool isCharging = false;
    public bool isReadyToSpawnNext = true;
    
    void Update()
    {
        isCharging = Input.GetKey(KeyCode.Mouse0);

        if (isCharging && isReadyToSpawnNext && magazineSize > candlesSpawned.Count && !reloading) {
            isReadyToSpawnNext = false;
            
            GameObject newCandle = Instantiate(bullet, new Vector3(shootPoint.position.x, shootPoint.position.y + (candlesSpawned.Count * 0f), shootPoint.position.z), Quaternion.identity);
            candlesSpawned.Add(newCandle);

            shotsInMagazine -= 1;

            Invoke(nameof(ResetSpawnNextCandleFlag), fireRate);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !reloading) {
            foreach (GameObject candle in candlesSpawned)
            {
                Vector3 aimDirection = CalculateAimDirection() - candle.transform.position;

                candle.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
                candle.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);
                candle.GetComponent<FireCandleBulletBehaviour>().StartFlying();
            }

            candlesSpawned.Clear();
            isCharging = false;

            Reload();
        }
    }

    private void ResetSpawnNextCandleFlag() {
        isReadyToSpawnNext = true;
    }
}
