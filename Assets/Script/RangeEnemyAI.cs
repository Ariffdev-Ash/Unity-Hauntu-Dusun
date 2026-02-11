using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float stopDistance = 5f;
    public float retreatDistance = 3f;

    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Combat")]
    public int damage = 1;
    public float attackCooldown = 2f;
    public float projectileSpeed = 10f;
    public float attackRange = 8f;
    private float lastAttackTime;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int projectileCount = 1;
    public float spreadAngle = 15f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;

    private Transform player;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Movement logic
        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else if (distanceToPlayer < retreatDistance)
        {
            Vector2 direction = (transform.position - player.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Face the player correctly without affecting scale
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // Attack logic
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // Play effects
        if (muzzleFlash != null) muzzleFlash.Play();
        if (shootSound != null && audioSource != null) audioSource.PlayOneShot(shootSound);

        Vector2 direction = (player.position - firePoint.position).normalized;

        if (projectileCount == 1)
        {
            FireProjectile(direction);
        }
        else
        {
            float angleStep = spreadAngle / (projectileCount - 1);
            float startingAngle = -spreadAngle / 2f;

            for (int i = 0; i < projectileCount; i++)
            {
                float currentAngle = startingAngle + angleStep * i;
                Vector2 spreadDirection = Quaternion.Euler(0, 0, currentAngle) * direction;
                FireProjectile(spreadDirection);
            }
        }
    }

    void FireProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(direction, projectileSpeed, damage);
        }
        else
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
        }

        Destroy(projectile, 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
