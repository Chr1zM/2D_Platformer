using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource finishSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            finishSoundEffect.Play();
            DisablePlayerMovement(collision.gameObject.GetComponent<PlayerMovement>());

            Invoke("CompleteLevel", 2f);
        }
    }

    private void DisablePlayerMovement(PlayerMovement playerMovement)
    {
        playerMovement.SetMovementEnabled(false);
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Load next Level");
    }

}
