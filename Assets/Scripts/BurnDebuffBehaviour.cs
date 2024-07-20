using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

public class BurnDebuffBehaviour : FireElementClass
{
    public int burnStackCount = 0;
    public float burnStackDamage = 5f;
    public float tickRate = 1f;

    public int burnTimer;

    HealthBehaviour entityHealth;

    bool allowBurnInvoke = true; //this stops multiple Invokes from being played at the same time

    public float spreadRadius = 10f;
    LayerMask spreadLayerMask;

    public Collider[] enemiesInRange;

    GameObject fireParticlesPrefab;

    void OnDrawGizmos() {
        Gizmos.color = new Color(1f,0f,0f,0.1f);
        
        Gizmos.DrawSphere(transform.position, spreadRadius);
    }

    void Awake()
    {
        entityHealth = GetComponent<HealthBehaviour>();
        spreadLayerMask = LayerMask.GetMask("Enemy");

        fireParticlesPrefab = Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BurningObj.prefab", typeof(GameObject)) ) as GameObject;
        fireParticlesPrefab.transform.SetParent(this.gameObject.transform);
        fireParticlesPrefab.transform.position = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowBurnInvoke) {
            Invoke(nameof(BurnDamageTick), tickRate);
        
            if (burnTimer <= 0) {
                fireParticlesPrefab.GetComponent<FlameParticleBehaviour>().EndFlame();

                Destroy(this);
            }
            burnTimer -= 1;
        
            allowBurnInvoke = false;
        }
    }

    private void BurnDamageTick() {
        entityHealth.health -= burnStackDamage * burnStackCount;
        print("burned for " + (burnStackDamage * burnStackCount) + " damage");
        
        Invoke(nameof(InvokeSpreadBurn), tickRate);

        allowBurnInvoke = true;
    }

    private void InvokeSpreadBurn() {
        SpreadBurn(spreadRadius, spreadLayerMask);
    }
}

