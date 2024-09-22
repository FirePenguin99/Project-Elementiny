using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCandleBulletBehaviour : FireElementClass
{
    public float projectileLifetime;

    public int addedBurnStack = 15;
    
    void Start()
    {
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision col) {
        // print("collided with " + col.gameObject.name);
        if (col.gameObject.GetComponent<HealthBehaviour>() != null) {
            // print("burned");
            // col.gameObject.GetComponent<HealthBehaviour>().health += -10;
            ApplyBurn(col.gameObject, addedBurnStack);
        } else {
            // print("no health script");
        }

        DestroyProjectile();
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }

    public void StartFlying() {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
    }
}
