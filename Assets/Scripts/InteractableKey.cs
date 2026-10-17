using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public PlayerController player;
    public string KeyID;

    public void Interact()
    {
        player.keys.Add(KeyID);
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
