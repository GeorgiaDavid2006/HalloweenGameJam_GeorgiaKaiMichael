using UnityEngine;

public class BookshelfDeposit : MonoBehaviour, IInteractable
{
    public PlayerController player;

    public string itemID1;
    public string itemID2;
    
    public bool receivedBook = false;

    public void Interact()
    {
        if (PlayerController.Instance.items.Contains(itemID1) && receivedBook == false)
        {
            PlayerController.Instance.items.Remove(itemID1);
            receivedBook = true;
            PlayerController.Instance.SetInteractable(null);
        }
        else if (PlayerController.Instance.items.Contains(itemID2) && receivedBook == false)
        {
            PlayerController.Instance.items.Remove(itemID2);
            receivedBook = true;
            PlayerController.Instance.SetInteractable(null);
        }
        else
        {
            return;
        }
    }

    void Start()
    {
        player = PlayerController.Instance;
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
