using UnityEngine;
using SPWN;
using Debug = UnityEngine.Debug;

/// <summary>
/// Simple Character Controller
/// Includes: Walking, Running, and Jumping
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class SpawnCampControllerLite : MonoBehaviour
{

    #region Variables
    [Header("Player Dynamic Floats")]
    [Tooltip("The gravity simulation applied to the player.")]
    [SerializeField] private float gravitySim;
    [SerializeField] private float jumpCounter;
    [SerializeField] private float initialSpeed;
    private float finalSpeed;
    [SerializeField] private float playersActualSpeed;
    [SerializeField] private float runningJumpModifier;

    [Header("Player Dynamic Vectors")]
    [SerializeField] private Vector3 groundVector;
    [SerializeField] private Vector3 airVector;
    private Vector3 finalVector;
    [SerializeField] private Vector3 jump;
    [SerializeField] private Vector3 previousPlayerPosition;

    [Header("Player Dynamic Bools")]
    [SerializeField] private bool isAiming;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool wasGrounded;

    [Header("Variables Assigned Via Script")]
    private AudioSource audioSource;
    [SerializeField] private CharacterController characterController;

    [Header("Variables Assigned Via Editor")]
    [SerializeField] private CharacterSettings3D playerSettings;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private Camera mainCamera;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private CamRebounder headbob;

    #endregion

    private void Awake()
    {
        InitializeComponents();
        InitializeSettings();
    }

    void InitializeComponents()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        previousPlayerPosition = transform.position;
    }

    void InitializeSettings()
    {
        gravitySim = playerSettings.gravity;
        jumpCounter = playerSettings.allowedJumps;
        runningJumpModifier = playerSettings.jumpBuffModifier;
    }

    void Update()
    {
        // i used to have logic in here to lock and hide the cursor
        // i've since moved it out of this script 
        // so i'll let my MasterMouse script handle that instead..

        wasGrounded = characterController.isGrounded;

        ProcessPlayerInput();
        GroundCheck();
        JumpCheck();
        Move();

        GetPlayersSpeed();

        // Custom ground check for landing
        if (!wasGrounded && characterController.isGrounded)
        {
            headbob.CamRebound();
        }
    }

    public Vector3 FinalVector() => finalVector;
    public bool Grounded() => characterController.isGrounded;
    public bool Running() => isRunning;

    private void Move()
    {
        ApplyGravity();
        GetDesiredMovementSpeed();

        finalVector =
            (groundVector * finalSpeed) +
            (airVector * playerSettings.airSpeed) +
            (Vector3.up * gravitySim);

        if(jump.magnitude > playerSettings.jumpMagn)
        {
            finalVector += jump;
        }

        characterController.Move(finalVector * Time.deltaTime);
        jump = Vector3.Lerp(jump,Vector3.zero,playerSettings.jumpDamp * Time.deltaTime);
    }

    private void ProcessPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (characterController.isGrounded)
        {
            airVector = Vector3.zero;

            if (horizontalInput != 0 || verticalInput != 0)
            {
                groundVector = new Vector3(horizontalInput, 0, verticalInput);
                groundVector.Normalize();

                groundVector = transform.TransformDirection(groundVector);
            }
            else
            {
                groundVector = Vector3.Lerp(groundVector, Vector3.zero, playerSettings.groundDamp * Time.deltaTime);
            }
        }
        else
        {
            Vector3 airControlVector = new Vector3(horizontalInput, 0, verticalInput);
            airControlVector.Normalize();
            airControlVector = transform.TransformDirection(airControlVector);

            airVector = Vector3.Lerp(airVector, airVector + airControlVector, playerSettings.airControlStrength * Time.deltaTime);
            groundVector = Vector3.Lerp(groundVector, Vector3.zero, playerSettings.airDamp * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && gravitySim < playerSettings.gravity)
            gravitySim = playerSettings.gravity;

        gravitySim += playerSettings.gravity * Time.deltaTime;
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0)
        {
            ResetVerticalVelocity();

            var calculatedJumpModifier = 1f;
            if (isRunning)
                calculatedJumpModifier = runningJumpModifier;

            jump += Vector3.up.normalized * (playerSettings.jumpForce * calculatedJumpModifier) / playerSettings.mass;
            jumpCounter--;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void GroundCheck()
    {
        if (characterController.isGrounded)
        {
            jumpCounter = playerSettings.allowedJumps;
        }
    }

    public void ResetVerticalVelocity()
    {
        jump.y = 0f;
        gravitySim = 0f;
    }

    public void GetDesiredMovementSpeed()
    {
        isAiming = Input.GetMouseButton(1);

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isRunning)
            isRunning = !isRunning;

        float desiredSpeed = playerSettings.walkSpeed;

        if (isRunning)
        {
            if ((finalVector.x == 0f && finalVector.z == 0f) || (Input.GetKeyUp(KeyCode.W)))
                isRunning = !isRunning;

            desiredSpeed = playerSettings.runSpeed;
        }

        if (isRunning && !isAiming)
            desiredSpeed = playerSettings.runSpeed;

        finalSpeed = desiredSpeed;
    }

    // Speedometer
    void GetPlayersSpeed()
    {
        playersActualSpeed = Vector3.Distance(previousPlayerPosition, transform.position) / Time.deltaTime;
        previousPlayerPosition = transform.position;
    }

    public bool visualizeGroundCheck = true;
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (visualizeGroundCheck)
            {
                Gizmos.color = characterController.isGrounded ? Color.green : Color.red;
                Gizmos.DrawRay(transform.position, -transform.up * (characterController.height / 2 + 0.2f));
            }
        }
    }
}
