using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject chaserPrefab;
    
    public enum enemyTypes {
        Chaser,
    }

    public Transform spawnPoint;

    public int[] waveArray = {5,6,7,10,15};
    public int waveNumber = 0;
    public List<GameObject> currentEnemies = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<enemyTypes, GameObject> enemyTypeToPrefabDict = new Dictionary<enemyTypes, GameObject>
        {
            { enemyTypes.Chaser, chaserPrefab },
        };

        // waveArray = new int[] {5,6,7,10,15};
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemies.Count == 0 && waveNumber < waveArray.Length) {
            SpawnWave();
        }
    }

    public void SpawnWave() { // may have to make this a coroutine to add delay between enemy spawns?
        for (int i = 0; i < waveArray[waveNumber]; i++)
        {
            GameObject newEnemy = Instantiate(chaserPrefab, spawnPoint.position + new Vector3( Random.Range(-10, 10), 0, Random.Range(-10, 10) ), spawnPoint.rotation);
            
            // add component to enemy thats sole purpose is to connect it to this class and it's specific wave
            EnemyWaveLink waveLink = newEnemy.AddComponent<EnemyWaveLink>();
            waveLink.ewsb = this;

            currentEnemies.Add(newEnemy);
        }

        waveNumber += 1;
    }
}
