using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpellObject", menuName = "SpellScriptableObjects")]
public class SpellScriptableObject : ScriptableObject
{
    public string spellName;
    public string description;
    public GameStateHandler.classes element;
    public GameObject weaponPrefab;

    public Sprite sprite;
}
