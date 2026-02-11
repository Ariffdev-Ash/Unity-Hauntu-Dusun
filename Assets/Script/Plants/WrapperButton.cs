using UnityEngine;
using TMPro;

public class SpawnButton : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int cost;
    public TextMeshProUGUI messageText;
    public float messageDuration = 0.5f;

    public void OnSpawnButtonClicked()
    {
        if (SpawnManager.Instance == null || CurrencyManager.Instance == null)
            return;

        // ?? NEW SYSTEM: get selected tile directly
        SpawnTrigger trigger = SpawnManager.Instance.selectedTrigger;
        if (trigger == null)
            return;

        // Currency check
        if (CurrencyManager.Instance.playerCurrency < cost)
        {
            ShowMessage("Not enough currency!");
            return;
        }

        CurrencyManager.Instance.AddCurrency(-cost);

        // Remove existing plant if any
        if (trigger.currentPlant != null)
        {
            trigger.RemovePlant();
        }

        // Spawn new plant with Y offset
        Vector3 spawnPos =
            trigger.transform.position + new Vector3(0f, 0.7f, 0f);

        GameObject newPlant =
            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // Register plant to this tile
        trigger.MarkAsSpawned(newPlant);
    }

    private void ShowMessage(string message)
    {
        if (messageText == null) return;

        messageText.text = message;
        messageText.gameObject.SetActive(true);
        CancelInvoke();
        Invoke(nameof(HideMessage), messageDuration);
    }

    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
