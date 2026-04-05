using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    public float enemyHealth = 2f;
    public float fleeHealthThreshold = 1f;
    public GameObject gunHitEffect;

    [Header("Punteggio")]
    public int pointsValue = 10;

    [Header("Danno al Giocatore")]
    public int damageToPlayer = 10;    
    public float attackCooldown = 1f;   
    private float nextAttackTime;       

    private bool isDead = false;

    void Update()
    {
        if (enemyHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("<color=yellow>Il nemico sta toccando: </color>" + collision.gameObject.name + " (Tag: " + collision.gameObject.tag + ")");

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                PlayerHealth player = collision.gameObject.GetComponentInParent<PlayerHealth>();

                if (player != null)
                {
                    Debug.Log("<color=red>Danno di 10 inflitto al giocatore!</color>");
                    player.PlayerDamage(damageToPlayer);
                    nextAttackTime = Time.time + attackCooldown;
                }
                else
                {
                    Debug.Log("<color=orange>Ho toccato il Player, ma NON trovo lo script PlayerHealth su di lui!</color>");
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log("Nemico colpito! Vita rimanente: " + enemyHealth);

        if (gunHitEffect != null)
        {
            Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        isDead = true;

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(pointsValue);
        }

        if (enemyManager != null)
        {
            enemyManager.RemoveEnemy(this);
        }
        else
        {
            Debug.LogWarning("EnemyManager non assegnato sul nemico, ma lo distruggo lo stesso.");
        }

        Destroy(gameObject);
    }
}