using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveTrigger2D : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject waveUIPanel;
    public TextMeshProUGUI waveMessageText;
    public Button startWaveButton;

    [Header("Custom Message")]
    [TextArea] public string message = "Prepare for battle!";

    [Header("Wave Manager Reference")]
    public WaveManager waveManager;

    [Header("Respawn Settings")]
    public float respawnDelay = 5f; // Time before the trigger reappears

    private Collider2D triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (waveUIPanel != null)
            waveUIPanel.SetActive(false);

        if (startWaveButton != null)
            startWaveButton.onClick.AddListener(OnStartWaveClicked);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && waveUIPanel != null)
        {
            waveMessageText.text = message;
            waveUIPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && waveUIPanel != null)
        {
            waveUIPanel.SetActive(false);
        }
    }

    private void OnStartWaveClicked()
    {
        // Start the wave
        if (waveManager != null)
        {
            waveManager.StartWave();
        }

        // Hide the UI
        if (waveUIPanel != null)
            waveUIPanel.SetActive(false);

        // Disable the trigger object and re-enable after a delay
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;       // Disable trigger
            StartCoroutine(ReenableTriggerAfterDelay(respawnDelay));
        }

        // Optional: hide the visual if it has a renderer
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
            rend.enabled = false;
    }

    private IEnumerator ReenableTriggerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Re-enable the trigger collider
        if (triggerCollider != null)
            triggerCollider.enabled = true;

        // Re-enable renderer if present
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
            rend.enabled = true;
    }
}
