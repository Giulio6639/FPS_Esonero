using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Impostazioni Spawn")]
    public GameObject enemyPrefab;       
    public float spawnInterval = 5f;     

    [Header("Punti di Spawn")]
    public Transform[] spawnPoints;     

    [Header("Riferimenti")]
    public EnemyManager enemyManager;    

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null)
        {
            Debug.LogWarning("Manca il Prefab del nemico o i punti di spawn non sono stati assegnati!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.enemyManager = this.enemyManager;

            enemyManager.AddEnemy(enemyScript);
        }
    }
}