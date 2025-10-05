using UnityEngine;

public class SpawnWithRayBehaviour : ShootBehaviour
{
    [SerializeField] private LayerMask rayLayerMask;

    public override void Fire()
    {
        Vector3 aimDirection = straightAimDirection.CalculateAimDirection() - shootPoint.position;
        Debug.DrawRay(shootPoint.position, aimDirection, Color.red, 0.1f);

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, aimDirection, out hit, Mathf.Infinity, rayLayerMask))
        {
            GameObject spawnedObj = Instantiate(bulletPrefab);
            spawnedObj.transform.position = hit.point;
            spawnedObj.transform.rotation = Quaternion.LookRotation(hit.normal);

            TickWhilstCollideBehaviour tickBehaviour = spawnedObj.GetComponent<TickWhilstCollideBehaviour>();
            if (tickBehaviour)
            {
                tickBehaviour.chargeMultiplier = chargeMultiplier;
            }
        }
    }
}
