using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolOnCollide : MonoBehaviour
{
    [SerializeField] private GameObject poolPrefab;

    void OnCollisionEnter(Collision col)
    {
        // At the moment, this works because the pool prefab is rotated 90 degrees. If I didnt want that, I could instantiate this and THEN rotate it by the 90 degrees
        Instantiate(poolPrefab, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal));

    }
}
