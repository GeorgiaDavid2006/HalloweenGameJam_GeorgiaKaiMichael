using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public PlayerController player;
    public string KeyID;

    public bool canGet = true;

    private void Start()
    {
        player = PlayerController.Instance;
    }

    private void Update()
    {
        if (PlayerController.Instance.keys.Contains(KeyID))
        {
            gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if (canGet == true)
        {
            player.keys.Add(KeyID);
            player.SetInteractable(null);
            gameObject.SetActive(false);
        }
        else
        {
            return;
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
