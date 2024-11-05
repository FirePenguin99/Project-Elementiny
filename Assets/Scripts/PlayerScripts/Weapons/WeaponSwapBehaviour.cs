using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapBehaviour : MonoBehaviour
{
    // this is a Singleton
    // public static WeaponSwapBehaviour instance;
    
    public GameObject[] weaponObjects;
    public int weaponArrayPointer = 0;
    
    void Awake() 
    { 
        // if (instance != null && instance != this) { 
        //     Destroy(this); 
        // } 
        // else { 
        //     instance = this; 
        // }

        SwitchWeapon();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0) {
            IterateWeaponPointer(1);
        } else if (Input.mouseScrollDelta.y < 0) {
            IterateWeaponPointer(-1);
        }
    }

    public void IterateWeaponPointer(int value) {
        weaponArrayPointer += value;
        weaponArrayPointer = weaponArrayPointer % weaponObjects.Length;
        weaponArrayPointer = weaponArrayPointer <0 ? weaponArrayPointer+weaponObjects.Length : weaponArrayPointer;

        SwitchWeapon();
    }

    public void SwitchWeapon() {
        foreach (var weapon in weaponObjects)
        {
            weapon.SetActive(false);
        }
        weaponObjects[weaponArrayPointer].SetActive(true);
    }
}
