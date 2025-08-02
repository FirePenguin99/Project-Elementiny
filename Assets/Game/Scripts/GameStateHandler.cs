using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public static GameStateHandler instance;

    public GameObject player;
    public Transform spawnPoint;
    public Camera playerCamera;

    [SerializeField] private GameObject playerFirePrefab;
    [SerializeField] private GameObject playerLightningPrefab;
    [SerializeField] private GameObject playerIcePrefab;
    [SerializeField] private GameObject playerEarthPrefab;
    public enum classes
    {
        Fire,
        Lightning,
        Ice,
        Earth
    }

    public GameObject StartMenuCanvas;
    public GameObject HudCanvas;
    public GameObject InventoryCanvas;

    public delegate void OnPlayerSpawn(); // here is the delegate, a collection of functions (like an array of functions)
    public static event OnPlayerSpawn onPlayerSpawn; // here is the event, which uses the delegate of functions "OnplayerSpawn"

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCharacterSelect();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleSpellInventory();
        }
    }

    public void SpawnPlayerAsClass(classes classEnum)
    {
        if (player) { Destroy(player); }

        Dictionary<classes, GameObject> classToPrefabDict = new Dictionary<classes, GameObject> // what an overcomplication compared to four if statements, but its cool as fuck.
        {
            { classes.Fire, playerFirePrefab },
            { classes.Lightning, playerLightningPrefab },
            { classes.Ice, playerIcePrefab },
            { classes.Earth, playerEarthPrefab }
        };

        player = Instantiate(classToPrefabDict[classEnum], spawnPoint.position, spawnPoint.rotation);

        // Camera
        // not the happiest about this use of Find and strings. All object names are subject to change and since there is no reference by object or class, I'd have to *remember* to update these strings
        Transform cameraPos = player.transform.Find("CameraPos");
        Transform playerOrientation = player.transform.Find("Orientation");
        if (cameraPos == null) { print("GameObject by the name of CameraPos not found!"); }
        if (playerOrientation == null) { print("GameObject by the name of Orientation not found!"); }

        CameraFollowPlayer followCamBehaviour = playerCamera.gameObject.GetComponent<CameraFollowPlayer>();
        followCamBehaviour.cameraPosition = cameraPos;
        followCamBehaviour.enabled = true;

        PlayerCamera playerCamBehaviour = playerCamera.gameObject.GetComponent<PlayerCamera>();
        playerCamBehaviour.playerOrientation = playerOrientation;
        playerCamBehaviour.enabled = true;

        playerCamBehaviour.resetRotation();

        // Inventory
        // SpellInventoryBehaviour playerInventory = player.GetComponent<SpellInventoryBehaviour>();
        // SpellInventoryUiBehaviour inventoryUiHotbars = InventoryCanvas.GetComponent<SpellInventoryUiBehaviour>();
        // if (inventoryUiHotbars) {inventoryUiHotbars.pla}

        // UI
        HudCanvas.SetActive(true);
        StartMenuCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);

        onPlayerSpawn?.Invoke();
    }

    public void ToggleCharacterSelect()
    {
        if (player)
        {
            Cursor.lockState = StartMenuCanvas.activeSelf ? CursorLockMode.Locked : CursorLockMode.None;
            HudCanvas.SetActive(StartMenuCanvas.activeSelf);
            InventoryCanvas.SetActive(false);

            StartMenuCanvas.SetActive(!StartMenuCanvas.activeSelf);
        }
    }

    public void ToggleSpellInventory()
    {
        if (player)
        {
            Cursor.lockState = InventoryCanvas.activeSelf ? CursorLockMode.Locked : CursorLockMode.None;
            HudCanvas.SetActive(InventoryCanvas.activeSelf);
            StartMenuCanvas.SetActive(false);

            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
        }
    }
}
