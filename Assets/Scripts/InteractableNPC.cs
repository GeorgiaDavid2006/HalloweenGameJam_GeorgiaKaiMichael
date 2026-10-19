using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public GameManager gameManager;
    public PlayerController player;

    public string itemID;
    public string rewardID;

    public string[] sentences = null;
    public string[] alternateSentences = null;

    public void Interact()
    {
        if (GameManager.instance.dialogueManager.inDialogue == true)
        {
            GameManager.instance.dialogueManager.DisplayNextString();
        }
        else
        {
            if (GameManager.instance.dialogueManager.inDialogue == false && PlayerController.Instance.items.Contains(itemID))
            {
                GameManager.instance.dialogueManager.StartDialogue(alternateSentences);
                PlayerController.Instance.items.Remove(itemID);
                PlayerController.Instance.keys.Add(rewardID);
            }
            else
            {
                GameManager.instance.dialogueManager.StartDialogue(sentences);
            }
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
