using UnityEngine;

public class ClickToOpenUI2D : MonoBehaviour
{
    public GameObject uiCanvas;
    public LayerMask clickableLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, clickableLayer);

            if (hit.collider == null) return;

           
            SpawnTrigger trigger = hit.collider.GetComponent<SpawnTrigger>();
            if (trigger == null) return;

            // ✅ Select the correct tile
            SpawnManager.Instance.SelectTile(trigger);

            // ✅ Open UI
            if (uiCanvas != null)
                uiCanvas.SetActive(true);
        }
    }
}
