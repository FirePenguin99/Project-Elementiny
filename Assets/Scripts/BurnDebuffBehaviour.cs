using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

public class BurnDebuffBehaviour : FireElementClass
{
    public int burnStackCount = 0;
    public float burnStackDamage = 5f;

    public float burnTimer;
    [SerializeField] private float tickRate = 1f;
    bool allowTickInvoke = true; //this stops multiple Invokes from being played at the same time
    
    [SerializeField] private Collider[] enemiesInRange;
    [SerializeField] private float spreadRadius = 10f;
    LayerMask spreadLayerMask;

    HealthBehaviour entityHealth;
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
        if (allowTickInvoke) {
            Invoke(nameof(BurnDamageTick), tickRate);
        
            if (burnTimer <= 0) {
                ExtinguishBurn();
            }
            burnTimer -= tickRate;
        
            allowTickInvoke = false;
        }
    }

    private void BurnDamageTick() {
        entityHealth.health -= burnStackDamage * burnStackCount;
        print("burned for " + (burnStackDamage * burnStackCount) + " damage");
        
        Invoke(nameof(InvokeSpreadBurn), tickRate);

        allowTickInvoke = true;
    }

    private void InvokeSpreadBurn() {
        SpreadBurn(spreadRadius, spreadLayerMask);
    }

    public void ExtinguishBurn() {
        fireParticlesPrefab.GetComponent<FlameParticleBehaviour>().EndFlame();
        Destroy(this);
    }
}

