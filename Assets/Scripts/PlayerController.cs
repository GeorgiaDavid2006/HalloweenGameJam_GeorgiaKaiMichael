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
        if (currentInteractable != null)
        {
            Debug.Log("Interacted with object");
        }
    }

    void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + MoveAction * moveSpeed * Time.fixedDeltaTime);
    }

    void SetInteractable(IInteractable interactable)
    {
        interactable = currentInteractable;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            SetInteractable(other.GetComponent<IInteractable>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        SetInteractable(null);
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
