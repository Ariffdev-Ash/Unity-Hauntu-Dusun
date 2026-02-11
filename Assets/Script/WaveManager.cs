using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public float spawnInterval = 1.5f;
    public float waveDuration = 10f;
    public int currencyRewardPerWave = 50;
    private int currentWave = 1;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI currentWaveText;
    public float textDisplayTime = 2f;

    [Header("Lighting")]
    public Light2D globalLight;
    public float dimIntensity = 0.1f;
    public float normalIntensity = 1f;
    public float fadeDuration = 0.5f;

    private bool isWaveActive = false;
    private PlayerHP playerHP;

    [Header("Audio")]
    public AudioSource waveMusic;
    public AudioSource backgroundMusic;

    [Header("Spawn Scaling")]
    public float spawnIntervalDecreasePerWave = 0.15f;
    public float minimumSpawnInterval = 0.1f;



    // ✅ List to track spawned enemies
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerHP = player.GetComponent<PlayerHP>();
    }

    void Start()
    {
        UpdateCurrentWaveText();
    }

    public void StartWave()
    {
        if (!isWaveActive)
            StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        isWaveActive = true;

        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        if (waveMusic != null && !waveMusic.isPlaying)
        {
            waveMusic.Play();
        }

        if (globalLight != null)
            StartCoroutine(FadeLight(dimIntensity, fadeDuration));

        // Show wave incoming message
        if (waveText != null)
        {
            waveText.text = "Wave " + currentWave + " Incoming!";
            waveText.gameObject.SetActive(true);
            yield return new WaitForSeconds(textDisplayTime);
            waveText.gameObject.SetActive(false);
        }

        // Spawn enemies for this wave
        float timer = 0f;
        while (timer < waveDuration)
        {
            SpawnEnemy();
            float currentSpawnInterval = Mathf.Max(minimumSpawnInterval,
              spawnInterval - (currentWave - 1) * spawnIntervalDecreasePerWave);

            SpawnEnemy();
            yield return new WaitForSeconds(currentSpawnInterval);
            timer += currentSpawnInterval;

        }

        // Wave ended
        isWaveActive = false;

        //Stop wave music
        if (waveMusic != null && waveMusic.isPlaying)
        {
            waveMusic.Stop();
        }
        
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }


        RemoveAllEnemies();

        if (globalLight != null)
            StartCoroutine(FadeLight(normalIntensity, fadeDuration));

        // Show wave ended message
        if (waveText != null)
        {
            waveText.text = "Wave " + currentWave + " Ended!";
            waveText.gameObject.SetActive(true);
            yield return new WaitForSeconds(textDisplayTime);
            waveText.gameObject.SetActive(false);
        }

        // Reward player
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCurrency(currencyRewardPerWave);
        }

        // Heal between waves
        if (playerHP != null)
        {
            playerHP.RestoreFullHealth();
        }

        Debug.Log("Wave " + currentWave + " ended.");

        currentWave++;
        UpdateCurrentWaveText();

        yield return new WaitForSeconds(3f);
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject enemyToSpawn;

        if (currentWave <= 2)
        {
            enemyToSpawn = enemyPrefabs[0];
        }
        else if (currentWave <= 4 && enemyPrefabs.Length >= 2)
        {
            enemyToSpawn = enemyPrefabs[Random.Range(0, 2)];
        }
        else
        {
            enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }

        // ✅ Spawn and add to list
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoints[spawnIndex].position, Quaternion.identity);
        activeEnemies.Add(spawnedEnemy);
    }

    void RemoveAllEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        activeEnemies.Clear();
    }

    void UpdateCurrentWaveText()
    {
        if (currentWaveText != null)
        {
            currentWaveText.text = "Current Wave: " + currentWave;
        }
    }

    // ⭐ Smooth light fade
    IEnumerator FadeLight(float target, float duration)
    {
        float start = globalLight.intensity;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }

        globalLight.intensity = target;
    }
}
