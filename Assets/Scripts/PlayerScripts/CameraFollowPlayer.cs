using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    // Script goes onto Camera GameObject //

    public Transform cameraPosition;

    void Update() {
        transform.position = cameraPosition.position; // Camera should copy the Position's position (to move in space)
    }

    void LateUpdate() {
        cameraPosition.rotation = transform.rotation; // Position should copy the Camera's rotation (to have the weapons in its hierarchy rotate (Could seperate the Weapon Holder from the CameraPosition and move this line into a new script for the Weapon Holder))
    }
}
