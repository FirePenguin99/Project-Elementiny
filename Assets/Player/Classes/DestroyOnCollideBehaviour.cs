using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollideBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private GameObject objectToBeDestroyed; // Leave empty if you want to Destroy the object the Component is sitting on

    void OnTriggerEnter(Collider col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            if (objectToBeDestroyed != null)
            {
                Destroy(objectToBeDestroyed);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            if (objectToBeDestroyed != null)
            {
                Destroy(objectToBeDestroyed);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
