using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool movementEnabled = true;

    private float dirX = 0f;
    [Header("Jump")]
    [SerializeField] private float movingSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState { idle, running, jumping, falling, wallsliding }

    [Header("Wall Jump")]
    [SerializeField] private float wallSlidingSpeed = 1f;
    [SerializeField] private float wallJumpDuration = 0.1f;
    [SerializeField] private Vector2 wallJumpForce = new Vector2(10f, 14f);

    private bool isJumping = false;
    private bool isSliding = false;
    private bool isTouchingWall = false;
    private bool isGrounded = false;
    private bool isWallJumping = false;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsMovementEnabled())
        {
            animator.SetInteger("movementState", (int)MovementState.idle);
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        isGrounded = IsGrounded();
        isTouchingWall = IsTouchingWall();

        if (isTouchingWall && !isGrounded && dirX != 0f)
        {
            isSliding = true;

        }
        else
        {
            isSliding = false;
        }

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            Jump();
        }

        if (isSliding)
        {
            WallSlide();
            Debug.Log("Player is WallSliding");
        }

        if (isWallJumping)
        {
            WallJump();
        }
        else
        {
            // Default Moving
            rigidbody2d.velocity = new Vector2(dirX * movingSpeed, rigidbody2d.velocity.y);
        }
    }

    private bool IsGrounded()
        => Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.05f, groundLayer);
    private bool IsTouchingWall()
        => Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, spriteRenderer.flipX ? Vector2.left : Vector2.right, 0.05f, groundLayer);
    private void WallSlide()
        => rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Mathf.Clamp(rigidbody2d.velocity.y, -wallSlidingSpeed, float.MaxValue));
    private void WallJump()
        => rigidbody2d.velocity = new Vector2(-dirX * wallJumpForce.x, wallJumpForce.y);

    private void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Player is Jumping");
            jumpSoundEffect.Play();
            rigidbody2d.velocity = new Vector2(dirX * movingSpeed, jumpForce);

        }
        else if (isSliding)
        {
            Debug.Log("Player is Walljumping");
            jumpSoundEffect.Play();
            isWallJumping = true;
            Invoke("StopWallJump", wallJumpDuration);
        }
        isJumping = false;
    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    private bool IsMovementEnabled() => movementEnabled;
    public void SetMovementEnabled(bool movementEnabled) => this.movementEnabled = movementEnabled;

    private void UpdateAnimationState()
    {
        MovementState movementState;

        if (dirX > 0f)
        {
            movementState = MovementState.running;
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            movementState = MovementState.running;
            spriteRenderer.flipX = true;
        }
        else
        {
            movementState = MovementState.idle;
        }

        if (rigidbody2d.velocity.y > 0.01f)
        {
            movementState = MovementState.jumping;
        }
        else if (rigidbody2d.velocity.y < -0.01f)
        {
            movementState = MovementState.falling;
        }

        if (isSliding)
        {
            movementState = MovementState.wallsliding;
        }
        animator.SetInteger("movementState", (int)movementState);
    }
}
