using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    public int playerCurrency = 0;

    public TextMeshProUGUI currencyText; // Drag your UI text here in the Inspector

    void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCurrencyUI();
    }

    public void AddCurrency(int amount)
    {
        playerCurrency += amount;
        UpdateCurrencyUI();
        Debug.Log("Balance: " + playerCurrency);
    }

    void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = "Balance: " + playerCurrency.ToString();
        }
    }
}
