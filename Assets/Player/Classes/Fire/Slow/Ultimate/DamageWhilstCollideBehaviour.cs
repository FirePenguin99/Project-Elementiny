using UnityEngine;

public class DamageWhilstCollideBehaviour : TickWhilstCollideBehaviour
{
    [SerializeField] private int damageOnTick = 20;

    public override void tickFunction(GameObject enemy)
    {
        print("damage tick fo " + damageOnTick * chargeMultiplier + " damage");
        enemy.GetComponent<HealthBehaviour>()?.DamageHealth(damageOnTick * chargeMultiplier);
    }
}
