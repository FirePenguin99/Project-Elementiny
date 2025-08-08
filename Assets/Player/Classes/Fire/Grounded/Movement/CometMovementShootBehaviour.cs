using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]

public class CometMovementShootBehaviour : ShootBehaviour
{
    private Rigidbody playerRb;
    private SphereCollider cometCollider;
    private WeaponClass weapon;

    [SerializeField] private float cometForce;
    [SerializeField] private float cometForceTimeout;
    private bool isCometing = false;
    private Vector3 cometTrajectory;
    private float cometForceTimer;

    void OnTriggerEnter(Collider col)
    {
        ExplodeComet();
    }

    void Start()
    {
        playerRb = GameStateHandler.instance.player.GetComponent<Rigidbody>();
        cometCollider = GetComponent<SphereCollider>();
        cometCollider.enabled = false;
        weapon = GetComponent<WeaponClass>();
    }

    void Update()
    {
        if (isCometing && cometForceTimer < cometForceTimeout)
        {
            cometForceTimer += Time.deltaTime;
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(cometTrajectory.normalized * cometForce, ForceMode.VelocityChange);
        }
        else
        {
            cometForceTimer = 0;
            isCometing = false;
        }
    }

    public override void Fire()
    {
        cometTrajectory = straightAimDirection.CalculateAimDirection() - transform.position;
        if (cometTrajectory.y > 0)
        {
            return;
        }
        cometCollider.enabled = true;
        isCometing = true;

        weapon.shotsInMagazine = 0;
    }

    void ExplodeComet()
    {
        isCometing = false;
        cometCollider.enabled = false;
        cometForceTimer = 0;
    }
}
