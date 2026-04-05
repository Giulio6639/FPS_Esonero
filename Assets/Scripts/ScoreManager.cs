using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private int nextHealThreshold = 100; 
    private PlayerHealth playerHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (score >= nextHealThreshold)
        {
            if (playerHealth != null)
            {
                playerHealth.Heal(20);
            }

            nextHealThreshold += 100;
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Punteggio: " + score;
        }
    }
}