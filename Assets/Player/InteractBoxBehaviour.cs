using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBoxBehaviour : MonoBehaviour
{
    public List<InteractableBehaviour> interactablesInRange = new List<InteractableBehaviour>();

    void OnTriggerEnter(Collider col)
    {
        InteractableBehaviour interact = col.gameObject.GetComponent<InteractableBehaviour>();
        if (interact != null)
        {
            interactablesInRange.Add(interact);
        }
    }

    void OnTriggerExit(Collider col)
    {
        InteractableBehaviour interact = col.gameObject.GetComponent<InteractableBehaviour>();
        if (interact != null)
        {
            interact.isUiActive = false;
            interactablesInRange.Remove(interact);
        }
    }
}
