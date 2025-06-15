using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TectonicWeaponBehaviour : ShootBehaviour
{
    public List<GameObject> tectonicAreasInside = new List<GameObject>();

    [SerializeField] private float weaponBaseRockDamage = 10;
    [SerializeField] private float weaponTectonicValue;
    
    public float tectonicReductionRate = 1;

    [SerializeField] protected ShootBehaviour shootBehaviour;


    void Update() {
        if (tectonicAreasInside.Count == 0) {
            weaponTectonicValue -= Time.deltaTime * tectonicReductionRate;
        } else {
            weaponTectonicValue = FindLargestTectonicValue();
        }
        
        if (weaponTectonicValue <= 0) {
            weaponTectonicValue = 0;
        }
    }

    private float FindLargestTectonicValue() {
        float largestValue = 0;
        foreach (GameObject tectonicGameObject in tectonicAreasInside) {
            if (tectonicGameObject.GetComponent<TectonicAreaBehaviour>().tectonicValue > largestValue) {
                largestValue = tectonicGameObject.GetComponent<TectonicAreaBehaviour>().tectonicValue;
            }
        }

        return largestValue;
    }

    public override void Fire() {
        foreach (GameObject tectonicGameObject in tectonicAreasInside) {
            tectonicGameObject.GetComponent<TectonicAreaBehaviour>().isIdle = false;
            tectonicGameObject.GetComponent<TectonicAreaBehaviour>().tectonicValue += 1;
        }

        shootBehaviour.Fire(weaponBaseRockDamage * weaponTectonicValue);
    }

    public override void StopFire() {
        foreach (GameObject tectonicGameObject in tectonicAreasInside) {
            tectonicGameObject.GetComponent<TectonicAreaBehaviour>().isIdle = true;
        }

        shootBehaviour.StopFire();
    }
}
