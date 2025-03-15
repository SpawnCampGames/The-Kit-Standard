using SPWN;
using UnityEngine;

public class OldInput : MonoBehaviour
{
    [Header("Player Inputs")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode attackKey = KeyCode.F;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private int mouseForwardButton = 3;
    [SerializeField] private int mouseBackwardButton = 4;

    [Header("UI Inputs")]
    [SerializeField] private KeyCode UI_Up = KeyCode.UpArrow;
    [SerializeField] private KeyCode UI_Down = KeyCode.DownArrow;
    [SerializeField] private KeyCode UI_Left = KeyCode.LeftArrow;
    [SerializeField] private KeyCode UI_Right = KeyCode.RightArrow;

    [SerializeField] private KeyCode submitKey = KeyCode.Return;
    [SerializeField] private KeyCode cancelKey = KeyCode.Escape;

    [SerializeField] private int leftClick = 0;
    [SerializeField] private int rightClick = 1;
    [SerializeField] private int middleClick = 2;

    private void Start()
    {
        Dbug.Info($"* Indicates Old Input System");
    }

    private void Update()
    {
        // Check for continuous movement input (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // If there is any movement input, call OnMove
        if(moveX != 0 || moveY != 0)
        {
            OnMove(moveX,moveY);
        }

        if(Input.GetKeyDown(jumpKey)) OnJump();
        if(Input.GetKeyDown(attackKey)) OnAttack();
        if(Input.GetKeyDown(interactKey)) OnInteract();
        if(Input.GetKeyDown(crouchKey)) OnCrouch();
        if(Input.GetKeyDown(sprintKey)) OnSprint();

        if(Input.GetMouseButtonDown(mouseForwardButton)) OnMouseForward();
        if(Input.GetMouseButtonDown(mouseBackwardButton)) OnMouseBackward();

        // Handling the navigation keys (Up, Down, Left, Right Arrow)
        if(Input.GetKeyDown(UI_Up)) OnNavigateUp();
        if(Input.GetKeyDown(UI_Down)) OnNavigateDown();
        if(Input.GetKeyDown(UI_Left)) OnNavigateLeft();
        if(Input.GetKeyDown(UI_Right)) OnNavigateRight();

        if(Input.GetKeyDown(submitKey)) OnSubmit();
        if(Input.GetKeyDown(cancelKey)) OnCancel();

        if(Input.GetMouseButtonDown(leftClick)) OnClick();
        if(Input.GetMouseButtonDown(rightClick)) OnRightClick();
        if(Input.GetMouseButtonDown(middleClick)) OnMiddleClick();

        if(Input.GetAxis("Mouse ScrollWheel") != 0) OnScrollWheel();

        OnPoint(); // mouse position
    }

    private void OnMove(float moveX,float moveY)
    {
        //floods console
        //Dbug.Input($"* Move: X = {moveX}, Y = {moveY}");
        // Example: Move the player based on the axis input
        Vector3 movement = new Vector3(moveX,0,moveY).normalized;
    }

    private void OnJump()
    {
        Dbug.Input($"* Jump {jumpKey.ToString()} pressed");
    }

    private void OnAttack()
    {
        Dbug.Input($"* Attack {attackKey.ToString()} pressed");
    }

    private void OnInteract()
    {
        Dbug.Input($"* Interact {interactKey.ToString()} pressed");
    }

    private void OnCrouch()
    {
        Dbug.Input($"* Crouch {crouchKey.ToString()} pressed");
    }

    private void OnSprint()
    {
        Dbug.Input($"* Sprint {sprintKey.ToString()} pressed");
    }

    private void OnMouseForward()
    {
        Dbug.Input($"* Mouse Forward Button pressed");
    }

    private void OnMouseBackward()
    {
        Dbug.Input($"* Mouse Backward Button pressed");
    }

    private void OnNavigateUp()
    {
        Dbug.Input($"* Navigate Up Arrow pressed");
    }

    private void OnNavigateDown()
    {
        Dbug.Input($"* Navigate Down Arrow pressed");
    }

    private void OnNavigateLeft()
    {
        Dbug.Input($"* Navigate Left Arrow pressed");
    }

    private void OnNavigateRight()
    {
        Dbug.Input($"* Navigate Right Arrow pressed");
    }
    private void OnSubmit()
    {
        Dbug.Input($"* Submit {submitKey.ToString()} pressed");
    }

    private void OnCancel()
    {
        Dbug.Input($"* Cancel {cancelKey.ToString()} pressed");
    }

    private void OnPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        // floods console
        // Dbug.Input($"* Mouse Position: {mousePosition}");
    }

    private void OnClick()
    {
        Dbug.Input($"* Click (Mouse0) clicked");
    }

    private void OnRightClick()
    {
        Dbug.Input($"* Click (Mouse1) clicked");
    }

    private void OnMiddleClick()
    {
        Dbug.Input($"* Middle Click (Mouse2) clicked");
    }

    private void OnScrollWheel()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        Dbug.Input($"* Scroll Wheel {scrollDelta} detected");
    }
}
