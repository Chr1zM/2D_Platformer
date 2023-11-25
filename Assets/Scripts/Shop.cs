using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {
    public void BackToMainMenu() {
        Debug.Log("Load Shop Scene");
        SceneManager.LoadScene("MainMenu");
    }
}
