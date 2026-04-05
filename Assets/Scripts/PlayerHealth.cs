using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public TextMeshProUGUI healthText;

    void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
    }

    public void PlayerDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        UpdateHealthUI();
        Debug.Log("Curato di " + amount + "! Vita attuale: " + health);
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HEALTH: " + health.ToString();
        }
    }

    private void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }
}