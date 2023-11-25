using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void StartGame() {
        Debug.Log("Load Level 1");
        SceneManager.LoadScene("Level 1");
    }

    public void ShowShop() {
        Debug.Log("Load Shop Scene");
        SceneManager.LoadScene("Shop");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
