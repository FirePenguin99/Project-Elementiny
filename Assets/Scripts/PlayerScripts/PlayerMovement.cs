using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    public Transform orientation;

    float xInput, yInput;
    Vector3 moveDirection;

    Rigidbody rb;

    bool isGrounded;
    public float playerHeight;
    public LayerMask groundLayer;

    public float jumpForce;
    bool readyToJump = true;
    float jumpCooldown = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        CheckIsGrounded();
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

        rb.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }
    
    private void LimitSpeed() {
        Vector3 playerVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (playerVelocity.magnitude > movementSpeed) {
            Vector3 limitedVelocity = playerVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
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
