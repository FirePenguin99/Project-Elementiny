using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHotbarBehaviour : MonoBehaviour
{
    private List<SpellScriptableObject> previousHotbarList;
    [SerializeField] private List<SpellScriptableObject> hotbarList = new List<SpellScriptableObject>();
    [SerializeField] private int maxHotbarSize = 0; // 0 means infinite size

    [SerializeField] private GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        previousHotbarList = hotbarList;
        UpdateHotbarUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (hotbarList != previousHotbarList)
        {
            UpdateHotbarUI();
        }
    }

    void UpdateHotbarUI()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        // foreach (GameObject child in this.gameObject.transform.parent)
        // {
        //     Destroy(child);
        // }

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
            if (rect)
            {
                rect.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void AddToHotbar(SpellScriptableObject spell)
    {
        if (hotbarList.Count == 0 || hotbarList.Count < maxHotbarSize)
        {
            hotbarList.Add(spell);
        }
        else
        {
            print("hotbar full");
        }
    }
}
