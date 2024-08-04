using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBulletBehaviour : MonoBehaviour
{
    public float baseRockDamage;
    public float tectonicValue;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        HealthBehaviour healthComponent = col.gameObject.GetComponent<HealthBehaviour>();

        if (healthComponent != null) {
            print("dude damaged for " + baseRockDamage * tectonicValue);
            healthComponent.health -= baseRockDamage * tectonicValue;
        }
        DestroyProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
