using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    private Button deleteButton;

    void Start()
    {
        GameObject btnObj = GameObject.Find("Delete");
        if (btnObj != null)
        {
            deleteButton = btnObj.GetComponent<Button>();
            if (deleteButton != null)
            {
                deleteButton.gameObject.SetActive(false); // Optional
            }
            else
            {
                Debug.LogWarning("Button component not found on GameObject named 'Delete'.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject named 'Delete' not found in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && deleteButton != null)
        {
            deleteButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && deleteButton != null)
        {
            deleteButton.gameObject.SetActive(false);
        }
    }
}
