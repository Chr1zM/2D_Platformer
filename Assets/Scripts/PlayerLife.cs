using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour {
    [SerializeField] private AudioSource deathSoundEffect;
    private GameOverMenu gameOverMenu;

    private Rigidbody2D rb;

    private Animator animator;

    private bool dead = false;

    // Start is called before the first frame update
    private void Start() {
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Trap") && !dead) Die();
    }

    private void Die() {
        dead = true;
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        Debug.Log("Player died");
    }

    private void GameOver() {
        // This Method is called in the Player Animation "Player_Death" as an Event
        Debug.Log("Game Over!");
        gameOverMenu.ShowGameOverMenu();
    }
}
