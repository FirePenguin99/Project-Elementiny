using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HealthBehaviour : MonoBehaviour
{
    public float health;
    
    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Death();
        }
    }

    private void Death() {
        if (GetComponent<BurnDebuffBehaviour>() != null) {
            print("burned to death");
            GameObject smoulderingCorpse = Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SmoulderingCorpse.prefab", typeof(GameObject)) ) as GameObject;

            smoulderingCorpse.transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
            smoulderingCorpse.GetComponent<SmoulderingCorpseBehaviour>().burnStackCount = GetComponent<BurnDebuffBehaviour>().burnStackCount;
            smoulderingCorpse.GetComponent<SmoulderingCorpseBehaviour>().burnTimer = GetComponent<BurnDebuffBehaviour>().burnTimer;
            
            RemoveFromWaveList();
            Destroy(this.gameObject);
        } else {
            print("i dead a hell");

            RemoveFromWaveList();
            Destroy(this.gameObject);
        }
    }

    private void RemoveFromWaveList() {
        EnemyWaveLink ewl = this.GetComponent<EnemyWaveLink>();
        if (ewl != null) {
            ewl.RemoveFromWave();
        }
    }
}
