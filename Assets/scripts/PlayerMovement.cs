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
    [SerializeField]
    private AudioClip footstepClip;
    [SerializeField]
    private AudioSource footSteps;

    [SerializeField]
    private float stepDelay = 0.5f; // Time between footsteps

    [SerializeField]
    private SoundCollection soundCollection;

    private float stepTimer = 0f;

    private void Start()
    {
        stepTimer = stepDelay;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        HandleInput();

        // Set drag
        rb.linearDamping = grounded ? groundDrag : 0f;

        bool isMoving = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                soundCollection.Play(footSteps,footstepClip,true);
                stepTimer = stepDelay;
            }
        }
        else
        {
            stepTimer = stepDelay;
        }
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
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        Vector3 moveDir = moveDirection;

        // Project movement on slope if grounded
        if (grounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, whatIsGround))
            {
                moveDir = Vector3.ProjectOnPlane(moveDir, hit.normal).normalized;
            }
        }
        else
        {
            moveDir = moveDir.normalized;
        }

        rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
    }
}
