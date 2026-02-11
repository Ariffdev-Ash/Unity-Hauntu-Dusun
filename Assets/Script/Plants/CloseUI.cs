using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAnywhereToClose : MonoBehaviour
{
    public GameObject canvasToClose;
    private float openTime;

    void OnEnable()
    {
        // Record the time when the UI was opened
        openTime = Time.time;
    }

    void Update()
    {
        if (canvasToClose == null || !canvasToClose.activeSelf)
            return;

        // Wait at least 0.1 seconds before allowing closing
        if (Time.time - openTime < 0.1f)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            // Ignore clicks on UI elements
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            // Close the UI
            canvasToClose.SetActive(false);
        }
    }
}
