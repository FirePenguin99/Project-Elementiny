using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningReleaseBehaviour : MonoBehaviour
{
    public static List<GameObject> chargedEnemies = new List<GameObject>();
    public ChargeDebuffBehaviour highestChargeStackComponent = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            print("Charge Released!");

            FindAllChargedEnemies();
            ReleaseAllChargedEnemies();
        }
    }

    public void FindAllChargedEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<ChargeDebuffBehaviour>() != null) {
                chargedEnemies.Add(enemy);
                
                if (highestChargeStackComponent == null) {
                    highestChargeStackComponent = enemy.GetComponent<ChargeDebuffBehaviour>();
                } else if(enemy.GetComponent<ChargeDebuffBehaviour>().chargeStackCount > highestChargeStackComponent.chargeStackCount) {
                    highestChargeStackComponent = enemy.GetComponent<ChargeDebuffBehaviour>();
                }
            }
        }
    }

    public void ReleaseAllChargedEnemies() {
        foreach (GameObject enemy in chargedEnemies)
        {
            ChargeDebuffBehaviour chargeDebuff = enemy.GetComponent<ChargeDebuffBehaviour>();
            highestChargeStackComponent.entityHealth.health -= chargeDebuff.ReleaseCharge(highestChargeStackComponent.chargeStackCount);
        }
        chargedEnemies.Clear();
        highestChargeStackComponent = null;
    }
}
