using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 MoveAction;
    private int moveSpeed = 5;

    public InputAction interactAction;

    public InteractableObject interactableObject;

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
        OnTriggerEnter(interactableObject.GetComponent<BoxCollider>());
    }

    void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + MoveAction * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractableObject>() != null)
        {
            Debug.Log("Interacted with object");
        }
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
