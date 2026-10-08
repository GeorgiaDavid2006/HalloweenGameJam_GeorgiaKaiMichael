using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 MoveAction;
    private int moveSpeed = 5;

    public InputAction interactAction;

    public IInteractable currentInteractable = null;

    private Rigidbody playerRb;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();

        interactAction.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAction = context.ReadValue<Vector3>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + MoveAction * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (interactable == null) Debug.Log("Current interactable set to null");
        else Debug.Log($"Current interactable set to {interactable}");

        currentInteractable = interactable;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
    }
}
