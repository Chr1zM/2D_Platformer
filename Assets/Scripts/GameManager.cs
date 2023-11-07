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
        ResetPlayerPrefs();

        int totalFruits = PlayerPrefs.GetInt("TotalFruits", 0);
        totalFruitsText.text = "TOTAL FRUITS: " + totalFruits;

        if (!PlayerPrefs.HasKey("SelectedPlayerSkin"))
        {
            PlayerPrefs.SetString("SelectedPlayerSkin", "VirtualGuy");
            PlayerPrefs.Save();
        }
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayerPrefsReset", 0);

        /* 
         * Uncomment this, if all the stats should be reseted
         * 
        if (PlayerPrefs.GetInt("PlayerPrefsReset", 0) == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("PlayerPrefsReset", 1);
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs wurden zurückgesetzt.");
        }
        */
    }

}
