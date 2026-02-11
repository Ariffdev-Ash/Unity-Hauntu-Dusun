using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    private Transform target;

    private Vector2 moveDirection;  // store last known direction

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            moveDirection = (target.position - transform.position).normalized;
        }
    }

    void Update()
    {
        if (target == null)
        {
            // Move in the last stored direction without changing
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
            return;
        }

        // Update direction every frame towards the target
        moveDirection = (target.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
   

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
