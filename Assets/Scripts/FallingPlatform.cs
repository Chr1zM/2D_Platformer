using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallingWaitTimer = 0.5f;
    [SerializeField] private float initialFallDistance = 0.1f;

    private Rigidbody2D rb2d;
    private bool isFalling = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            rb2d.position += Vector2.down * initialFallDistance;
            StartCoroutine(StartFalling());
        }
    }

    private IEnumerator StartFalling()
    {
        isFalling = true;

        yield return new WaitForSeconds(fallingWaitTimer);

        rb2d.isKinematic = false;
    }
}
