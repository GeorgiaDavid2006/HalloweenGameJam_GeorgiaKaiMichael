using UnityEngine;

public class InteractablePickup : MonoBehaviour, IInteractable
{
    public PlayerController player;
    public string ItemID;

    public void Interact()
    {
        player.items.Add(ItemID);
        player.SetInteractable(null);
        gameObject.SetActive(false);
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
