using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBulletBehaviour : FireElementClass
{
    public float projectileLifetime;

    public int addedBurnStack = 5;
    
    void Start()
    {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider col) {
        // print("collided with " + col.gameObject.name);
        if (col.gameObject.GetComponent<HealthBehaviour>() != null) {
            // print("burned");
            // col.gameObject.GetComponent<HealthBehaviour>().health += -10;
            ApplyBurn(col.gameObject, addedBurnStack);
        } else {
            // print("no health script");
        }
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
