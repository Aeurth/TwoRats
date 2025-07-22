using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePlayerRB : MonoBehaviour
{
    public static event Action<SoundType> PlayerGrounded;
    public Rigidbody rb;
    public float speed;
    public float jumpForce;
    public int maxJumps;
    public Transform cameraTransform;
    public float gravityMultiplier;
    public Animator animator;


    private Vector3 moveDirection;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private Vector3 velocity; // Stores the player's velocity
    private CameraControler cameraControler;

    private void Awake()
    {
        NotifyFalling.Falling += SetFallingState;
    }
    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent unintended rotation
        cameraControler = cameraTransform.GetComponent<CameraControler>();
    }
    private void Update()
    {
        // Separate methods for movement and jumping
        HandleDirection();
        HandleHorizontalMovement();
        HandleJumping();
    }

    private void HandleDirection()
    {
        // Get raw input from WSAD keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate input movement direction
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        //sett direction relative to where camera is facing
        float targetAngle = cameraTransform.eulerAngles.y;
        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * inputDir;
    }

    private void HandleHorizontalMovement()
    {
        if(moveDirection.magnitude < 0.1f) 
        {
            animator.SetBool("isRunning", false);
            return; 
        }
        //handle player object rotation
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        animator.SetBool("isRunning", true);

        // Move the character using Rigidbody.MovePosition
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
    }

    private void HandleJumping()
    {

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

            if (jumpCount == 1)
            {
                Debug.Log("jumped once");
                animator.SetBool("isJumping", true);
            }
            else if (jumpCount == 2)
            {
                Debug.Log("jumped Twice");
                animator.SetBool("isFrontFlipping", true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the character is touching the ground by comparing the collision tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFrontFlipping", false);
            animator.SetBool("isFalling", false) ;

            if (collision.gameObject.layer == 6)
            {
                PlayerGrounded?.Invoke(SoundType.carpetGrounded);
            }
            else
            {
                PlayerGrounded?.Invoke(SoundType.grounded);
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Special"))
        {
            cameraControler.ChangeState(false);
        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("hit");
            rb.velocity = Vector3.zero; // Stop movement
            rb.angularVelocity = Vector3.zero; // Stop rotation
            transform.position = new Vector3(99f, 71f, 95f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the character is leaving contact with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isFalling", true);
        }
    }
    public void SetFallingState(bool state)
    {
        Debug.Log("Falling state changed");
        animator.SetBool("isFalling", state);
        animator.SetBool("isJumping", false);

    }



}

