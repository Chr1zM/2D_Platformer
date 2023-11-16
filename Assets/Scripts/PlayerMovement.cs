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
    private float dirX = 0f;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;
    
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private Vector2 wallJumpForce = new Vector2(20f, 14f);
    [SerializeField] private float wallSlideSpeed = 1f;
    private bool isSliding = false;

    [SerializeField] private float moveSpeed = 7f;
    private enum MovementState { idle, running, jumping, falling, wallsliding }

    [SerializeField] private bool movementEnabled = true;

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
        rigidbody2d.velocity = new Vector2(dirX * moveSpeed, rigidbody2d.velocity.y);

        isSliding = false;
        if (IsTouchingWall() && !IsGrounded() && dirX != 0f)
        {
            isSliding = true;
            WallSlide();
            Debug.Log("Player is WallSliding");
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        UpdateAnimationState();
    }

    private bool IsTouchingWall() => Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, spriteRenderer.flipX ? Vector2.left : Vector2.right, 0.01f, jumpableGround);
    private void WallSlide() => rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Mathf.Clamp(rigidbody2d.velocity.y, -wallSlideSpeed, float.MaxValue));
    private bool IsGrounded() => Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.01f, jumpableGround);

    private void Jump()
    {
        if (IsGrounded())
        {
            Debug.Log("Player is Jumping");
            jumpSoundEffect.Play();
            rigidbody2d.velocity = new Vector2(dirX * moveSpeed, jumpForce);

        }
        else if (isSliding)
        {
            Debug.Log("Player is Walljumping");
            jumpSoundEffect.Play();
            rigidbody2d.velocity = new Vector2(-dirX * wallJumpForce.x, wallJumpForce.y);
        }
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
