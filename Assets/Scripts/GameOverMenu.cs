using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(isActive);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowGameOverMenu()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
        isActive = true;
        gameOverMenu.SetActive(isActive);
    }

    public void RestartGame()
    {
        Debug.Log("Level Restarted");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Debug.Log("Load Main Menu");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public bool isGameOver()
    {
        return isActive;
    }
}
