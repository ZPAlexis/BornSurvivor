using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicManager : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void Start()
    {
        Time.timeScale = 1;
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

//     public Text o2Text;
//     public GameObject escMenuScreen;
//     public GameObject optionsMenuScreen;
//     public float score;
//     public bool timerIsRunning = false;
//     public float clock = 0;
//     public TMP_Text clockText;
//     public TMP_Text highsScorePauseText;
//     public TMP_Text highsScoreDeadText;
//     private string formatedScore;
//     public Animator animator;

//     public void Start()
//     {
//         // Starts the timer automatically
//         timerIsRunning = true;
//         Time.timeScale = 1;
//         formatScore(PlayerPrefs.GetFloat("HighScore", 0));
//         highsScorePauseText.text = formatedScore;
//         highsScoreDeadText.text = formatedScore;
//     }

//     void Update()
//     {
//         if (timerIsRunning && score > 0)
//         {
//             clock += Time.deltaTime;
//             updateTimerDisplay(score);
//             updateClockDisplay(clock);
//         }
//         else
//         {
//             score = 0;
//             updateTimerDisplay(score);
//         }

//     }

//     public void addTimer(int scoreToAdd)
//     {
//         score += scoreToAdd;
//     }

//     public void reduceScore()
//     {
//         score -= 1;
//     }

//     public void timeout()
//     {
//         if (timerIsRunning)
//         {
//             timerIsRunning = false;
//             Debug.Log("Time has run out!");
//             animator.SetBool("PlayerOutOfO2", true);
//             FindObjectOfType<AudioManager>().Play("Timeout");
//             gameOver();
//         }
//     }


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


//     void updateTimerDisplay(float currentTimer)
//     {
//         // currentTimer += 1;
//         float minutes = Mathf.FloorToInt(currentTimer / 60);
//         float seconds = Mathf.FloorToInt(currentTimer % 60);

//         o2Text.text = string.Format("{1:00}", minutes, seconds); // old        scoreText.text = timeRemaining.ToString();
//     }

//     void updateClockDisplay(float currentClockTime)
//     {
//         // clock += Time.deltaTime;
//         float minutesClock = Mathf.FloorToInt(currentClockTime / 60);
//         float secondsClock = Mathf.FloorToInt(currentClockTime % 60);

//         clockText.text = string.Format("{0:00}:{1:00}", minutesClock, secondsClock);
//     }

//     void updateHighScore(float currentTimer)
//     {
//         if(currentTimer > PlayerPrefs.GetFloat("HighScore", 0))
//         {
//             formatScore(currentTimer);
//             highsScorePauseText.text = formatedScore;
//             highsScoreDeadText.text = formatedScore;
//             PlayerPrefs.SetFloat("HighScore", currentTimer);
//         }
        
//     }

//     void formatScore(float score)
//     {
//         float minutes = Mathf.FloorToInt(score / 60);
//         float seconds = Mathf.FloorToInt(score % 60);
//         formatedScore = string.Format("{0:00}:{1:00}", minutes, seconds);
//     }

//     [ContextMenu("Reset Score")]
//     public void ResetScore()
//     {
//         PlayerPrefs.DeleteKey("HighScore"); // or .DeleteAll to delete all player prefs
//     }
}