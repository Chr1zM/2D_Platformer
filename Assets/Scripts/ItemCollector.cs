using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text fruitsText;
    [SerializeField] private AudioSource collectSoundEffect;

    private int fruitsCollected = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiwi counts as 1 fruit
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            fruitsCollected++;

            Debug.Log($"Kiwi collected. fruitsCollected: {fruitsCollected}");
            fruitsText.text = "Fruits: " + fruitsCollected;
        }

        if (collision.gameObject.CompareTag("Banana"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            fruitsCollected+=3;

            Debug.Log($"Banana collected. fruitsCollected: {fruitsCollected}");
            fruitsText.text = "Fruits: " + fruitsCollected;
        }
    }

    /// <summary>
    /// The method is only called when finishing a level.
    /// <para>See: <code>Finish.cs</code></para>
    /// </summary>
    public void UpdateTotalFruitCounter()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruits", 0);
        totalFruits += fruitsCollected; // Add all collected fruits in the level
        PlayerPrefs.SetInt("TotalFruits", totalFruits);
        PlayerPrefs.Save();
    }
}
