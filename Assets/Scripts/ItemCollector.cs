using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text fruitsText;
    [SerializeField] private AudioSource collectSoundEffect;

    private int levelFruits = 0;
    private int fruitsCollected = 0;

    private void Start()
    {
        levelFruits = GameObject.FindGameObjectsWithTag("Kiwi").Length + (GameObject.FindGameObjectsWithTag("Banana").Length * 3);
        UpdateFruitsText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiwi counts as 1 fruit
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            fruitsCollected++;

            UpdateFruitsText();
        }

        // Kiwi counts as 3 fruits
        if (collision.gameObject.CompareTag("Banana"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            fruitsCollected+=3;

            UpdateFruitsText();
        }
    }
    private void UpdateFruitsText()
    {
        fruitsText.text = $"Fruits: {fruitsCollected}/{levelFruits}";
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
