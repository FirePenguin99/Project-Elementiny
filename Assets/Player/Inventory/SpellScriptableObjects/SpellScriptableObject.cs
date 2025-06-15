using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpellObject", menuName = "SpellScriptableObjects")]
public class SpellScriptableObject : ScriptableObject
{
    public string spellName;
    public string description;
    public string[] spellType; // close, far, etc...
    public string element;
    public GameObject weaponPrefab;

    public Image sprite;
}
