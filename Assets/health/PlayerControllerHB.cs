using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerHB : MonoBehaviour
{
    public Damage damage;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealthNum -= 1;

            currentHealth -= 31.25f;
            UpdateHealthBar();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealthNum -= amount;
        if (currentHealthNum >= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
