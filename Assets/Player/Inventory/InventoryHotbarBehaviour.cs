using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHotbarBehaviour : MonoBehaviour
{
    public List<SpellScriptableObject> hotbarList;
    [SerializeField] private GameObject itemPrefab;

    void OnEnable()
    {
        UpdateHotbarUI();
        AddSpellToInventoryBehaviour.onInventoryChange += UpdateHotbarUI;
    }
    void OnDisable()
    {
        AddSpellToInventoryBehaviour.onInventoryChange -= UpdateHotbarUI;
    }

    void UpdateHotbarUI()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        foreach (SpellScriptableObject spell in hotbarList)
        {
            GameObject newItem = Instantiate(itemPrefab);
            newItem.transform.SetParent(this.gameObject.transform);

            Image icon = newItem.GetComponent<Image>();
            if (icon)
            {
                icon.sprite = spell.sprite;
            }

            RectTransform rect = newItem.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.transform.localScale = new Vector3(1, 1, 1);
            }

            SpellItemSOReference spellReference = newItem.GetComponent<SpellItemSOReference>();
            if (spellReference != null)
            {
                spellReference.spellObject = spell;
            }
        }
    }
}
