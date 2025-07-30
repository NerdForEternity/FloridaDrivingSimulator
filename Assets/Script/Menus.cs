using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public bool timerExpired = false;
    public Button firstSelectedButton;

    private bool gameEnded = false;
    public static bool GameIsPaused = false;
    private CarControls controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new CarControls();
            controls.Driving.Start.performed += ctx => OnStartPressed();
        }
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void OnStartPressed()
    {
        if (gameEnded) return;

        if (!GameIsPaused)
            Pause();
        else
            Resume();
    }

    void Update()
    {
        if (gameEnded) return;

        // Open or close pause menu with Escape key (keyboard)
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
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
        SceneManager.LoadScene("HubScene"); // Replace with actual hub scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}