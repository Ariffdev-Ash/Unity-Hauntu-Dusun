using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText; // Assign a "Game Over" UI Text (disabled by default)

    [Header("Audio")]
    public AudioClip damageSound; // Assign the damage sound in Inspector
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        // Play damage sound
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound, 0.03f);

        }

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = $"{currentHealth}";
    }

    void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(true); // Disable player

        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER!";
            gameOverText.gameObject.SetActive(true);
        }

        StartCoroutine(WaitThenGameOver());
    }

    IEnumerator WaitThenGameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(5); 
    }
}
