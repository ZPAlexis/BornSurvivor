using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicManager : MonoBehaviour
{
    public GameObject gameOverScreen, startScreen, escMenuScreen;
    public bool clockIsRunning = false;
    public float clock = 0;
    public TMP_Text clockText, difficultyText;
    public string[] difficulty = {"Easy", "Normal", "Hard", "Very Hard", "Insane", "Impossible"};


    public void Start()
    {
        // Starts the timer automatically
        Time.timeScale = 0;
    }

    void Update()
    {
        if (clockIsRunning)
        {
            clock += Time.deltaTime;
            updateClockDisplay(clock);
            updateDifficultyDisplay(GetDifficulty());
        }
    }


    void updateClockDisplay(float currentClockTime)
    {
        float minutesClock = Mathf.FloorToInt(currentClockTime / 60);
        float secondsClock = Mathf.FloorToInt(currentClockTime % 60);

        clockText.text = string.Format("{0:00}:{1:00}", minutesClock, secondsClock);
    }

    void updateDifficultyDisplay(string difficulty)
    {
        difficultyText.text = difficulty;
    }

    public string GetDifficulty()
    {
        if(clock <= 150) //300
        {
            return difficulty[0];
        }
        if(clock > 150 && clock <= 300) //        if(clock > 300 && clock <= 600)
        {
            return difficulty[1];
        }
        if(clock > 300 && clock <= 450)
        {
            return difficulty[2];
        }
        if(clock > 450 && clock <= 600)
        {
            return difficulty[3];
        }
        if(clock > 600 && clock <= 900)
        {
            return difficulty[4];
        }
        if(clock > 900)
        {
            return difficulty[5];
        }
        return "Impossible";
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        clockIsRunning = false;
    }

    public void startGame()
    {
        startScreen.SetActive(false);
        clockIsRunning = true;
        Time.timeScale = 1;
    }

    public void escMenuOpen()
    {
        escMenuScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void escMenuClose()
    {
        escMenuScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Game closed");
        Application.Quit();
    }

    public void giveFeedback()
    {
    System.Diagnostics.Process.Start("https://forms.gle/mNkKakb9cp1Scmc49");
    }
}