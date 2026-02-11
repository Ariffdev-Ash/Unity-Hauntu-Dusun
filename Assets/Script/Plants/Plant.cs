using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 5f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Targeting")]
    public string enemyTag = "Enemy";
    private GameObject currentTarget;

    [Header("Audio")]
    private AudioSource shootAudio;

    private float fireCooldown;

    void Awake()
    {
        shootAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        FindTarget();

        if (currentTarget != null)
        {
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }
            else
            {
                fireCooldown -= Time.deltaTime;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= attackRange)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        currentTarget = nearestEnemy;
    }

    void Shoot()
    {

        if (shootAudio != null)
            shootAudio.Play();

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            Projectile projectile = bullet.GetComponent<Projectile>();
            if (projectile != null && currentTarget != null)
            {
                projectile.SetTarget(currentTarget.transform);
            }
        }
    }




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
