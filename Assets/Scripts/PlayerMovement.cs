using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float moveSpeed = 8f;
    public float coyoteDuration = .05f;
    public float maxFallSpeed = -25f;

    [Header("Jump Properties")]
    public float jumpSpeed = 20f;

    [Header("Climb Properties")]
    public float climbSpeed = 3f;

    [Header("Status Flags")]
    public bool isOnGround;
    public bool isMoving;
    public bool isJumping;
    public bool isClimbable;
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
        ClimbMovement();
        AirMovement();
    }

    private void PhysicsCheck()
    {
        isOnGround = false;
        isClimbable = false;

        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) isOnGround = true;
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) isClimbable = true;
    }

    private void GroundMovement()
    {
        float xVelocity = moveSpeed * input.horizontal;
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        isMoving = (rigidBody.velocity.x == 0f) ? false : true;

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

    private void ClimbMovement()
    {
        if (!isClimbable) return;

        float yVelocity = climbSpeed * input.vertical;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, yVelocity);

        //rigidBody.bodyType = Rigidbody2D;
    }

    private void AirMovement()
    {
        if (isOnGround && input.jumpPressed)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += jumpVelocityToAdd;
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
