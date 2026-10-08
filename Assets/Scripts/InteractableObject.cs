using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player)) 
        {
            player.currentInteractable = this;

            Debug.Log("Object can be interacted with");
        }
    }
}
