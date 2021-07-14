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
    public float jumpForce = 5f;

    [Header("Climb Properties")]
    public float climbSpeed = 3f;

    [Header("Status Flags")]
    public bool isOnGround;
    public bool isJumping;
    public bool isClimbing;

    // component references
    PlayerInput input;
    CapsuleCollider2D bodyCollider;
    Rigidbody2D rigidBody;

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

        originalXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        float xVelocity = moveSpeed * input.horizontal;

        if (xVelocity * direction < 0f) FlipCharacterDirection();

        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        if (isOnGround) coyoteTime = Time.time + coyoteDuration;
    }

    private void FlipCharacterDirection()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x = originalXScale * direction;
        transform.localScale = scale;
    }
}
