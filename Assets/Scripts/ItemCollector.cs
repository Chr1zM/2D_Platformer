using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text kiwisText;

    private int kiwisCollected = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            Destroy(collision.gameObject);
            kiwisCollected++;
            Debug.Log($"Kiwi collected and destroyed. KiwisCollected: {kiwisCollected}");
            kiwisText.text = "Kiwis: " + kiwisCollected;
        }
    }

}
