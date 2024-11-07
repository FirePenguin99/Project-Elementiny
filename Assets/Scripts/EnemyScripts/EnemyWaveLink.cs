using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveLink : MonoBehaviour
{
    public EnemyWaveSpawnerBehaviour ewsb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveFromWave() {
        ewsb.currentEnemies.Remove(this.gameObject);
    }
}
