using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Script goes onto Camera GameObject //

    public Transform cameraPosition;

    public void UpdateCameraPosition() {
        transform.position = cameraPosition.position;
        transform.rotation = cameraPosition.rotation;
    }
}
