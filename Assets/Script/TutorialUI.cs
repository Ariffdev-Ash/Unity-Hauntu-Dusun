using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;   // Assign your panel here
    public Button closeButton;         // Assign your close button

    void Start()
    {
        // Show the panel when the scene starts
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);

        // Assign button click listener
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseTutorial);
    }

    void CloseTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
}
