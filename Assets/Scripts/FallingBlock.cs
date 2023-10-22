using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider2d;
    private Vector2 initialPosition;

    private Animator animator;

    private bool falling = false;

    [SerializeField] private float fallSpeed = 12f;
    [SerializeField] private float returnSpeed = 4f;
    [SerializeField] private LayerMask resetGround;

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
            rb.velocity = new Vector2(0, -fallSpeed);
        else
            transform.position = Vector2.Lerp(transform.position, initialPosition, Time.deltaTime * returnSpeed);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.01f, resetGround))
        {
            // TODO Animation on Hit
            animator.SetTrigger("hit");
            ResetBlock();
        }
            
    }

    private bool isOnInitPosition()
    {
        float positionDifferenceX = Mathf.Abs(transform.position.x - initialPosition.x);
        float positionDifferenceY = Mathf.Abs(transform.position.y - initialPosition.y);

        // TODO Animation "Blink" for Player Info that the Block is now "Activated" again
        animator.SetTrigger("reset");
        return positionDifferenceX < 0.1f && positionDifferenceY < 0.1f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isOnInitPosition())
        {
            falling = true;
        }
    }

    private void ResetBlock()
    {
        falling = false;
        rb.velocity = Vector2.zero;
    }
}
