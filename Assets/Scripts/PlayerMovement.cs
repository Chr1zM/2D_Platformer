using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidbody2d.velocity = new Vector2(dirX * moveSpeed, rigidbody2d.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Player Jump");
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
}
