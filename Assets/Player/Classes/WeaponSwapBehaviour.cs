using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapBehaviour : MonoBehaviour
{
    public List<GameObject> weaponObjects;
    public int weaponArrayPointer = 0;

    [SerializeField] private SpellInventoryHotbarHandler.Hotbars hotbar;
    [SerializeField] private Transform weaponContainerObject;
    [SerializeField] private Transform shootPosition;

    private SpellInventoryBehaviour playerInventory;

    void Start()
    {
        RefreshWeapons();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0) IterateWeaponPointer(1);
        else if (Input.mouseScrollDelta.y < 0) IterateWeaponPointer(-1);
    }

    public void IterateWeaponPointer(int diff)
    {
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
        weaponObjects[weaponArrayPointer].SetActive(true);
    }

    void RefreshWeapons()
    {
        playerInventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
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
            print("instantiated that shi");

            // SpellScriptableObject spell = inventoryList[i];
            GameObject newWeapon = Instantiate(spell.weaponPrefab);
            newWeapon.transform.SetParent(weaponContainerObject);

            newWeapon.GetComponent<ShootBehaviour>().shootPoint = shootPosition;

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
