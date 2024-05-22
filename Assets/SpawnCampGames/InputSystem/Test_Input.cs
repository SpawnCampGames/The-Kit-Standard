using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        // Initialize the input actions
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        // Enable the input actions
        inputActions.Enable();
    }

    private void OnDisable()
    {
        // Disable the input actions
        inputActions.Disable();
    }

    private void Start()
    {
        // Example of referencing an action, add your specific actions as needed
        inputActions.Player.Move.performed += ctx => OnMove(ctx);
        inputActions.Player.Move.canceled += ctx => OnMove(ctx);
        inputActions.Player.Jump.performed += ctx => OnJump();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        Debug.Log($"Move: {movement}");
    }

    private void OnJump()
    {
        Debug.Log("Jump");
    }
}