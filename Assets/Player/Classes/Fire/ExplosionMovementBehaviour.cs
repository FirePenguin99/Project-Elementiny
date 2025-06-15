using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMovementBehaviour : MonoBehaviour
{
    PlayerMovement playerMov;
    Rigidbody rb;

    // could be accesed via PlayerMovement, rather than duplicated
    [SerializeField] private Transform orientation;
    float xInput, yInput;
    Vector3 moveDirection;

    [SerializeField] private float dashMaxSpeed = 20;
    [SerializeField] private float dashHorizontalForce = 10;
    [SerializeField] private float dashVerticalForce = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerMov = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            playerMov.movementSpeedMax = dashMaxSpeed;

            moveDirection = orientation.forward * yInput + orientation.right * xInput;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // reset gravity for a bit
            rb.AddForce((moveDirection.normalized * dashHorizontalForce) + (transform.up * dashVerticalForce), ForceMode.Impulse);
        }
    }
}
