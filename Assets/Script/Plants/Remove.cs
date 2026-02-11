using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    public void OnRemoveButtonClicked()
    {
        if (SpawnManager.Instance == null) return;

        SpawnTrigger trigger = SpawnManager.Instance.selectedTrigger;
        if (trigger == null) return;

        trigger.RemovePlant();
    }
}
