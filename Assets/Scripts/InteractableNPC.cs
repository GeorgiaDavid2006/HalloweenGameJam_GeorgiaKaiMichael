using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public DialogueManager dialogueManager;

    public string[] sentences = null;

    private void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void Interact()
    {
        if (dialogueManager.inDialogue == true)
        {
            dialogueManager.DisplayNextString();
        }
        else
        {
            dialogueManager.StartDialogue(sentences);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            player.SetInteractable(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            player.SetInteractable(null);
        }
    }
}
