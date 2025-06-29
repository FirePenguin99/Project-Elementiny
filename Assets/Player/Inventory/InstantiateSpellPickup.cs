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
        InstantiateUI();
    }

    public void InstantiateUI()
    {
        spell = GetComponent<SpellItemSOReference>().spellObject;
        if (spell == null)
        {
            return;
        }

        header.text = spell.spellName;
        description.text = spell.description;
        if (spell.sprite != null)
        {
            image.sprite = spell.sprite;
        }
    }

}
