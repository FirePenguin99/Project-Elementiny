using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHelixParentBehaviour : MonoBehaviour
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed*Time.deltaTime, 0, 0));
    }
}
