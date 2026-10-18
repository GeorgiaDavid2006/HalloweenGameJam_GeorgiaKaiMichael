using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    public Vector3 MoveAction;
    private int moveSpeed = 5;

    public InputAction interactAction;

    public IInteractable currentInteractable = null;

    private Rigidbody playerRb;
    private Camera mainCamera;

    public List<string> keys;
    public List<string> items;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        interactAction.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAction = context.ReadValue<Vector3>();
        Quaternion cameraYRotation = new Quaternion(0, mainCamera.transform.rotation.y, 0, mainCamera.transform.rotation.w);
        MoveAction = cameraYRotation * MoveAction;
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
        if (GameManager.instance.dialogueManager.inDialogue == true)
        {
            return;
        }
        else
        {
            playerRb.MovePosition(playerRb.position + MoveAction * moveSpeed * Time.fixedDeltaTime);
        }
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
