using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance so other scripts can easily access this manager
    public static ScoreManager Instance;

    // UI panel to show when player wins
    public GameObject winScreen;

    // Current score and style values
    public int score = 0;
    public int style = 0;

    // Score needed to win the level
    [SerializeField] private int scoreToWin = 5000;

    // Maximum style value
    public int maxStyle = 100;

    // References to UI text elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI styleText;

    // Tracks time since last style gain for decay
    private float lastStyleHitTime = 0f;
    private const float styleDecayDelay = 3f; // Wait 3 seconds before style starts decaying

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Initialize UI
        UpdateScoreUI();
        UpdateStyleUI();
    }

    // Adds score and checks for win condition
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        // If score reaches target, show win screen and pause game
        if (score >= scoreToWin && winScreen != null)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            GameProgressManager.Instance?.CompleteLevel(SceneManager.GetActiveScene().name);
        }
    }

    // Adds style points and updates last hit time for decay
    public void AddStyle(int amount)
    {
        style = Mathf.Clamp(style + amount, 0, maxStyle);
        lastStyleHitTime = Time.time; // Reset decay timer
        UpdateStyleUI();
    }

    private void Update()
    {
        // Gradually reduce style over time if no recent style gain
        if (Time.time - lastStyleHitTime > styleDecayDelay && style > 0)
        {
            style = Mathf.Max(0, style - 1); // Prevent negative style
            UpdateStyleUI();
        }
    }

    // Update the score text UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // Update the style text UI including the rank description
    private void UpdateStyleUI()
    {
        if (styleText != null)
            styleText.text = "Style: " + style + " (" + GetStyleRank() + ")";
    }

    // Returns a textual rank based on current style
    public string GetStyleRank()
    {
        if (style >= 100) return "A True Floridian";
        if (style >= 75) return "Aw heck yea";
        if (style >= 50) return "Getting the hang of it";
        if (style >= 25) return "Not used to the humidity?";
        return "Are you a tourist?";
    }
}
