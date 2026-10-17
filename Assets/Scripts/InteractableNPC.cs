using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public GameManager gameManager;
    public PlayerController player;

    public string[] sentences = null;

    public void Interact()
    {
        if (GameManager.instance.dialogueManager.inDialogue == true)
        {
            GameManager.instance.dialogueManager.DisplayNextString();
        }
        else
        {
            GameManager.instance.dialogueManager.StartDialogue(sentences);
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
