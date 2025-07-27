using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton reference

    public GameObject winScreen;

    public int score = 0;
    public int style = 0;
    [SerializeField] private int scoreToWin = 5000;
    public int maxStyle = 100;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI styleText;

    private float lastStyleHitTime = 0f;
    private const float styleDecayDelay = 3f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
        UpdateStyleUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= scoreToWin && winScreen != null)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }

    public void AddStyle(int amount)
    {
        style = Mathf.Clamp(style + amount, 0, maxStyle);
        lastStyleHitTime = Time.time;
        UpdateStyleUI();
    }

    private void Update()
    {
        // Handle style decay over time
        if (Time.time - lastStyleHitTime > styleDecayDelay && style > 0)
        {
            style = Mathf.Max(0, style - 1);
            UpdateStyleUI();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private void UpdateStyleUI()
    {
        if (styleText != null)
            styleText.text = "Style: " + style + " (" + GetStyleRank() + ")";
    }

    public string GetStyleRank()
    {
        if (style >= 100) return "A True Floridian";
        if (style >= 75) return "Aw heck yea";
        if (style >= 50) return "Getting the hang of it";
        if (style >= 25) return "Not used to the humidity?";
        return "Are you a tourist?";
    }
}
