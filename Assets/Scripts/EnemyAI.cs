using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;
    private NavMeshAgent enemyNavMeshAgent;

    // Riferimento allo script della vita
    private Enemy enemyScript;

    // Quanto lontano prova a scappare il nemico ad ogni frame
    public float fleeDistance = 5f;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = FindObjectOfType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        enemyScript = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemyScript.enemyHealth <= enemyScript.fleeHealthThreshold)
        {
            FleeFromPlayer();
        }
        else if (enemyAwareness.isAggro)
        {
            enemyNavMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }

    private void FleeFromPlayer()
    {
        Vector3 directionAwayFromPlayer = transform.position - playerTransform.position;

        Vector3 fleePosition = transform.position + directionAwayFromPlayer.normalized * fleeDistance;

        enemyNavMeshAgent.SetDestination(fleePosition);
    }
}