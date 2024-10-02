using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // -- speed stats --
    public float movementSpeedMax;
    public float defaultMovementSpeed; // constant
    [SerializeField] private float speedDropOffRate = 15;

    [SerializeField] float currentPhysicsSpeed; // for debugging

    // -- input --
    float xInput, yInput;
    Vector3 moveDirection;

    // -- Component initialisations --
    Rigidbody rb;
    [SerializeField] private Transform orientation;

    // -- jumping --
    bool isGrounded;
    public float playerHeight;
    public LayerMask groundLayer;
    
    // -- jumping stats --
    [SerializeField] private float jumpForce;
    bool readyToJump = true;
    float jumpCooldown = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        movementSpeedMax = defaultMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        CheckIsGrounded();

        currentPhysicsSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude; // for debugging
    }

    void FixedUpdate() {
        MovePlayer();
        LimitSpeed();
    }

    private void PlayerInput() {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && isGrounded && readyToJump) {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown); //needs to be used this way as a Cooldown is necessary, because without one Jump() would activate every frame the RayCast is under the floor, shooting the player upwards at random speeds
        }
    }
    
    private void MovePlayer() {
        moveDirection = orientation.forward * yInput + orientation.right * xInput;
        rb.AddForce(moveDirection.normalized * defaultMovementSpeed * 10f, ForceMode.Force);
    }
    
    private void LimitSpeed() {
        Vector3 playerVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (playerVelocity.magnitude > movementSpeedMax) {
            Vector3 limitedVelocity = playerVelocity.normalized * movementSpeedMax;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }

        if (movementSpeedMax > defaultMovementSpeed) { // if the speed is above default,
            movementSpeedMax -= Time.deltaTime * speedDropOffRate; // decrease maxMovementSpeed after it been increased above default (almost a kind of air resistance)
            
            // Dont really need the thing below. Theoretically its good but it currently bugs tf out. I'd think I'd have to make it see if the difference between this frame and last frame is 0, if it is set velocity to default (strip extra speed)
            // if (playerVelocity.magnitude < movementSpeedMax) { // if player walks into a wall, set max speed to their now current speed (most likely 0, but will be capped at defaultMovementSpeed)
            //     movementSpeedMax = playerVelocity.magnitude;
            // }
        } else {
            movementSpeedMax = defaultMovementSpeed;
        }
    }

    private void CheckIsGrounded() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        // Debug.Log("isGrouned: " + isGrounded);
    }

    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() { // needs to be here to be Invoked using a Cooldown
        readyToJump = true;
    }

}
