using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollideBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;

    void OnTriggerEnter(Collider col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if ((collisionLayers.value & (1 << col.transform.gameObject.layer)) > 0)
        {
            Destroy(this.gameObject);
        }
    }
}
