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

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

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

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rigidbody2d.velocity = new Vector2(dirX, jumpForce);
        }

        UpdateAnimationState();
    }

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

        animator.SetInteger("movementState", (int)movementState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.01f, jumpableGround);
    }

    private bool IsMovementEnabled()
    {
        return movementEnabled;
    }

    public void SetMovementEnabled(bool movementEnabled)
    {
        this.movementEnabled = movementEnabled;
    }
}
