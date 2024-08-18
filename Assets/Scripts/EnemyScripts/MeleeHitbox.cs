using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    LayerMask playerLayerMask;

    public float meleeDamage;

    // Start is called before the first frame update
    void Start()
    {
        playerLayerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<HealthBehaviour>() != null) {
            col.gameObject.GetComponent<HealthBehaviour>().health += -meleeDamage;
            print("Oof");
        } else {
            // print("no health script");
        }
    }
}
