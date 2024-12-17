using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDebuffBehaviour : MonoBehaviour
{
    public float coldStackCount = 0;
    public float freezeLimit = 100;
    public bool isFrozen = false;
    
    public float thawTimer;
    [SerializeField] private float thawPerTick = 5;
    [SerializeField] private float tickRate = 0.25f;
    bool allowTickInvoke = true; //this stops multiple Invokes from being played at the same time
    
    public float shatterDamage = 10000;
    [SerializeField] private float shatteredCount = 0;

    HealthBehaviour entityHealth;
    EnemyMovementBehaviour enemyMovement;

    [SerializeField] private Collider[] enemiesInRange;
    [SerializeField] private float spreadRadius = 10f;
    LayerMask spreadLayerMask;

    // GameObject fireParticlesPrefab;

    void OnDrawGizmos() {
        Gizmos.color = new Color(1f,0f,0f,0.0f);
        
        Gizmos.DrawSphere(transform.position, spreadRadius);
    }

    void Awake()
    {
        entityHealth = GetComponent<HealthBehaviour>();
        enemyMovement = GetComponent<EnemyMovementBehaviour>();
        spreadLayerMask = LayerMask.GetMask("Enemy");

        // fireParticlesPrefab = Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BurningObj.prefab", typeof(GameObject)) ) as GameObject;
        // fireParticlesPrefab.transform.SetParent(this.gameObject.transform);
        // fireParticlesPrefab.transform.position = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowTickInvoke) {
            Invoke(nameof(Tick), tickRate);
            allowTickInvoke = false;
        }
    }

    private void Tick() {
        if (thawTimer <= 0) {
            coldStackCount -= thawPerTick;
            ApplySlow();
            if (coldStackCount <= 0) {
                EndCold();
            }
        }
        thawTimer -= tickRate;
        allowTickInvoke = true;
    }

    public void ApplySlow() {
        if (!enemyMovement) { return; }
        float speedMultiplier = (-(1/freezeLimit)*coldStackCount) + 1; // y = -(1/100)x + 1. Range is: 0-100, Domain is: 0.00-1.00
        enemyMovement.UpdateSpeeds(speedMultiplier);
    }
    public void RemoveSlow() {
        if (!enemyMovement) { return; }
        enemyMovement.UpdateSpeeds(1);
    }

    public void CheckIsFrozen() {
        if (coldStackCount == 100) {
            isFrozen = true;
        }
    }

    public void ShatterFrozen() {
        if (!isFrozen) { return; } // if is not frozen yet, return

        print("shattered myself, ow");

        isFrozen = false; // immediately set isFrozen to false to avoid recalling this ShatterFrozen function when calling other enemies' ShatterFrozen functions

        entityHealth.health -= shatterDamage;
        shatteredCount += 1;

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spreadRadius, spreadLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name && enemy != null) { // dont apply to itself
                print("shattered another poor sod");
                enemy.gameObject.GetComponent<ColdDebuffBehaviour>().ShatterFrozen();
            }
        }

        // set coldStacks to 50, 75, 87.5, etc...
        coldStackCount = 0;
        for (int i = 1; i < shatteredCount+1; i++)
        {
            coldStackCount += (freezeLimit / 2) / i;
        }

        ApplySlow();
    }

    public void EndCold() {
            // fireParticlesPrefab.GetComponent<FlameParticleBehaviour>().EndFlame();
            RemoveSlow();
            Destroy(this);
        }
}
