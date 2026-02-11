using UnityEngine;

public class SpawnRecallUI : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = SpawnManager.Instance;
        if (spawnManager == null)
        {
            Debug.LogWarning("SpawnManager instance not found.");
        }
    }

    public void RecallSpawnedObject()
    {
        if (spawnManager == null)
        {
            Debug.LogWarning("SpawnManager is missing.");
            return;
        }

        SpawnTrigger trigger = spawnManager.selectedTrigger;
        if (trigger == null)
        {
            Debug.Log("No tile selected to recall plant from.");
            return;
        }

        if (trigger.currentPlant == null)
        {
            Debug.Log("No plant to recall on the selected tile.");
            return;
        }

        // Remove the plant properly
        trigger.RemovePlant();

        Debug.Log("Plant recalled successfully from selected tile.");
    }
}
