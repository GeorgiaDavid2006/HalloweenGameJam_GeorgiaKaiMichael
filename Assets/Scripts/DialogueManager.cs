using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> dialogueQueue;

    public GameManager gameManager;
    public PlayerController playerController;

    public bool inDialogue = false;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences)
    {
        GameManager.instance.uiManager.ShowDialoguePanel();
        inDialogue = true;

        foreach (string currentString in sentences)
        {
            dialogueQueue.Enqueue(currentString);
        }

        DisplayNextString();

        Debug.Log($"Interacted with {gameObject.name}");
    }

    public void DisplayNextString()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
        }
        else if (dialogueQueue.Count > 0)
        {
            GameManager.instance.uiManager.SetDialogueText(dialogueQueue.Dequeue());
        }
    }

    private void EndDialogue()
    {
        dialogueQueue.Clear();
        GameManager.instance.uiManager.HideDialoguePanel();
        inDialogue = false;
    }

}
