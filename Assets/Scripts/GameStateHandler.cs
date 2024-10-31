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
    public enum classes {
        Fire,
        Lightning,
        Ice,
        Earth
    }

    public GameObject StartMenuCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this) { 
            Destroy(this); 
        } 
        else { 
            instance = this; 
        }
    }

    public void SpawnPlayerAsClass(classes classEnum) {
        if (player) {Destroy(player);}

        // what an overcomplication compared to four if statements, but its cool as fuck.
        Dictionary<classes, GameObject> classToPrefabDict = new Dictionary<classes, GameObject>
        {
            { classes.Fire, playerFirePrefab },
            { classes.Lightning, playerLightningPrefab },
            { classes.Ice, playerIcePrefab },
            { classes.Earth, playerEarthPrefab }
        };

        player = Instantiate(classToPrefabDict[classEnum], spawnPoint.position, spawnPoint.rotation);

        // not the happiest about this use of Find and strings. All object names are subject to change and since there is no reference by object or class, I'd have to *remember* to update these strings
        Transform cameraPos = player.transform.Find("Orientation").transform.Find("CameraPos");
        if (cameraPos == null) { print("GameObject by the name of CameraPos not found!"); }
        
        playerCamera.gameObject.GetComponent<CameraFollowPlayer>().cameraPosition = cameraPos;
    
        StartMenuCanvas.SetActive(false);
    }
}
