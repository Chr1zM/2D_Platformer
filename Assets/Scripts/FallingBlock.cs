using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fallSpeed = 12f;
    [SerializeField] private float returnSpeed = 4f;
    [SerializeField] private float standTime = 1f;
    [SerializeField] private LayerMask resetGround;

    private BoxCollider2D collider2d;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 initPosition;
    private bool isFalling = false;
    private bool hasHitGround = false;
    private bool playerIsNear = false;
    private bool isJustReseted = false;

    private enum BlockState { Idle, Hit, Ready }
    private BlockState blockState = BlockState.Idle;

    private void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initPosition = transform.position;
    }

    private void Update()
    {
        HandleFallingAndReturning();
        UpdateAnimationState();
    }

    private void HandleFallingAndReturning()
    {
        if (isFalling)
        {
            rb.velocity = new Vector2(0, -fallSpeed);
        }
        else
        {
            if (hasHitGround)
            {
                StartCoroutine(StandForAWhile());
            }
            else if (IsOnInitPosition())
            {
                if (playerIsNear)
                {
                    isFalling = true;
                    playerIsNear = false;
                }
            }
            else
            {
                if (playerIsNear)
                {
                    StartCoroutine(StartFalling());
                    playerIsNear = false;
                }
                MoveToInitPosition();
            }
        }
    }

    private void MoveToInitPosition()
    {
        transform.position = Vector2.Lerp(transform.position, initPosition, Time.deltaTime * returnSpeed);
    }

    private IEnumerator StartFalling()
    {
        yield return new WaitForSeconds(standTime);
        isFalling = true;
    }

    private IEnumerator StandForAWhile()
    {
        yield return new WaitForSeconds(standTime);
        hasHitGround = false;
        isJustReseted = true;
    }

    private void UpdateAnimationState()
    {
        if (hasHitGround)
            blockState = BlockState.Hit;
        else if (IsOnInitPosition() && isJustReseted)
        {
            blockState = BlockState.Ready;
            isJustReseted = false;
        }
        else
            blockState = BlockState.Idle;

        animator.SetInteger("blockState", (int)blockState);
    }

    private bool IsOnInitPosition()
    {
        return Vector2.Distance(transform.position, initPosition) < 0.01f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.01f, resetGround))
        {
            Debug.Log("RockHead: Hit the Ground");
            hasHitGround = true;
            isFalling = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
