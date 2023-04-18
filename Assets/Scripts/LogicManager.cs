using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public bool clockIsRunning = false;
    public float clock = 0;
    public TMP_Text clockText;
    public string[] difficulty = {"Easy", "Normal", "Hard", "Very Hard", "Insane", "Impossible"};


    public void Start()
    {
        // Starts the timer automatically
        clockIsRunning = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (clockIsRunning)
        {
            clock += Time.deltaTime;
            updateClockDisplay(clock);
        }
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

    void updateClockDisplay(float currentClockTime)
    {
        float minutesClock = Mathf.FloorToInt(currentClockTime / 60);
        float secondsClock = Mathf.FloorToInt(currentClockTime % 60);

        clockText.text = string.Format("{0:00}:{1:00}", minutesClock, secondsClock);
    }

    public string GetDifficulty()
    {
        if(clock <= 60) //300
        {
            return difficulty[0];
        }
        if(clock > 60 && clock <= 120) //        if(clock > 300 && clock <= 600)
        {
            return difficulty[1];
        }
        if(clock > 120 && clock <= 180)
        {
            return difficulty[2];
        }
        if(clock > 180 && clock <= 240)
        {
            return difficulty[3];
        }
        if(clock > 240 && clock <= 300)
        {
            return difficulty[4];
        }
        if(clock > 360)
        {
            return difficulty[5];
        }
        return "Impossible";
    }
//     public void escMenuOpen()
//     {
//         escMenuScreen.SetActive(true);
//         Time.timeScale = 0;
//     }

//     public void escMenuClose()
//     {
//         escMenuScreen.SetActive(false);
//         Time.timeScale = 1;
//     }

}