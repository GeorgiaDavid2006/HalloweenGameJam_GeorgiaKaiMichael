using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public GameManager gameManager;
    public PlayerController player;

    public string foodItemID;
    public string collectibleItem1ID;
    public string collectibleItem2ID;
    public string collectibleItem3ID;

    public string rewardID;

    public string[] sentences = null;
    public string[] alternateSentences = null;

    public bool isKitchenGhost = false;
    public bool isGreenHouseGhost = false;
    public bool isLibraryGhost = false;
    public bool isPianistGhost = false;

    public void Interact()
    {
        if (GameManager.instance.dialogueManager.inDialogue == true)
        {
            GameManager.instance.dialogueManager.DisplayNextString();
        }
        else
        {
            if (GameManager.instance.dialogueManager.inDialogue == false && PlayerController.Instance.items.Contains(foodItemID) && isKitchenGhost == true)
            {
                GameManager.instance.dialogueManager.StartDialogue(alternateSentences);
                PlayerController.Instance.items.Remove(foodItemID);
                PlayerController.Instance.keys.Add(rewardID);
            }
            else if (GameManager.instance.dialogueManager.inDialogue == false && PlayerController.Instance.items.Contains(collectibleItem1ID) && PlayerController.Instance.items.Contains(collectibleItem2ID) && PlayerController.Instance.items.Contains(collectibleItem3ID) && isGreenHouseGhost == true)
            {
                GameManager.instance.dialogueManager.StartDialogue(alternateSentences);
                PlayerController.Instance.items.Remove(collectibleItem1ID);
                PlayerController.Instance.items.Remove(collectibleItem2ID);
                PlayerController.Instance.items.Remove(collectibleItem3ID);
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
