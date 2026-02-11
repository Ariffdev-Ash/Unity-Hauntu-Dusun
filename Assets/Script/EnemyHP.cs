using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currencyReward = 10; // Amount of currency to give on death
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
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
        // Give currency to player
        CurrencyManager.Instance.AddCurrency(currencyReward);

        // You can add animation or effects here
        Destroy(gameObject);
    }
}
