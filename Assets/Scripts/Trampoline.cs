using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce = 30f;
    [SerializeField] private float cooldownTime = 1f;

    private Animator animator;

    private bool canBounce = true;
    private bool jump = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canBounce && collision.gameObject.CompareTag("Player"))
        {
            BouncePlayer(collision.gameObject);
        }
    }

    private void BouncePlayer(GameObject player)
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        Vector2 bounceDirection = CalculateBounceDirection(player.transform.position);

        playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

        canBounce = false;
        jump = true;
        animator.SetBool("jump", jump);
        StartCoroutine(Cooldown());
    }

    private Vector2 CalculateBounceDirection(Vector2 playerPosition)
    {
        return (playerPosition - (Vector2)transform.position).normalized;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canBounce = true;
        jump = false;
        animator.SetBool("jump", jump);
    }
}
