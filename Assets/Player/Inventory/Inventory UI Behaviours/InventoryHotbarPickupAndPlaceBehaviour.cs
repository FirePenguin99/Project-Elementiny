using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryHotbarPickupAndPlaceBehaviour : MonoBehaviour, IPointerClickHandler
{
    private SpellInventoryHotbarHandler.Hotbars hotbarType;

    void Start()
    {
        hotbarType = GetComponent<InventoryHotbarBehaviour>().hotbarType;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryItemPickupAndPlaceBehaviour.movingSpell == null) // pickup
        {
            return; // can't pickup a hotbar
        }
        else // add to this hotbar
        {
            print("moved to " + hotbarType);
            InventoryItemPickupAndPlaceBehaviour.AddInsertSpellToHotbar(InventoryItemPickupAndPlaceBehaviour.movingSpellOriginHotbar, hotbarType, InventoryItemPickupAndPlaceBehaviour.movingSpellId);
            InventoryItemPickupAndPlaceBehaviour.movingSpell = null;
        }
    }
}
