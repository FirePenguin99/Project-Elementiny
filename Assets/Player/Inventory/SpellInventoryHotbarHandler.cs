using UnityEngine;

public class SpellInventoryHotbarHandler : MonoBehaviour
{
    public InventoryHotbarBehaviour inventoryHotbar;
    public InventoryHotbarBehaviour rightHandHotbar;
    public InventoryHotbarBehaviour leftHandHotbar;
    public InventoryHotbarBehaviour movementHotbar;
    public InventoryHotbarBehaviour ultimateHotbar;

    private SpellInventoryBehaviour playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameStateHandler.instance.player.GetComponent<SpellInventoryBehaviour>();
        inventoryHotbar.hotbarList = playerInventory.spellInventory;
        rightHandHotbar.hotbarList = playerInventory.rightSpellInventory;
        leftHandHotbar.hotbarList = playerInventory.leftSpellInventory;
        movementHotbar.hotbarList = playerInventory.movementSpellInventory;
        ultimateHotbar.hotbarList = playerInventory.ultimateSpellInventory;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
