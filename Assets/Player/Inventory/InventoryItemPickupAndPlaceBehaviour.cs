using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemPickupAndPlaceBehaviour : MonoBehaviour, IPointerClickHandler
{
    public static SpellScriptableObject movingSpell = null;
    public static int movingSpellId = 0;
    public static SpellInventoryHotbarHandler.Hotbars movingSpellOriginHotbar;

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
            movingSpellOriginHotbar = currentHotbarPlacement;
            icon.color = new Color(255, 0, 0);
        }
        else if (movingSpell == spell && movingSpellId == id) // clicked on the same one twice, so cancel pickup
        {
            movingSpell = null;
            icon.color = new Color(255, 255, 255);
        }
        else // add to the left of this item
        {
            print("moved to " + spell.spellName);
            AddInsertSpellToHotbar(movingSpellOriginHotbar, currentHotbarPlacement, movingSpellId, id);
            movingSpell = null;
            // don't need to null movingId, as the check is only on movingSpell
        }
    }

    public static void AddInsertSpellToHotbar(SpellInventoryHotbarHandler.Hotbars starthotbar, SpellInventoryHotbarHandler.Hotbars endHotbar, int startPlacementIndex, int endPlacementIndex = -1)
    {
        SpellInventoryBehaviour playerInventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
        if (playerInventory == null)
        {
            print("Player Inventory is null");
            return;
        }

        if (
            endHotbar == SpellInventoryHotbarHandler.Hotbars.Inventory ||
            endHotbar == SpellInventoryHotbarHandler.Hotbars.RightHand ||
            endHotbar == SpellInventoryHotbarHandler.Hotbars.LeftHand ||
            endHotbar == SpellInventoryHotbarHandler.Hotbars.Movement ||
            endHotbar == SpellInventoryHotbarHandler.Hotbars.Ulitmate
        )
        {
            RemoveSpellFromHotbar(playerInventory, starthotbar, startPlacementIndex);
            switch (endHotbar)
            {
                case SpellInventoryHotbarHandler.Hotbars.Inventory:
                    if (endPlacementIndex == -1)
                    {
                        playerInventory.spellInventory.Add(movingSpell);
                    }
                    else
                    {
                        playerInventory.spellInventory.Insert(
                        endPlacementIndex,
                        movingSpell);
                    }
                    break;
                case SpellInventoryHotbarHandler.Hotbars.RightHand:
                    if (endPlacementIndex == -1)
                    {
                        playerInventory.rightSpellInventory.Add(movingSpell);
                    }
                    else
                    {
                        playerInventory.rightSpellInventory.Insert(
                        endPlacementIndex,
                        movingSpell);
                    }
                    break;
                case SpellInventoryHotbarHandler.Hotbars.LeftHand:
                    if (endPlacementIndex == -1)
                    {
                        playerInventory.leftSpellInventory.Add(movingSpell);
                    }
                    else
                    {
                        playerInventory.leftSpellInventory.Insert(
                        endPlacementIndex,
                        movingSpell);
                    }
                    break;
                case SpellInventoryHotbarHandler.Hotbars.Movement:
                    if (endPlacementIndex == -1)
                    {
                        playerInventory.movementSpellInventory.Add(movingSpell);
                    }
                    else
                    {
                        playerInventory.movementSpellInventory.Insert(
                        endPlacementIndex,
                        movingSpell);
                    }
                    break;
                case SpellInventoryHotbarHandler.Hotbars.Ulitmate:
                    if (endPlacementIndex == -1)
                    {
                        playerInventory.ultimateSpellInventory.Add(movingSpell);
                    }
                    else
                    {
                        playerInventory.ultimateSpellInventory.Insert(
                        endPlacementIndex,
                        movingSpell);
                    }
                    break;
            }
            AddSpellToInventoryBehaviour.InvokeInventoryChange();
        }
        else
        {
            if (endHotbar == SpellInventoryHotbarHandler.Hotbars.None)
            {
                print("SHITS NONE MAN");
                return;
            }
            else
            {
                print("skibidy edge case? this shi aint any of the hotbar enums");
                return;
            }
        }
    }

    public static void RemoveSpellFromHotbar(SpellInventoryBehaviour playerInventory, SpellInventoryHotbarHandler.Hotbars hotbar, int spellIndex)
    {
        switch (hotbar)
        {
            case SpellInventoryHotbarHandler.Hotbars.Inventory:
                playerInventory.spellInventory.RemoveAt(spellIndex);
                break;
            case SpellInventoryHotbarHandler.Hotbars.RightHand:
                playerInventory.rightSpellInventory.RemoveAt(spellIndex);
                break;
            case SpellInventoryHotbarHandler.Hotbars.LeftHand:
                playerInventory.leftSpellInventory.RemoveAt(spellIndex);
                break;
            case SpellInventoryHotbarHandler.Hotbars.Movement:
                playerInventory.movementSpellInventory.RemoveAt(spellIndex);
                break;
            case SpellInventoryHotbarHandler.Hotbars.Ulitmate:
                playerInventory.ultimateSpellInventory.RemoveAt(spellIndex);
                break;
            case SpellInventoryHotbarHandler.Hotbars.None:
                print("SHITS NONE MAN, cant remove a spell from NONE dumbass");
                break;
            default:
                print("This shi aint any of the hotbar enums, cant remove a spell if I dont know what the hotbar is, dumbass");
                break;
        }
    }
}
