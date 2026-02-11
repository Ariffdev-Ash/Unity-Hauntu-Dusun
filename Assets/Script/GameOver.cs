using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private int previousSceneIndex;

    void Start()
    {
        // Store the previous scene index to use for retry
        previousSceneIndex = PlayerPrefs.GetInt("LastGameScene", 1); // Default to scene 1 if not set
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(3);
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
