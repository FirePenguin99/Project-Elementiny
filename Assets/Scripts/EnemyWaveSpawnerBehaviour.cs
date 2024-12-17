using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyWaveSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject chaserPrefab;
    [SerializeField] private GameObject shooterPrefab;
    
    public enum enemyTypes {
        Chaser,
        Shooter,
    }

    public Transform spawnPoint;

    // first number is the Chasers, second number is Shooters e.g. {Chasers, Shooters}
    public int[,] waveArray = {{4,1},{5,2},{6,3},{5,5},{10,5}};
    public int waveNumber = 0;
    public List<GameObject> currentEnemies = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<enemyTypes, GameObject> enemyTypeToPrefabDict = new Dictionary<enemyTypes, GameObject>
        {
            { enemyTypes.Chaser, chaserPrefab },
            { enemyTypes.Shooter, shooterPrefab },
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemies.Count == 0 && waveNumber < waveArray.GetLength(0)) {
            SpawnWave();
            print("$1500 Cardiac frames");
        } else {
        }
    }

    public void SpawnWave() { // may have to make this a coroutine to add delay between enemy spawns?
        for (int i = 0; i < waveArray[waveNumber, 0]; i++)
        {
            GameObject newEnemy = Instantiate(chaserPrefab, spawnPoint.position + new Vector3( Random.Range(-10, 10), 0, Random.Range(-10, 10) ), spawnPoint.rotation);
            
            // add component to enemy thats sole purpose is to connect it to this class and it's specific wave
            EnemyWaveLink waveLink = newEnemy.AddComponent<EnemyWaveLink>();
            waveLink.ewsb = this;

            currentEnemies.Add(newEnemy);
        }
        for (int i = 0; i < waveArray[waveNumber, 1]; i++)
        {
            GameObject newEnemy = Instantiate(shooterPrefab, spawnPoint.position + new Vector3( Random.Range(-10, 10), 0, Random.Range(-10, 10) ), spawnPoint.rotation);
            
            // add component to enemy thats sole purpose is to connect it to this class and it's specific wave
            EnemyWaveLink waveLink = newEnemy.AddComponent<EnemyWaveLink>();
            waveLink.ewsb = this;

            currentEnemies.Add(newEnemy);
        }

        waveNumber += 1;
    }
}
