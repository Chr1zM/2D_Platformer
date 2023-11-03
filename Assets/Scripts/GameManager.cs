using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text totalFruitsText;

    // Start is called before the first frame update
    void Start()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruits", 0);
        totalFruitsText.text = "TOTAL FRUITS: " + totalFruits;
    }

}
