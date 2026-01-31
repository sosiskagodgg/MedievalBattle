using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;


    private PlayerInput playerInput;
    private CharacterController characterController;
    private Vector2 movementInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.Move.Walk.performed += OnMovementPerformed;
        playerInput.Move.Walk.canceled += OnMovementCanceled;
        playerInput.Move.Walk.started += OnMovementStart;

        playerInput.Enable();
    }

    private void OnDestroy()
    {
        playerInput.Move.Walk.performed -= OnMovementPerformed;
        playerInput.Move.Walk.canceled -= OnMovementCanceled;
        playerInput.Move.Walk.started -= OnMovementStart;
        playerInput.Disable();
    }

    private void OnMovementStart(InputAction.CallbackContext context)
    {
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }

    private void Update()
    {
        // Простое движение по осям X и Z
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed;
        characterController.Move(move * Time.deltaTime);
    }
}