using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SmoulderingCorpseBehaviour : FireElementClass
{
    public int burnStackCount = 0;
    public float tickRate = 1f;

    public int burnTimer;

    bool allowSpreadInvoke = true; //this stops multiple Invokes from being played at the same time

    public float spreadRadius = 10f;
    LayerMask spreadLayerMask;

    public Collider[] enemiesInRange;
    
    void OnDrawGizmos() {
        Gizmos.color = new Color(1f,0f,0f,0.1f);
        
        Gizmos.DrawSphere(transform.position, spreadRadius);
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        spreadLayerMask = LayerMask.GetMask("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        if (allowSpreadInvoke) {
            Invoke(nameof(InvokeSpreadBurn), tickRate);
        
            if (burnTimer <= 0) {
                // spreadParticlesPrefab.GetComponent<FlameParticleBehaviour>().EndFlame();
                Destroy(this.gameObject);
            }
            burnTimer -= 1;
        
            allowSpreadInvoke = false;
        }
    }

    private void InvokeSpreadBurn() {
        SpreadBurn(spreadRadius, spreadLayerMask);
        
        allowSpreadInvoke = true;
    }
}
