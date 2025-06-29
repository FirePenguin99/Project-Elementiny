using UnityEngine;

public class AddSpellToInventoryBehaviour : MonoBehaviour
{
    public delegate void OnInventoryChange();
    public static event OnInventoryChange onInventoryChange;

    public void AddToInventory()
    {
        SpellInventoryBehaviour inventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
        SpellScriptableObject spell = GetComponent<SpellItemSOReference>().spellObject;
        if (inventory && spell)
        {
            inventory.spellInventory.Add(spell);

            onInventoryChange?.Invoke();
        }
        else
        {
            print("inventory is null");
        }
    }
}
