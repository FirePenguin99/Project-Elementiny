using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapBehaviour : MonoBehaviour
{
    public static WeaponSwapBehaviour instance;
    
    // [SerializeField]
    // public static List<GameObject> weaponObjects = new List<GameObject>();
    public GameObject[] weaponObjects;
    public int weaponArrayPointer = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SwitchWeapon();
    }

    // Update is called once per frame
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
