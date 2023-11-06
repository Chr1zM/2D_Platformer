using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float maxTime = 60.0f;
    [SerializeField] private float currentTime;
    [SerializeField] private Text textTimer;
    [SerializeField] private GameOverMenu gameOverMenu;

    private bool isRunning;
    private bool isGameOver;

    // Start is called before the first frame update
    private void Start()
    {
        currentTime = maxTime;
        textTimer.text = currentTime.ToString();
        isRunning = true;
        isGameOver = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            textTimer.text = string.Format("Timer: {0:000}", currentTime);
            if (currentTime <= 0)
            {
                currentTime = 0;
                textTimer.text = "Timer: 0";

                if (!isGameOver)
                {
                    isGameOver = true;
                    gameOverMenu.ShowGameOverMenu();
                    // TODO Start Game Over Music
                    //  Stop BackgroundMusic
                }
            }
        }
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}