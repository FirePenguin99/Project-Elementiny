using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class HudBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // private PlayerHealthBehaviour playerHealth;

    [SerializeField] private TMP_Text AmmoCounter;
    [SerializeField] private TMP_Text WeaponCounter;
    [SerializeField] private TMP_Text HealthCounter;

    private PlayerHealthBehaviour playerHealth;
    private WeaponSwapBehaviour weaponSwap;
    
    // Start is called before the first frame update
    void Awake()
    {
    }

    void OnEnable() {
        GameStateHandler.onPlayerSpawn += SetPlayerObject;
    }
    void OnDisable() {
        GameStateHandler.onPlayerSpawn -= SetPlayerObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (weaponSwap.weaponObjects[weaponSwap.weaponArrayPointer]) {
            AmmoCounter.text = weaponSwap.weaponObjects[weaponSwap.weaponArrayPointer].GetComponent<WeaponClass>().shotsInMagazine.ToString();
            WeaponCounter.text = weaponSwap.weaponObjects[weaponSwap.weaponArrayPointer].GetComponent<WeaponClass>().weaponName.ToString();
        }
        
        HealthCounter.text = playerHealth.health.ToString() + "/100";
    }

    private void SetPlayerObject() {
        player = GameStateHandler.instance.player;
        playerHealth = player.GetComponent<PlayerHealthBehaviour>();
        weaponSwap = player.GetComponent<WeaponSwapBehaviour>();
    }
}
