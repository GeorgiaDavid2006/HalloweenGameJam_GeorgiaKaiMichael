using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public PlayerController player;

    public GameObject door;
    public string keyID;

    private void Start()
    {
        player = PlayerController.Instance;
    }

    public void Interact()
    {
        if (player.keys.Contains(keyID))
        {
            player.SetInteractable(null);
            Destroy(door);
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
