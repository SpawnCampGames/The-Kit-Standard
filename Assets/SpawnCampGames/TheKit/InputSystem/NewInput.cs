using UnityEngine;
using UnityEngine.InputSystem;
using SPWN;
using static UnityEngine.InputSystem.DefaultInputActions;

public class NewInput : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference crouchAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference mouseForwardAction;
    [SerializeField] private InputActionReference mouseBackwardAction;

    [SerializeField] private InputActionReference navigateAction;
    [SerializeField] private InputActionReference submitAction;
    [SerializeField] private InputActionReference cancelAction;
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference clickAction;
    [SerializeField] private InputActionReference middleClickAction;
    [SerializeField] private InputActionReference rightClickAction;
    [SerializeField] private InputActionReference scrollWheelAction;

    InputActionReference[] playerActionReferences = new InputActionReference[8];
    InputActionReference[] uiActionReferences = new InputActionReference[8];
    private void Start()
    {
        Dbug.Info($"No Asterisks Indicate New Input System");
    }

    private void OnEnable()
    {
        // handles collecting, enabling, and subscribing
        PlayerInputsInitialization();
        UIInputsInitialization();
    }

    private void OnDisable()
    {
        // unsubscribe
        UnSubscribePlayerInputs();
        UnSubscribeUIInputs();

        // disable
        DisablePlayerInputs();
        DisableUIInputs();
    }

    void PlayerInputsInitialization()
    {
        // add player input actions to collection for loop fun later
        playerActionReferences[0] = moveAction;
        playerActionReferences[1]= jumpAction;
        playerActionReferences[2]= attackAction;
        playerActionReferences[3]= interactAction;
        playerActionReferences[4]= crouchAction;
        playerActionReferences[5]= sprintAction;
        playerActionReferences[6] = mouseForwardAction;
        playerActionReferences[7] = mouseBackwardAction;

        // now enable
        foreach(var action in playerActionReferences)
        {
            EnableAction(action);
        }

        // now subscribe
        SubscribePlayerInputs();
    }

    void SubscribePlayerInputs()
    {
        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
        jumpAction.action.performed += OnJump;
        attackAction.action.performed += OnAttack;
        interactAction.action.performed += OnInteract;
        crouchAction.action.performed += OnCrouch;
        sprintAction.action.performed += OnSprint;
        mouseForwardAction.action.performed += OnMouseForward;
        mouseBackwardAction.action.performed += OnMouseBackward;
    }
    void UnSubscribePlayerInputs()
    {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;
        jumpAction.action.performed -= OnJump;
        attackAction.action.performed -= OnAttack;
        interactAction.action.performed -= OnInteract;
        crouchAction.action.performed -= OnCrouch;
        sprintAction.action.performed -= OnSprint;
        mouseForwardAction.action.performed -= OnMouseForward;
        mouseBackwardAction.action.performed -= OnMouseBackward;
    }

    void DisablePlayerInputs()
    {
        foreach(var action in playerActionReferences)
        {
            DisableAction(action);
        }
    }

    void UIInputsInitialization()
    {
        // add UI action references to collection for loop fun later
        uiActionReferences[0] = navigateAction;
        uiActionReferences[1] = submitAction;
        uiActionReferences[2] = cancelAction;
        uiActionReferences[3] = pointAction;
        uiActionReferences[4] = clickAction;
        uiActionReferences[5] = middleClickAction;
        uiActionReferences[6] = rightClickAction;
        uiActionReferences[7] = scrollWheelAction;

        // now enable
        foreach(var action in uiActionReferences)
        {
            EnableAction(action);
        }

        // now subscribe
        SubscribeUIInputs();
    }

    void SubscribeUIInputs()
    {
        navigateAction.action.performed += OnNavigate;
        submitAction.action.performed += OnSubmit;
        cancelAction.action.performed += OnCancel;
        pointAction.action.performed += OnPoint;
        clickAction.action.performed += OnClick;
        middleClickAction.action.performed += OnMiddleClick;
        rightClickAction.action.performed += OnRightClick;
        scrollWheelAction.action.performed += OnScrollWheel;
    }
    void UnSubscribeUIInputs()
    {
        navigateAction.action.performed -= OnNavigate;
        submitAction.action.performed -= OnSubmit;
        cancelAction.action.performed -= OnCancel;
        pointAction.action.performed -= OnPoint;
        clickAction.action.performed -= OnClick;
        middleClickAction.action.performed -= OnMiddleClick;
        scrollWheelAction.action.performed -= OnScrollWheel;
    }

    void DisableUIInputs()
    {
        foreach(var action in uiActionReferences)
        {
            DisableAction(action);
        }
    }

    void EnableAction(InputActionReference inputAction)
    {
        inputAction.action.Enable();
    }
    void DisableAction(InputActionReference inputAction)
    {
        inputAction.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext k)
    {
        Vector2 movement = k.ReadValue<Vector2>();
        Dbug.Input($"Move");
    }

    private void OnJump(InputAction.CallbackContext k)
    {
        Dbug.Input($"Jump {k.time.ToString("00:00")}");
    }

    private void OnAttack(InputAction.CallbackContext k)
    {
        Dbug.Input($"Attack {k.time.ToString("00:00")}");
    }

    private void OnInteract(InputAction.CallbackContext k)
    {
        Dbug.Input($"Interact {k.time.ToString("00:00")}");
    }

    private void OnCrouch(InputAction.CallbackContext k)
    {
        Dbug.Input($"Crouch {k.time.ToString("00:00")}");
    }

    private void OnSprint(InputAction.CallbackContext k)
    {
        Dbug.Input($"Sprint {k.time.ToString("00:00")}");
    }

    private void OnMouseForward(InputAction.CallbackContext k)
    {
        float scrollValue = k.ReadValue<float>();
        Dbug.Input($"MouseForward {scrollValue} {k.time.ToString("00:00")}");
    }

    private void OnMouseBackward(InputAction.CallbackContext k)
    {
        float scrollValue = k.ReadValue<float>();
        Dbug.Input($"MouseBackward {scrollValue} {k.time.ToString("00:00")}");
    }

    private void OnNavigate(InputAction.CallbackContext k)
    {
        Vector2 navigateDirection = k.ReadValue<Vector2>();
        Dbug.Input($"Navigate {navigateDirection} {k.time.ToString("00:00")}");
    }

    private void OnSubmit(InputAction.CallbackContext k)
    {
        Dbug.Input($"Submit {k.time.ToString("00:00")}");
    }

    private void OnCancel(InputAction.CallbackContext k)
    {
        Dbug.Input($"Cancel {k.time.ToString("00:00")}");
    }

    private void OnPoint(InputAction.CallbackContext k)
    {
        Vector2 pointPosition = k.ReadValue<Vector2>();
        //Dbug.Input($"Point {pointPosition} : {k.time.ToString("00:00")}");
    }

    private void OnClick(InputAction.CallbackContext k)
    {
        float clickValue = k.ReadValue<float>();
        Dbug.Input($"Click {clickValue} {k.time.ToString("00:00")}");
    }

    private void OnMiddleClick(InputAction.CallbackContext k)
    {
        float middleClickValue = k.ReadValue<float>();
        Dbug.Input($"MiddleClick {middleClickValue} {k.time.ToString("00:00")}");
    }

    private void OnRightClick(InputAction.CallbackContext k)
    {
        float rightClickValue = k.ReadValue<float>();
        Dbug.Input($"RightClick {k.time.ToString("00:00")}");
    }

    private void OnScrollWheel(InputAction.CallbackContext k)
    {
        Vector2 scrollDelta = k.ReadValue<Vector2>();
        Dbug.Input($"ScrollDelta {scrollDelta} {k.time.ToString("00:00")}");
    }
}
