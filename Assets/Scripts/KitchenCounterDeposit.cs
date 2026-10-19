using UnityEngine;

public class KitchenCounterDeposit : MonoBehaviour, IInteractable
{
    public PlayerController player;

    public string itemID1;
    public string itemID2;
    public string itemID3;

    public bool receivedItem1 = false;
    public bool receivedItem2 = false;
    public bool receivedItem3 = false;
    public void Interact()
    {
        if (PlayerController.Instance.items.Contains(itemID1)) 
        {
            PlayerController.Instance.items.Remove(itemID1);
            receivedItem1 = true;
            PlayerController.Instance.SetInteractable(null);
        }

        if(PlayerController.Instance.items.Contains(itemID2))
        {
            PlayerController.Instance.items.Remove(itemID2);
            receivedItem2 = true;
            PlayerController.Instance.SetInteractable(null);
        }

        if(PlayerController.Instance.items.Contains(itemID3))
        {
            PlayerController.Instance.items.Remove(itemID3);
            receivedItem3 = true;
            PlayerController.Instance.SetInteractable(null);
        }

        if(receivedItem1 && receivedItem2 && receivedItem3)
        {

        }
    }

    void Start()
    {
        player = PlayerController.Instance;
    }
}
