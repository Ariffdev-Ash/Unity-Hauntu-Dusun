using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 1f;
    public float detectionRadius = 2f;
    public float avoidanceForce = 2f;
    public LayerMask obstacleLayer;

    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Attack")]
    public int damage = 1;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private bool isPlayerInContact;

    private Transform player;
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    private Vector3 originalScale;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (player != null)
        {
            playerCollider = player.GetComponent<Collider2D>();
        }

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        originalScale = transform.localScale;
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (player == null) return;

        FacePlayer();

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector2 direction = CalculateMovementDirection();
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (isPlayerInContact && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
        }
    }

    void FacePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void AttackPlayer()
    {
        if (playerCollider != null)
        {
            PlayerHP playerHealth = playerCollider.GetComponent<PlayerHP>();
            if (playerHealth != null)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(damage);
                Debug.Log("Player took damage: " + damage);

                // Destroy enemy immediately after dealing damage
                Destroy(gameObject);

                // Optional: reset cooldown (not necessary if enemy disappears)
                lastAttackTime = Time.time;
            }
        }
    }

    Vector2 CalculateMovementDirection()
    {
        Vector2 desiredDirection = (player.position - transform.position).normalized;
        Vector2 avoidanceDirection = Vector2.zero;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, desiredDirection, detectionRadius, obstacleLayer);

        if (hit.collider != null)
        {
            Vector2 dir1 = Vector2.Perpendicular(hit.normal);
            Vector2 dir2 = -Vector2.Perpendicular(hit.normal);

            if (Vector2.Dot(dir1, desiredDirection) > Vector2.Dot(dir2, desiredDirection))
            {
                avoidanceDirection = dir1 * avoidanceForce;
            }
            else
            {
                avoidanceDirection = dir2 * avoidanceForce;
            }
        }

        return (desiredDirection + avoidanceDirection).normalized;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInContact = true;
            AttackPlayer(); // Enemy disappears immediately after damage
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInContact = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
