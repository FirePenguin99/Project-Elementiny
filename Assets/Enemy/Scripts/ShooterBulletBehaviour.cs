using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBulletBehaviour : MonoBehaviour
{
    public float projectileLifetime;
    public float projectileDamage;
    
    void Start()
    {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<PlayerHealthBehaviour>() != null) {
            col.gameObject.GetComponent<PlayerHealthBehaviour>().health += -projectileDamage;
            print("Oof");
            DestroyProjectile();
        } else {
            // print("no health script");
        }
    }

    private void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
