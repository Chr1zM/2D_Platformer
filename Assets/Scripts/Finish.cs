using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource finishSoundEffect;

    private Rigidbody2D playerRigidbody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            finishSoundEffect.Play();
            playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            
            // TODO Player is stuck in the air when jumping in the FinishLine
            playerRigidbody.bodyType = RigidbodyType2D.Static;

            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
