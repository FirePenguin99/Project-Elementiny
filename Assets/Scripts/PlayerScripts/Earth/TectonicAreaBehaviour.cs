using UnityEngine;

public class TectonicAreaBehaviour : MonoBehaviour
{    
    public float tectonicValue = 0;
    
    [SerializeField] private int quakeThreshold = 100;
    [SerializeField] private float earthquakeDamage = 1000;

    private WeaponSwapBehaviour weaponSwap;

    public bool isIdle = true;
    private float currentIdleDuration = 0;
    [SerializeField] private float decayLimit;
    [SerializeField] private float decayAmount;
    
    void Awake() {
        weaponSwap = GameStateHandler.instance.player.GetComponent<WeaponSwapBehaviour>();
    }

    void Start() {
    }

    void Update() {
        if (tectonicValue >= quakeThreshold) {
            Earthquake();
        }

        if (isIdle) {
            currentIdleDuration += Time.deltaTime;
            if (currentIdleDuration >= decayLimit) {
                tectonicValue -= decayAmount * Time.deltaTime;
                Mathf.Clamp(tectonicValue, 0, Mathf.Infinity);
            }
        } else {
            currentIdleDuration = 0; // a bit poo of an every-frame write, but fuck you i cant be asked.
        }
    }

    void Earthquake() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, transform.localScale.x, LayerMask.GetMask("Enemy"));
        
        foreach (Collider enemyCol in enemiesInRange) {
            HealthBehaviour enemyHealth = enemyCol.gameObject.GetComponent<HealthBehaviour>();
            if (enemyHealth) {
                enemyHealth.health -= earthquakeDamage;
            } 
        }

        foreach (GameObject weapon in weaponSwap.weaponObjects) {
            weapon.GetComponent<WeaponRockthrower>()?.tectonicAreasInside.Remove(this.gameObject); // the use of ? means "only run if GetComponent<WeaponRockthrower>() exists
        }

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            foreach (GameObject weapon in weaponSwap.weaponObjects) {
                weapon.GetComponent<WeaponRockthrower>()?.tectonicAreasInside.Add(this.gameObject); // the use of ? means "only run if GetComponent<WeaponRockthrower>() exists
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            foreach (GameObject weapon in weaponSwap.weaponObjects) {
                weapon.GetComponent<WeaponRockthrower>()?.tectonicAreasInside.Remove(this.gameObject); // the use of ? means "only run if GetComponent<WeaponRockthrower>() exists
            }
        }
    }
}
