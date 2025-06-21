using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateSpellPickup : MonoBehaviour
{
    public TMP_Text header;
    public TMP_Text description;
    public Image image;

    private SpellScriptableObject spell;

    void Awake()
    {
        spell = GetComponent<AddSpellToInventoryBehaviour>().spellSO;
        if (spell == null)
        {
            return;
        }

        header.text = spell.spellName;
        description.text = spell.description;
    }

}
