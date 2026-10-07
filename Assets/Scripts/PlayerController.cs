using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 MoveAction;
    private int moveSpeed = 5;

    public InputAction interactAction;

    private Rigidbody playerRb;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAction = context.ReadValue<Vector3>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

    }

    void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + MoveAction * moveSpeed * Time.fixedDeltaTime);
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
