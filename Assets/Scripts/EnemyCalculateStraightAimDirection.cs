using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCalculateStraightAimDirection : AimDirection
{
    public override Vector3 CalculateAimDirection() {
        return GameStateHandler.instance.player.transform.position;
    }
}
