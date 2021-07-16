using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float moveSpeed = 1f;
    public float coyoteDuration = .05f;
    public float maxFallSpeed = -25f;

    [Header("Jump Properties")]
    public float jumpSpeed = 5f;

    [Header("Climb Properties")]
    public float climbSpeed = 3f;

    [Header("Status Flags")]
    public bool isOnGround;
    public bool isMoving;
    public bool isJumping;
    public bool isClimbing;

    // component references
    PlayerInput input;
    CapsuleCollider2D bodyCollider;
    Rigidbody2D rigidBody;
    Animator animator;

    float jumpTime;
    float coyoteTime;
    float playerHeight;
    float originalXScale;
    int direction = 1;

    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;

    // Start is called before the first frame update
    void Start()
    {
        // get component references
        input = GetComponent<PlayerInput>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        originalXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsCheck();
        GroundMovement();
        AirMovement();
    }

    private void PhysicsCheck()
    {
        isOnGround = false;
    }

    private void GroundMovement()
    {
        float xVelocity = moveSpeed * input.horizontal;
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        isMoving = (rigidBody.velocity.x == 0f) ? false : true;
        isOnGround = (rigidBody.velocity.y == 0f) ? true : false;

        // sprite and animation changes
        if (xVelocity * direction < 0f) FlipCharacterDirection();
        animator.SetBool("Running", isMoving);

        if (!isOnGround)
        {
            coyoteTime = Time.time + coyoteDuration;
        } else
        {
            coyoteTime = 0f;
        }
    }

    private void AirMovement()
    {
        isOnGround = true; // temporarily force isOnGround

        if (isOnGround && input.jumpPressed)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += jumpVelocityToAdd;
            Debug.Log(jumpVelocityToAdd);
            isOnGround = false;
            isJumping = true;
        }

        
    }

    private void FlipCharacterDirection()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x = originalXScale * direction;
        transform.localScale = scale;
    }
}
