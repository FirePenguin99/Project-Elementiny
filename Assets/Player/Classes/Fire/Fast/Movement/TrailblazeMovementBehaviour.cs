using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailblazeMovementBehaviour : ShootBehaviour
{
    [SerializeField] private PlayerMovement movement;

    [SerializeField] private GameObject firePoolPrefab;
    [SerializeField] private float runningMaxSpeed = 20;
    [SerializeField] private float dashTimer = 5;
    [SerializeField] private float firePoolInterval = 0.1f;
    [SerializeField] private float playerHeight = 1.45f;

    private Coroutine changeSpeed;
    private bool isCoroutineRunning = false;
    private Transform orientation;

    void Start()
    {
        movement = GameStateHandler.instance.player.GetComponent<PlayerMovement>();
        orientation = movement.orientation;
    }

    public override void Fire()
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(changeSpeed);
        }
        changeSpeed = StartCoroutine("ChangeSpeed");
    }

    IEnumerator ChangeSpeed()
    {
        isCoroutineRunning = true;
        float defaultSpeed = movement.defaultMovementSpeed;
        movement.defaultMovementSpeed = runningMaxSpeed;

        float timePassed = 0;
        while (timePassed < dashTimer)
        {
            yield return new WaitForSeconds(firePoolInterval);
            timePassed += firePoolInterval;

            Instantiate(firePoolPrefab, new Vector3(transform.position.x, transform.position.y - playerHeight, transform.position.z),
            orientation.transform.rotation);
        }

        movement.defaultMovementSpeed = defaultSpeed;
        isCoroutineRunning = false;
    }
}
