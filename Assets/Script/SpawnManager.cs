using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    
    public SpawnTrigger selectedTrigger;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SelectTile(SpawnTrigger trigger)
    {
        selectedTrigger = trigger;
    }

    public void SpawnObjectWithCost(GameObject prefab, int cost)
    {
        if (selectedTrigger == null || prefab == null) return;
        if (CurrencyManager.Instance == null) return;

        if (CurrencyManager.Instance.playerCurrency < cost)
            return;

        CurrencyManager.Instance.AddCurrency(-cost);

        // Remove old plant if exists
        if (selectedTrigger.currentPlant != null)
        {
            selectedTrigger.RemovePlant();
        }

        Vector3 spawnPos =
            selectedTrigger.transform.position + new Vector3(0f, 0.7f, 0f);

        GameObject plant = Instantiate(prefab, spawnPos, Quaternion.identity);
        selectedTrigger.MarkAsSpawned(plant);
    }
}
