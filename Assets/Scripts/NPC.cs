using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject questionMark;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public DialogueData dialogueData;  // Reference to the DialogueData ScriptableObject

    private bool playerNearby = false;
    private int dialogueIndex = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        questionMark.SetActive(false);
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        Debug.Log("NPC script initialized.");
    }

    void Update()
    {
        if (playerNearby && Input.GetMouseButtonDown(0) && !isDialogueActive)
        {
            Debug.Log("Player clicked NPC.");
            StartDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger.");
            questionMark.SetActive(true);
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D called.");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger.");
            questionMark.SetActive(false);
            playerNearby = false;
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        Debug.Log("Starting dialogue.");
        dialoguePanel.SetActive(true);
        dialogueIndex = 0;
        isDialogueActive = true;
        DisplayNextMessage();
    }

    void OnNextButtonClicked()
    {
        if (isDialogueActive)
        {
            DisplayNextMessage();
        }
    }

    void DisplayNextMessage()
    {
        if (dialogueIndex < dialogueData.dialogueMessages.Count)
        {
            Debug.Log($"Displaying message {dialogueIndex + 1}/{dialogueData.dialogueMessages.Count}: {dialogueData.dialogueMessages[dialogueIndex]}");
            dialogueText.text = dialogueData.dialogueMessages[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        Debug.Log("Ending dialogue.");
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }
}