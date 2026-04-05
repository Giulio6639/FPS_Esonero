using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f;

    public float smallDamage = 1f;
    public float bigDamage = 2f;

    public float fireRate = 1f;
    public float nextTimeToFire;
    private BoxCollider gunTrigger;

    public EnemyManager enemyManager;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    public AudioClip gunShotSound;
    private AudioSource audioSource;

    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }

        if (gunShotSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gunShotSound);
        }

        // danno ai nemici
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    // check del range
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5f)
                        enemy.TakeDamage(smallDamage);
                    else
                        enemy.TakeDamage(bigDamage);
                }
            }
        }

        // reset del timer
        nextTimeToFire = Time.time + fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy) { }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy) { }
    }
}