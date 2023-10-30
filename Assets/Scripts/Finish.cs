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
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Load next Level");
        }
        else
        {
            SceneManager.LoadScene(0); // Lade das Hauptmenü
            Debug.Log("All Levels completed. Returning to Main Menu.");
        }
    }

}
