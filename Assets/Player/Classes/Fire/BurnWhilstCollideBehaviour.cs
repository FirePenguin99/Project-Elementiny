using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BurnWhilstCollideBehaviour : TickWhilstCollideBehaviour
{
    [SerializeField] private int addedBurnStack = 2;

    public override void tickFunction(GameObject enemy)
    {
        FireElementClass.ApplyBurn(enemy, addedBurnStack);
    }
}
