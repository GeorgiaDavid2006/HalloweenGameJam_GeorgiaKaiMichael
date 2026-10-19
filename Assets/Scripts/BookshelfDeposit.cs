using UnityEngine;

public class BookshelfDeposit : MonoBehaviour, IInteractable
{
    public PlayerController player;

    public string itemID1;
    public string itemID2;
    public void Interact()
    {
        if (player.items.Contains(itemID1))
        {
            player.items.Remove(itemID1);
            player.SetInteractable(null);
        }
        else if (player.items.Contains(itemID2))
        {
            player.items.Remove(itemID2);
            player.SetInteractable(null);
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
}
