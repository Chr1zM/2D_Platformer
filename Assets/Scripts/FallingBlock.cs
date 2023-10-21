using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D collider2d;

    private Vector2 initialPosition;
    private bool falling = false;

    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private LayerMask resetGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            rigidbody2D.velocity = new Vector2(0, -fallSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            falling = true;
            StartCoroutine(ResetBlock());
        }
    }


    IEnumerator ResetBlock()
    {
        yield return new WaitForSeconds(resetTime);
        falling = false;
        rigidbody2D.velocity = Vector2.zero;
        transform.position = initialPosition;
    }
}
