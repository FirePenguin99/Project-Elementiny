using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcethrowerBulletBehaviour : MonoBehaviour
{
    public float projectileLifetime;

    public float addedColdStack = 1.5f;
    public float antiGravity;

    private Rigidbody rb;
    
    void Start()
    {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.up * antiGravity, ForceMode.Acceleration); // no idea why antiGravity doesn't have to be negated, it just works?
    }

    void OnTriggerEnter(Collider col) {
        // print("collided with " + col.gameObject.name);
        if (col.gameObject.GetComponent<HealthBehaviour>() != null) {
            // col.gameObject.GetComponent<HealthBehaviour>().health += -10;
            ApplyCold(col.gameObject, addedColdStack);
        } else {
            // print("no health script");
        }
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }

    public void ApplyCold(GameObject targetGameObject, float addedStackCount) {
        // all the code blocks should really be in the ColdDebuffBehaviour class as a method, not here
        if (targetGameObject.GetComponent<ColdDebuffBehaviour>() == null && targetGameObject.GetComponent<HealthBehaviour>() != null) // if there is no ColdDebuffBehaviour, add one
        {
            ColdDebuffBehaviour coldDebuff = targetGameObject.AddComponent<ColdDebuffBehaviour>();
            coldDebuff.coldStackCount = addedStackCount;
            coldDebuff.thawTimer = 10;
            coldDebuff.ApplySlow();
        } else 
        {
            ColdDebuffBehaviour coldDebuff = targetGameObject.GetComponent<ColdDebuffBehaviour>();
            if (coldDebuff.coldStackCount >= 100) {
                coldDebuff.coldStackCount = 100;
                coldDebuff.isFrozen = true;
            } else {
                coldDebuff.coldStackCount += addedStackCount;
            }
            coldDebuff.thawTimer = 10;
            coldDebuff.ApplySlow();
        }
        
    }
}
