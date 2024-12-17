using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class FlamethrowerBulletBehaviour : FireElementClass
{
    public float projectileLifetime;

    public int addedBurnStack = 5;
    public float antiGravity;

    private Rigidbody rb;
    
    void Start()
    {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb) {rb.AddForce(Vector3.up * antiGravity, ForceMode.Acceleration);} // no idea why antiGravity doesn't have to be negated, it just works?
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
