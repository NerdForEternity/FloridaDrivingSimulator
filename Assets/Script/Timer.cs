using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 59f;
    public bool timerIsRunning = false;

    public TextMeshProUGUI timerText;
    public GameObject loseScreen;

    private bool gameEnded = false;

    void Start()
    {
        timerIsRunning = true;
        loseScreen.SetActive(false);
    }

    void Update()
    {
        if (!timerIsRunning || gameEnded) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timerIsRunning = false;
            gameEnded = true;
            loseScreen.SetActive(true);
            Time.timeScale = 0f; // pause game
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // to show last second correctly

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
