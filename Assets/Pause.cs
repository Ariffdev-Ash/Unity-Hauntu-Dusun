using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;
    bool isPaused = false;

    void Update()
    { if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }
}
