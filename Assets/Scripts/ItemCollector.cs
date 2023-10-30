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

            UpdateTotalFruitCounter(1);

            Debug.Log($"Kiwi collected. fruitsCollected: {fruitsCollected}");
            fruitsText.text = "Fruits: " + fruitsCollected;
        }

        // TODO: implement Bananas
        // Bananas count as 3 fruits
    }

    private void UpdateTotalFruitCounter(int count)
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruits", 0);
        totalFruits += count;
        PlayerPrefs.SetInt("TotalFruits", totalFruits);
        PlayerPrefs.Save();
    }

}
