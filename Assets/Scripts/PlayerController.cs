using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public GameManager gameManager;

    public Vector3 MoveAction;
    private int moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;

    public InputAction interactAction;

    public IInteractable currentInteractable = null;

    private Rigidbody playerRb;
    private Camera mainCamera;

    public Animator playerAnimator;
    public Transform playerModel;

    public List<string> keys;
    public List<string> items;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();

        Instance = this;

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

        if (MoveAction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MoveAction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandlePlayerAnimations()
    {
        if (MoveAction != Vector3.zero)
        {
            playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (interactable == null) Debug.Log("Current interactable set to null");
        else Debug.Log($"Current interactable set to {interactable}");

        currentInteractable = interactable;
    }

    void Update()
    {
        HandlePlayerAnimations();
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
    }
}
