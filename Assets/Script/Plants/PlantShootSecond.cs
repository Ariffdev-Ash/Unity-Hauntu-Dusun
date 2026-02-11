using UnityEngine;

public class CirclePlant : MonoBehaviour
{
    public float attackRange = 5f;
    public float fireRate = 1f;
    public int bulletCount = 12;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public string enemyTag = "Enemy";

    private float fireCooldown;

    void Update()
    {
        if (!EnemyInRange()) return;

        if (fireCooldown <= 0f)
        {
            ShootCircle();
            fireCooldown = 1f / fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    bool EnemyInRange()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange,
            LayerMask.GetMask("Enemy"));
    }

    void ShootCircle()
    {
        float step = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = step * i;
            Quaternion rot = Quaternion.Euler(0, 0, angle);
            Instantiate(projectilePrefab, firePoint.position, rot);
        }
    }
}
