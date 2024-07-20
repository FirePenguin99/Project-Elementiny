using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBulletBehaviour : MonoBehaviour
{
    public float projectileLifetime;
    
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
            print("charged");
            ApplyCharge(col.gameObject);
        } else {
            // print("no health script");
        }
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }

    public void ApplyCharge(GameObject targetGameObject) {
        if (targetGameObject.GetComponent<ChargeDebuffBehaviour>() == null) // if there is no BurnDebuffBehaviour, add one
        {
            ChargeDebuffBehaviour chargeDebuff = targetGameObject.AddComponent<ChargeDebuffBehaviour>();
            chargeDebuff.chargeStackCount = 1;
        } else 
        {
            ChargeDebuffBehaviour chargeDebuff = targetGameObject.GetComponent<ChargeDebuffBehaviour>();
            chargeDebuff.chargeStackCount += 1;
        }
    }
}
