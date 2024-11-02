using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class HudBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject WeaponContainer;
    private WeaponSwapBehaviour wsb;

    [SerializeField] private GameObject Player;
    private PlayerHealthBehaviour playerHealth;

    [SerializeField] private TMP_Text AmmoCounter;
    [SerializeField] private TMP_Text WeaponCounter;
    [SerializeField] private TMP_Text HealthCounter;
    
    // Start is called before the first frame update
    void Awake()
    {
        wsb = WeaponContainer.GetComponent<WeaponSwapBehaviour>();
        playerHealth = Player.GetComponent<PlayerHealthBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AmmoCounter.text = wsb.weaponObjects[wsb.weaponArrayPointer].GetComponent<WeaponClass>().shotsInMagazine.ToString();
        WeaponCounter.text = wsb.weaponObjects[wsb.weaponArrayPointer].GetComponent<WeaponClass>().weaponName.ToString();
        HealthCounter.text = playerHealth.health.ToString() + "/100";
    }
}
