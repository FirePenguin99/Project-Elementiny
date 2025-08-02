using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpellTypeObject", menuName = "SpellTypeScriptableObjects")]
public class SpellTypeScriptableObject : ScriptableObject
{
    public string[] spellType; // close, far, etc...
    public string description;
    public GameStateHandler.classes element;

    public Sprite sprite;

    public SpellScriptableObject weaponSpell;
    public SpellScriptableObject movementSpell;
    public SpellScriptableObject ultimateSpell;
}
