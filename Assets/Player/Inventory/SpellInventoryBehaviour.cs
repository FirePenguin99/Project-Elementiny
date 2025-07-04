using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventoryBehaviour : MonoBehaviour
{
    public List<SpellScriptableObject> spellInventory = new List<SpellScriptableObject>();
    public List<SpellScriptableObject> rightSpellInventory = new List<SpellScriptableObject>();
    public List<SpellScriptableObject> leftSpellInventory = new List<SpellScriptableObject>();
    public List<SpellScriptableObject> movementSpellInventory = new List<SpellScriptableObject>();
    public List<SpellScriptableObject> ultimateSpellInventory = new List<SpellScriptableObject>();
}
