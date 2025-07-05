using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverInventoryItemBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject spellPopup;
    Image icon;

    void Start()
    {
        spellPopup = transform.GetChild(0).gameObject;

        InstantiateSpellPickup popUpUI = GetComponent<InstantiateSpellPickup>();
        if (popUpUI) { popUpUI.InstantiateUI(); }
        // this code below is for handling if the pointer moves off of the Item, but onto the Hover Popup GameObject. Basically just gives the popup a OnPointerExit that disables runs THIS DisablePopUp() method
        HoverInventoryItemPopUpBehaviour popupHover = spellPopup.AddComponent<HoverInventoryItemPopUpBehaviour>();
        popupHover.parentHover = this;

        icon = this.GetComponent<Image>();
    }

    public void DisablePopUp()
    {
        spellPopup.SetActive(false);
        spellPopup.transform.SetParent(transform);
    }
    void EnablePopUp()
    {
        spellPopup.SetActive(true);
        spellPopup.transform.SetParent(transform.parent.parent);
        spellPopup.transform.SetAsLastSibling();
    }

    void OnDisable()
    {
        icon.color = new Color(255, 255, 255);
        spellPopup.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnablePopUp();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DisablePopUp();
    }
}


public class HoverInventoryItemPopUpBehaviour : MonoBehaviour, IPointerExitHandler
{
    public HoverInventoryItemBehaviour parentHover;

    public void OnPointerExit(PointerEventData eventData)
    {
        if (parentHover) { parentHover.DisablePopUp(); }
    }
}


