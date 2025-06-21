using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    [SerializeField] private InteractBoxBehaviour interactBox;
    private InteractableBehaviour closestInteractable;

    // public int idk;
    // public int bruh
    // {
    //     get { return idk; }
    //     set
    //     {
    //         // idk = value;
    //         idk = value < 0 ? 0 : value;
    //     }
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (closestInteractable) // turn off the old closest
        {
            closestInteractable.isUiActive = false;
        }

        closestInteractable = null;
        float closestInteractDistance = Mathf.Infinity;
        foreach (InteractableBehaviour interactable in interactBox.interactablesInRange)
        {
            float distance = Vector3.Distance(interactable.gameObject.transform.position, interactBox.gameObject.transform.position);
            if (distance < closestInteractDistance)
            {
                closestInteractable = interactable;
                closestInteractDistance = distance;
            }
        }

        if (closestInteractable)
        {
            closestInteractable.isUiActive = true;
        }

    }

    void Interact()
    {
        if (closestInteractable)
        {
            interactBox.interactablesInRange.Remove(closestInteractable);
            closestInteractable.onInteract();
        }
    }
}
