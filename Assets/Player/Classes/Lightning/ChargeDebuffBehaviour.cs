using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDebuffBehaviour : MonoBehaviour
{
    public int chargeStackCount = 0;
    public HealthBehaviour entityHealth; // could turn this into a public setter in LightningReleaseBehaviour.cs but thog no care.

    void Awake() {
        entityHealth = GetComponent<HealthBehaviour>();

    }

    public float ReleaseCharge(int highestChargeStack, float releaseDamageMultiplier) {
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
