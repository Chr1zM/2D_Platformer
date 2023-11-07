using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private bool isPaused = false;

    private GameOverMenu gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu = FindObjectOfType<GameOverMenu>();
        pauseMenuPanel.SetActive(isPaused);
        continueButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !gameOverMenu.isGameOver())
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    void PauseGame()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0;
        isPaused = true;
        pauseMenuPanel.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        Time.timeScale = 1;
        isPaused = false;
        pauseMenuPanel.SetActive(isPaused);
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
}
