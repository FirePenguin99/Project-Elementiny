using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class HudBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // private PlayerHealthBehaviour playerHealth;

    [SerializeField] private TMP_Text R_AmmoCounter;
    [SerializeField] private TMP_Text R_WeaponCounter;
    [SerializeField] private TMP_Text L_AmmoCounter;
    [SerializeField] private TMP_Text L_WeaponCounter;
    [SerializeField] private TMP_Text HealthCounter;

    [SerializeField] private PlayerHealthBehaviour playerHealth;
    [SerializeField] private WeaponSwapBehaviour R_weaponSwap;
    [SerializeField] private WeaponSwapBehaviour L_weaponSwap;

    // Start is called before the first frame update
    void Awake()
    {
    }

    void OnEnable()
    {
        GameStateHandler.onPlayerSpawn += SetPlayerObject;
    }
    void OnDisable()
    {
        GameStateHandler.onPlayerSpawn -= SetPlayerObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRightHUD(R_weaponSwap, R_AmmoCounter, R_WeaponCounter);
        UpdateRightHUD(L_weaponSwap, L_AmmoCounter, L_WeaponCounter);

        HealthCounter.text = playerHealth.health.ToString() + "/100";
    }

    private void SetPlayerObject()
    {
        player = GameStateHandler.instance.player;
        playerHealth = player.GetComponent<PlayerHealthBehaviour>();
        R_weaponSwap = player.GetComponent<WeaponSwapBehaviour>();
        // L_weaponSwap;
    }

    void UpdateRightHUD(WeaponSwapBehaviour swapBehaviour, TMP_Text ammoCounter, TMP_Text weaponCounter)
    {
        if (swapBehaviour.weaponObjects.Count <= swapBehaviour.weaponArrayPointer) return;
        if (swapBehaviour.weaponObjects[swapBehaviour.weaponArrayPointer])
        {
            ammoCounter.text = swapBehaviour.weaponObjects[swapBehaviour.weaponArrayPointer].GetComponent<WeaponClass>().shotsInMagazine.ToString();
            weaponCounter.text = swapBehaviour.weaponObjects[swapBehaviour.weaponArrayPointer].GetComponent<WeaponClass>().weaponName.ToString();
        }
    }
}
