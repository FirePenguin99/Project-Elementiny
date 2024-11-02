using System.Collections.Generic;
using UnityEngine;

public class WeaponRockthrower : WeaponClass
{
    public List<GameObject> tectonicAreasInside = new List<GameObject>();

    public float weaponBaseRockDamage = 10;
    public float weaponTectonicValue;
    
    public float tectonicReductionRate = 1;

    // public GameObject tectonicGameObject;

    public override void Shoot() { // not confident in this use of override as Polymorphism
        readyToShoot = false;
        shotsInMagazine--;
        
        Vector3 aimDirection = CalculateAimDirection() - shootPoint.position;

        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);

        currentBullet.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
        currentBullet.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);

        currentBullet.GetComponent<RockBulletBehaviour>().baseRockDamage = weaponBaseRockDamage;
        currentBullet.GetComponent<RockBulletBehaviour>().tectonicValue = weaponTectonicValue;
        
        foreach (GameObject tectonicGameObject in tectonicAreasInside)
        {
            tectonicGameObject.GetComponent<TectonicAreaBehaviour>().tectonicValue += 1;
        }
        
        if (allowInvoke) {
            Invoke(nameof(ResetShot), fireRate);
            allowInvoke = false;
        }
    }

    void Update() {
        if (tectonicAreasInside.Count == 0) {
            weaponTectonicValue -= Time.deltaTime * tectonicReductionRate;
        } else {
            weaponTectonicValue = FindLargestTectonicValue();
        }
        if (weaponTectonicValue <= 0) {
            weaponTectonicValue = 0;
        }
        PlayerInput();
    }

    private int FindLargestTectonicValue() {
        int largestValue = 0;
        foreach (GameObject tectonicGameObject in tectonicAreasInside)
        {
            if (tectonicGameObject.GetComponent<TectonicAreaBehaviour>().tectonicValue > largestValue) {
                largestValue = tectonicGameObject.GetComponent<TectonicAreaBehaviour>().tectonicValue;
            }
        }
        return largestValue;
    }
}
