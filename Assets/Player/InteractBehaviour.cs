using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject cameraPos;
    private LayerMask detectionLayerMask;


    // Start is called before the first frame update
    void Awake()
    {
        detectionLayerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("E");
            CastInteractBox();
        }
    }

    void CastInteractBox()
    {
        Vector3 boxInfrontOfFace = cameraPos.transform.position + cameraPos.transform.forward * 1.25f;

        Collider[] interactablesInRange = Physics.OverlapBox(boxInfrontOfFace, new Vector3(1.25f, 1.25f, 1.25f), cameraPos.transform.rotation, detectionLayerMask);
        if (interactablesInRange.Length == 0) {
            return;
        }

        InteractableBehaviour firstInteractedBehaviour = interactablesInRange[0].GetComponent<InteractableBehaviour>();
        if (firstInteractedBehaviour) {
            firstInteractedBehaviour.onInteract();
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = new Color(1f, 0f, 0f, 0.1f);

        Gizmos.DrawCube(cameraPos.transform.position + cameraPos.transform.forward * 1.25f, new Vector3(1.25f, 1.25f, 1.25f));
    }
}
