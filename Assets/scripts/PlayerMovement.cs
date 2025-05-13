using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] 
    private float moveSpeed = 5f;

    [SerializeField] 
    private float groundDrag = 5f;

    [SerializeField]
    private float airMultiplier = 0.5f;

    [Header("Ground Check")]
    [SerializeField]
    private float playerHeight = 2f;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool grounded;

    [SerializeField]
    private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Check if player is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        HandleInput();

        // Apply drag based on grounded state
        rb.linearDamping = grounded ? groundDrag : 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Determine movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        Vector3 moveDir = moveDirection.normalized;

        if (grounded)
        {
            rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(
                moveDir.x * moveSpeed * airMultiplier,
                rb.linearVelocity.y,
                moveDir.z * moveSpeed * airMultiplier
            );
        }
    }
}
