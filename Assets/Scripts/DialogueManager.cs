using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> dialogueQueue;

    public UIManager uIManager;
    public PlayerController playerController;

    public bool inDialogue = false;

    private void Awake()
    {
        uIManager = GetComponent<UIManager>();
        playerController = GetComponent<PlayerController>();

        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences)
    {
        uIManager.ShowDialoguePanel();
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
            uIManager.SetDialogueText(dialogueQueue.Dequeue());
        }
    }

    private void EndDialogue()
    {
        dialogueQueue.Clear();
        uIManager.HideDialoguePanel();
        inDialogue = false;
    }

}
