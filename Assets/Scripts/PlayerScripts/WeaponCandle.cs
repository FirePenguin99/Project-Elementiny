using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCandle : MonoBehaviour
{
    public float spawnRate;
    public int maxCandleAmount;
    public float shootForce;
    
    public static List<GameObject> candlesSpawned = new List<GameObject>();
    public bool isCharging = false;
    public bool isReadyToSpawnNext = true;
    
    public GameObject candlePrefab;
    public Camera playerCam;
    public LayerMask projectileLayerMask;
    public Transform shootPoint;


    bool allowInvoke = true; //this stops multiple Invokes from being played at the same time

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isCharging = Input.GetKey(KeyCode.Mouse0);

        if (isCharging && isReadyToSpawnNext && maxCandleAmount > candlesSpawned.Count) {
            isReadyToSpawnNext = false;
            
            GameObject newCandle = Instantiate(candlePrefab, new Vector3(transform.position.x, transform.position.y + (candlesSpawned.Count * 0f), transform.position.z), Quaternion.identity);
            candlesSpawned.Add(newCandle);


            Invoke(nameof(ResetSpawnNextCandleFlag), spawnRate);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            // Vector3 aimDirection = CalculateAimDirection();

            foreach (GameObject candle in candlesSpawned)
            {
                Vector3 aimDirection = CalculateAimDirection() - candle.transform.position;

                candle.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
                candle.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);
                candle.GetComponent<FireCandleBulletBehaviour>().StartFlying();
            }

            candlesSpawned.Clear();
            isCharging = false;
        }
    }

    private void ResetSpawnNextCandleFlag() {
        isReadyToSpawnNext = true;
    }

    private Vector3 CalculateAimDirection() {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen

        Vector3 aimPosition;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectileLayerMask)) {
            aimPosition = hit.point;
        } else {
            aimPosition = ray.GetPoint(100); // if the ray hasnt hit anything, just point if far away from the player
        }

        return aimPosition;
    }
}
