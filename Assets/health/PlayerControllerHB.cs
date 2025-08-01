using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerHB : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private float maxHealth = 250f;
    private float currentHealth;
    public int healthNumMax = 8;
    public int currentHealthNum;

    void Start()
    {
        currentHealth = maxHealth;
        currentHealthNum = healthNumMax;
        UpdateHealthBar();
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
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}