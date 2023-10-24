using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSoundEffect;

    private Rigidbody2D rb;

    private Animator animator;

    private bool dead = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && !dead) Die();
    }

    private void Die()
    {
        dead = true;
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        Debug.Log("Player died");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reload Game");
    }
}
