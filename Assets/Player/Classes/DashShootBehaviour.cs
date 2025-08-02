using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShootBehaviour : ShootBehaviour
{
    [SerializeField] private PlayerMovement movement;
    private Rigidbody rb;

    // could be accesed via PlayerMovement, rather than duplicated
    [SerializeField] private Transform orientation;
    private float xInput, yInput;
    private Vector3 moveDirection;

    [SerializeField] private float dashMaxSpeed = 20;
    [SerializeField] private float dashHorizontalForce = 20;
    [SerializeField] private float dashVerticalForce = 2;

    // Start is called before the first frame update
    void Start()
    {
        movement = GameStateHandler.instance.player.GetComponent<PlayerMovement>();
        rb = GameStateHandler.instance.player.GetComponent<Rigidbody>();
        orientation = movement.orientation;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    public override void Fire()
    {
        movement.movementSpeedMax = dashMaxSpeed;

        moveDirection = orientation.forward * yInput + orientation.right * xInput;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // reset gravity for a bit
        rb.AddForce((moveDirection.normalized * dashHorizontalForce) + (transform.up * dashVerticalForce), ForceMode.Impulse);
    }
}
