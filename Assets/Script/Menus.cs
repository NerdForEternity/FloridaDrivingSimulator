using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject howToPlayScreen;

    [Header("Score & Timer")]
    public int playerScore = 0;
    public float winScore = 5000f;
    public bool timerExpired = false; // You can set this from your timer script later

    private bool gameEnded = false;
    public static bool GameIsPaused = false;

    void Update()
    {
        if (gameEnded) return;

        // Open or close pause menu with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
                Pause();
            else
                Resume();
        }

        // Win check
        if (playerScore >= winScore)
        {
            ShowWinScreen();
        }

        // Lose check
        if (timerExpired)
        {
            ShowLoseScreen();
        }
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    void ShowWinScreen()
    {
        if (gameEnded) return;

        gameEnded = true;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void ShowLoseScreen()
    {
        if (gameEnded) return;

        gameEnded = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        howToPlayScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ShowHowToPlay()
    {
        howToPlayScreen.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        howToPlayScreen.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HubLevel"); // Replace with actual hub scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
