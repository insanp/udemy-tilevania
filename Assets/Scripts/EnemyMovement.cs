using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float moveSpeed = 1f;
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

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        } else
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);

        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1f);
    }
}
