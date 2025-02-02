using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetimeBehaviour : MonoBehaviour
{
    [SerializeField] private float projectileLifetime = 10;
    [SerializeField] bool countdownStartsOnStart = true;

    void Start()
    {
        if (countdownStartsOnStart) {
            InvokeDestroyProjectile();
        }
    }

    public void InvokeDestroyProjectile() {
        Invoke(nameof(DestroyProjectile), projectileLifetime);
    }

    public void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
