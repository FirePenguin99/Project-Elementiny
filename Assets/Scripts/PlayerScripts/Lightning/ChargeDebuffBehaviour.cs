using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDebuffBehaviour : MonoBehaviour
{
    public int chargeStackCount = 0;
    public int releaseDamageMultiplier = 2;
    
    public HealthBehaviour entityHealth;

    // Start is called before the first frame update
    void Awake()
    {
        entityHealth = GetComponent<HealthBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ReleaseCharge(int highestChargeStack) {
        if (highestChargeStack != chargeStackCount) {

            entityHealth.health -= (highestChargeStack - chargeStackCount) * releaseDamageMultiplier;

            print("Release Damaged for: " + (highestChargeStack - chargeStackCount) * releaseDamageMultiplier);

            Destroy(this);

            return (highestChargeStack - chargeStackCount) * releaseDamageMultiplier;
        }
        Destroy(this);
        return 0;
    }
}
