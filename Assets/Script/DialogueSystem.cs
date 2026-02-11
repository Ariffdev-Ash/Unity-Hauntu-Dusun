using UnityEngine;
using TMPro;
using System.Collections;

public class DelayedDialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private float delayBeforeStart = 10f; // 10 sec delay
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private int currentLineIndex = 0;

    void Start()
    {
        // Clear text at start
        dialogueText.text = "";

        // Start dialogue after delay
        StartCoroutine(StartDialogueAfterDelay());
    }

    IEnumerator StartDialogueAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart); // Wait 10 secs
        StartDialogue();
    }

    void StartDialogue()
    {
        if (dialogueLines.Length == 0) return;
        currentLineIndex = 0;
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (isTyping)
        {
            // Skip to end if typing
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
            return;
        }

        if (currentLineIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
        currentLineIndex++;
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // Auto-proceed to next line after a short delay (optional)
        yield return new WaitForSeconds(1.5f);
        DisplayNextLine();
    }

    void EndDialogue()
    {
        dialogueText.text = ""; // Clear when done
    }

    // Optional: Manual control via player input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }
}