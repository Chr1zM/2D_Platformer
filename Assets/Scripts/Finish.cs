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
            collision.gameObject.GetComponent<ItemCollector>().UpdateTotalFruitCounter();
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
            Debug.Log("Load next Level");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("All Levels completed. Returning to Main Menu.");
            SceneManager.LoadScene("MainMenu");
        }
    }

}
