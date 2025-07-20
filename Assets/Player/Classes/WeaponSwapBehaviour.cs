using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponSwapBehaviour : MonoBehaviour
{
    public List<GameObject> weaponObjects;
    public int weaponArrayPointer = 0;

    [SerializeField] private KeyCode attackKeycode;

    [SerializeField] private KeyCode fowardIterate;
    [SerializeField] private KeyCode backwardIterate;
    [SerializeField] private SpellInventoryHotbarHandler.Hotbars hotbar;

    [SerializeField] private Transform weaponContainerObject;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private SpellInventoryBehaviour playerInventory;

    void Start()
    {
        RefreshWeapons();
    }

    void Update()
    {
        // if (Input.mouseScrollDelta.y > 0) IterateWeaponPointer(1);
        // else if (Input.mouseScrollDelta.y < 0) IterateWeaponPointer(-1);
        if (Input.GetKeyDown(fowardIterate)) IterateWeaponPointer(1);
        else if (Input.GetKeyDown(backwardIterate)) IterateWeaponPointer(-1);
    }

    public void IterateWeaponPointer(int diff)
    {
        if (weaponObjects.Count == 0) return;
        weaponArrayPointer += diff;
        weaponArrayPointer = weaponArrayPointer % weaponObjects.Count;
        weaponArrayPointer = weaponArrayPointer < 0 ? weaponArrayPointer + weaponObjects.Count : weaponArrayPointer;

        SwitchWeapon();
    }

    public void SwitchWeapon()
    {
        foreach (var weapon in weaponObjects)
        {
            if (weapon != null) weapon.SetActive(false);
        }
        if (weaponObjects.Count <= weaponArrayPointer) return;

        weaponObjects[weaponArrayPointer].SetActive(true);
    }

    void RefreshWeapons()
    {
        if (playerInventory == null) return;

        switch (hotbar)
        {
            case SpellInventoryHotbarHandler.Hotbars.Inventory:
                InstantiateSpellWeapons(playerInventory.spellInventory);

                break;
            case SpellInventoryHotbarHandler.Hotbars.RightHand:
                InstantiateSpellWeapons(playerInventory.rightSpellInventory);

                break;
            case SpellInventoryHotbarHandler.Hotbars.LeftHand:
                InstantiateSpellWeapons(playerInventory.leftSpellInventory);

                break;
            case SpellInventoryHotbarHandler.Hotbars.Movement:
                InstantiateSpellWeapons(playerInventory.movementSpellInventory);

                break;
            case SpellInventoryHotbarHandler.Hotbars.Ulitmate:
                InstantiateSpellWeapons(playerInventory.ultimateSpellInventory);

                break;
            case SpellInventoryHotbarHandler.Hotbars.None:
                print("hotbar set to none dumbass, cant instantiate weapons :(");
                return;
            default:
                print("hotbar set to none of the hotbars i've declared dumbass, cant instantiate weapons :(");
                return;
        }

        SwitchWeapon();
    }

    void InstantiateSpellWeapons(List<SpellScriptableObject> inventoryList)
    {
        for (int i = 0; i < weaponContainerObject.childCount; i++)
        {
            Destroy(weaponContainerObject.GetChild(i).gameObject);
        }

        weaponObjects.Clear();

        foreach (SpellScriptableObject spell in inventoryList)
        {
            GameObject newWeapon = Instantiate(spell.weaponPrefab);
            newWeapon.transform.SetParent(weaponContainerObject);

            newWeapon.GetComponent<ShootBehaviour>().shootPoint = shootPosition;
            newWeapon.GetComponent<WeaponClass>().attackKeycode = attackKeycode;

            weaponObjects.Add(newWeapon);
        }
    }

    void OnEnable()
    {
        AddSpellToInventoryBehaviour.onInventoryChange += RefreshWeapons;
    }
    void OnDisable()
    {
        AddSpellToInventoryBehaviour.onInventoryChange -= RefreshWeapons;
    }
}
