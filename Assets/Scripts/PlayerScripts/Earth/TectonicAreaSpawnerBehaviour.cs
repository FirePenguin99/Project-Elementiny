using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TectonicAreaSpawnerBehaviour : MonoBehaviour
{
    public GameObject tectonicAreaPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) { 
            SpawnTectonicArea();
        }
    }

    public void SpawnTectonicArea() {
        GameObject newTectonicArea = Instantiate(tectonicAreaPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
    }
}
