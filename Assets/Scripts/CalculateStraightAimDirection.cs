using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDirection : MonoBehaviour
{
    [SerializeField] private LayerMask projectileLayerMask;
 
    public virtual Vector3 CalculateAimDirection() {
        Ray ray = GameStateHandler.instance.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen

        Vector3 aimPosition;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectileLayerMask)) {
            aimPosition = hit.point;
        } else {
            aimPosition = ray.GetPoint(100); // if the ray hasnt hit anything, just point if far away from the player
        }

        return aimPosition;
    }
}
