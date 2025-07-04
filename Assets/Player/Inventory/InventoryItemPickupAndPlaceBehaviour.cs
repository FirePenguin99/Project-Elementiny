using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemPickupAndPlaceBehaviour : MonoBehaviour, IPointerClickHandler
{
    public static SpellScriptableObject movingSpell = null;
    public static int movingSpellId = 0;

    public SpellInventoryHotbarHandler.Hotbars currentHotbarPlacement;
    public int id;

    private SpellScriptableObject spell;
    Image icon;

    void Start()
    {
        spell = GetComponent<SpellItemSOReference>().spellObject;
        icon = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (movingSpell == null) // pickup
        {
            print("picked up " + spell.spellName);
            movingSpell = spell;
            movingSpellId = id;
            icon.color = new Color(255, 0, 0);
        }
        else // add to the left of this item
        {
            print("moved to " + spell.spellName);
            InsertSpellToHotbar(currentHotbarPlacement);
            movingSpell = null;
            // don't need to null movingId, as the check is only on movingSpell
            icon.color = new Color(255, 255, 255);
        }
    }

    void AddSpellToHotbar(SpellInventoryHotbarHandler.Hotbars hotbar, SpellScriptableObject spellSO)
    {
        SpellInventoryBehaviour playerInventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
        if (playerInventory == null)
        {
            print("Player Inventory is null");
            return;
        }

        switch (hotbar)
        {
            case SpellInventoryHotbarHandler.Hotbars.Inventory:
                playerInventory.spellInventory.Add(spellSO);
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.RightHand:
                playerInventory.rightSpellInventory.Add(spellSO);
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.LeftHand:
                playerInventory.leftSpellInventory.Add(spellSO);
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.Movement:
                playerInventory.movementSpellInventory.Add(spellSO);
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.Ulitmate:
                playerInventory.ultimateSpellInventory.Add(spellSO);
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.None:
                print("SHITS NONE MAN");
                break;
            default:
                print("skibidy edge case? this shi aint any of the hotbar enums");
                break;
        }
    }
    void InsertSpellToHotbar(SpellInventoryHotbarHandler.Hotbars hotbar)
    {
        SpellInventoryBehaviour playerInventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
        if (playerInventory == null)
        {
            print("Player Inventory is null");
            return;
        }

        switch (hotbar)
        {
            case SpellInventoryHotbarHandler.Hotbars.Inventory:
                playerInventory.spellInventory.RemoveAt(movingSpellId);

                playerInventory.spellInventory.Insert(
                    id,
                    movingSpell
                );
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.RightHand:
                playerInventory.rightSpellInventory.RemoveAt(movingSpellId);

                playerInventory.rightSpellInventory.Insert(
                    id,
                    movingSpell
                );
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.LeftHand:
                playerInventory.leftSpellInventory.RemoveAt(movingSpellId);

                playerInventory.leftSpellInventory.Insert(
                    id,
                    movingSpell
                );
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.Movement:
                playerInventory.movementSpellInventory.RemoveAt(movingSpellId);

                playerInventory.movementSpellInventory.Insert(
                    id,
                    movingSpell
                );
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.Ulitmate:
                playerInventory.ultimateSpellInventory.RemoveAt(movingSpellId);

                playerInventory.ultimateSpellInventory.Insert(
                    id,
                    movingSpell
                );
                AddSpellToInventoryBehaviour.InvokeInventoryChange();
                break;
            case SpellInventoryHotbarHandler.Hotbars.None:
                print("SHITS NONE MAN");
                break;
            default:
                print("skibidy edge case? this shi aint any of the hotbar enums");
                break;
        }
    }
}
