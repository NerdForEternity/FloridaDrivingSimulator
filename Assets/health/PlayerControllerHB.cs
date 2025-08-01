using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerHB : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject loseScreen; // ? Drag your LoseScreen UI here

    private float maxHealth = 250f;
    private float currentHealth;
    public int healthNumMax = 8;
    public int currentHealthNum;

    void Start()
    {
        currentHealth = maxHealth;
        currentHealthNum = healthNumMax;
        UpdateHealthBar();

        if (loseScreen != null)
            loseScreen.SetActive(false); // Make sure it's hidden at start
    }

    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        currentHealthNum -= amount;
        currentHealth -= (maxHealth / healthNumMax) * amount;

        UpdateHealthBar();

        if (currentHealthNum <= 0)
        {
            if (loseScreen != null)
                loseScreen.SetActive(true); // Show lose screen
            // Optionally disable car movement here
            // Destroy(gameObject); ? don't use this now
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}