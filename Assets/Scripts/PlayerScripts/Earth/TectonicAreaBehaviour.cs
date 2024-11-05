using UnityEngine;

public class TectonicAreaBehaviour : MonoBehaviour
{    
    public int tectonicValue = 0;
    public int quakeThreshold = 100;

    public float earthquakeDamage = 1000;

    private WeaponSwapBehaviour weaponSwap; 
    
    void Awake() {
        weaponSwap = GameStateHandler.instance.player.GetComponent<WeaponSwapBehaviour>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (tectonicValue >= quakeThreshold) {
            Earthquake();
        }
    }

    void Earthquake() {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, transform.localScale.x, LayerMask.GetMask("Enemy"));
        foreach (Collider enemyCol in enemiesInRange)
        {
            HealthBehaviour enemyHealth = enemyCol.gameObject.GetComponent<HealthBehaviour>();
            if (enemyHealth) {
                enemyHealth.health -= earthquakeDamage;
            }
            
        }

        foreach (GameObject weapon in weaponSwap.weaponObjects)
        {
            weapon.GetComponent<WeaponRockthrower>()?.tectonicAreasInside.Remove(this.gameObject); // the use of ? means "only run if GetComponent<WeaponRockthrower>() exists
        }

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            foreach (GameObject weapon in weaponSwap.weaponObjects)
            {
                weapon.GetComponent<WeaponRockthrower>()?.tectonicAreasInside.Add(this.gameObject); // the use of ? means "only run if GetComponent<WeaponRockthrower>() exists
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            foreach (GameObject weapon in weaponSwap.weaponObjects)
            {
                weapon.GetComponent<WeaponRockthrower>()?.tectonicAreasInside.Remove(this.gameObject); // the use of ? means "only run if GetComponent<WeaponRockthrower>() exists
            }
        }
    }
}
