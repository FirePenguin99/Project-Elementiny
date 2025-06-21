using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpellToInventoryBehaviour : MonoBehaviour
{
    public SpellScriptableObject spellSO;

    public void AddToInventory()
    {
        SpellInventoryBehaviour inventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
        if (inventory)
        {
            print("HOLY MOLY");
            inventory.spellInventory.Add(spellSO);
        }
        else
        {
            print("inventory is null");
        }
    }
}
