using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject impactPrefab;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
        {
            Vector3 spawnPos = hit.point + (hit.normal * 0.01f);

            Quaternion spawnRotation = Quaternion.LookRotation(hit.normal);

            if (hit.transform.CompareTag("Enemy"))
            {

                GameObject enemyImpact = Instantiate(impactPrefab, spawnPos, spawnRotation);

                Destroy(enemyImpact, 1f);

                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(25f);
                }

            }
            else
            {

                GameObject impact = Instantiate(impactPrefab, spawnPos, spawnRotation);

                Destroy(impact, 0.01f);
            }

        }
    }
}
