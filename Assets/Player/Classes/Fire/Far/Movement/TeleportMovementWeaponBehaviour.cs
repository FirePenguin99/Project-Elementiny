using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovementWeaponBehaviour : ShootBehaviour
{
    // Make fire rate really high, and ammo really huge
    [SerializeField] private float maxTeleportDistance = 50;
    [SerializeField] private LayerMask projectileLayerMask;
    [SerializeField] private float playerHeight = 1.45f;

    private Vector3? teleportPosition = null;
    private WeaponClass weapon;
    private GameObject teleportLocator;

    void Start()
    {
        weapon = GetComponent<WeaponClass>();
    }

    public override void Fire()
    {
        if (!teleportLocator) { teleportLocator = Instantiate(bulletPrefab); }
        Ray ray = GameStateHandler.instance.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, maxTeleportDistance, projectileLayerMask))
        {
            teleportPosition = hit.point;
            Debug.DrawLine(transform.position, hit.point, Color.red, 0.1f);

            teleportLocator.transform.position = teleportPosition.Value;
        }
    }

    public override void StopFire()
    {
        if (teleportPosition.HasValue && weapon.shotsInMagazine != 0)
        {
            // TeleportPlayer(teleportPosition.Value);
            // weapon.shotsInMagazine = 0;

            // Destroy(teleportLocator);
            StartCoroutine("TeleportAtEndOfFrame");
        }
    }

    void TeleportPlayer(Vector3 teleportPos)
    {
        GameStateHandler.instance.player.transform.position = new Vector3(teleportPos.x, teleportPos.y + playerHeight, teleportPos.z);
    }

    IEnumerator TeleportAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame(); // WaitUntilEndOfFrame kinda did something, but didnt fix 100% of buggy teleport
        TeleportPlayer(teleportPosition.Value);
        weapon.shotsInMagazine = 0;

        Destroy(teleportLocator);
    }
}
