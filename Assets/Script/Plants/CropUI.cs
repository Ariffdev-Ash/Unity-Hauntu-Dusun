using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject uiPanel;
    public GameObject currentPlant;
    public int currencySell = 10;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                ShowUI();
            }
        }
    }

    void ShowUI()
    {
        if (uiPanel == null) return;

        uiPanel.SetActive(true);

        SpawnManager.Instance.SelectTile(this);
    }

    public void MarkAsSpawned(GameObject plant)
    {
        currentPlant = plant;
        uiPanel.SetActive(false);
    }

    public void RemovePlant()
    {
        if (currentPlant != null)
        {
            Destroy(currentPlant);
            currentPlant = null;
            CurrencyManager.Instance.AddCurrency(currencySell);
        }

        uiPanel.SetActive(false);
    }
}
