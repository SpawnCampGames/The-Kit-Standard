using UnityEngine;
using SPWN;
using Debug = UnityEngine.Debug;

/// <summary>
/// Character Controller
/// Includes: Walking, Running, Jumping, Crouching, and Sliding
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class SpawnCampController : MonoBehaviour
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
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isSliding;
    [SerializeField] private bool obstacleOverhead;
    [SerializeField] private bool wasGrounded;

    [Header("Variables Assigned Via Script")]
    private AudioSource audioSource;
    [SerializeField] private CharacterController characterController;

    [Header("Variables Assigned Via Editor")]
    [SerializeField] private CharacterSettings3D characterSettings;
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
        gravitySim = characterSettings.gravity;
        jumpCounter = characterSettings.allowedJumps;
        runningJumpModifier = characterSettings.jumpBuffModifier;
    }

    void Update()
    {
        wasGrounded = characterController.isGrounded;

        ProcessPlayerInput();
        GroundCheck();
        CeilingCheck();
        CrouchCheck();
        JumpCheck();
        Move();

        GetPlayersSpeed();

        // custom ground check for *landing*
        if (!wasGrounded && characterController.isGrounded)
        {
            headbob.CamRebound();
        }
    }
    public Vector3 FinalVector() => finalVector;
    public bool Grounded() => characterController.isGrounded;
    public bool Running() => isRunning;
    public bool Sliding() => isSliding;

    private void Move()
    {
        ApplyGravity();
        GetDesiredMovementSpeed();

        finalVector =
            (groundVector * finalSpeed) +
            (airVector * characterSettings.airSpeed) +
            (Vector3.up * gravitySim);

        if (jump.magnitude > characterSettings.jumpMagn)
        {
            finalVector += jump;
        }

        characterController.Move(finalVector * Time.deltaTime);
        jump = Vector3.Lerp(jump, Vector3.zero, characterSettings.jumpDamp * Time.deltaTime);
    }

    private void ProcessPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (characterController.isGrounded)
        {
            airVector = Vector3.zero;

            if (isCrouching && isRunning)
            {
                isSliding = true;
                groundVector.x = Mathf.Lerp(groundVector.x, 0, characterSettings.slideDamp * Time.deltaTime);
                groundVector.z = Mathf.Lerp(groundVector.z, 0, characterSettings.slideDamp * Time.deltaTime);

                if (Mathf.Abs(groundVector.x) < .175f && Mathf.Abs(groundVector.z) < .175f)
                    isRunning = false;
            }
            else
            {
                isSliding = false;
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    groundVector = new Vector3(horizontalInput, 0, verticalInput);
                    groundVector.Normalize();
                    groundVector = transform.TransformDirection(groundVector);
                }
                else
                    groundVector = Vector3.Lerp(groundVector, Vector3.zero, characterSettings.groundDamp * Time.deltaTime);
            }
        }
        else
        {
            Vector3 airControlVector = new Vector3(horizontalInput, 0, verticalInput);
            airControlVector.Normalize();
            airControlVector = transform.TransformDirection(airControlVector);

            //im getting this kind of stuff while its still being created i think
            airVector = Vector3.Lerp(airVector, airVector + airControlVector, characterSettings.airControlStrength * Time.deltaTime);
            groundVector = Vector3.Lerp(groundVector, Vector3.zero, characterSettings.airDamp * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && gravitySim < characterSettings.gravity)
            gravitySim = characterSettings.gravity;

        gravitySim += characterSettings.gravity * Time.deltaTime;
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCrouching && jumpCounter > 0)
        {
            ResetVerticalVelocity();

            var calculatedJumpModifier = 1f;
            if (isRunning)
                calculatedJumpModifier = runningJumpModifier;


            jump += Vector3.up.normalized * (characterSettings.jumpForce * calculatedJumpModifier) / characterSettings.mass;
            jumpCounter--;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void CrouchCheck()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            // Height and Center deltaMaxs need to match
            characterController.height = Mathf.MoveTowards(characterController.height, 1f, (7f * Time.deltaTime));
            characterController.center = Vector3.MoveTowards(characterController.center, characterSettings.crouchingVector, (7f * Time.deltaTime));
        }
        else if (!obstacleOverhead)
        {
            isCrouching = false;
            // Height and Center deltaMaxs need to match
            characterController.height = Mathf.MoveTowards(characterController.height, 2f, (5f * Time.deltaTime));
            characterController.center = Vector3.MoveTowards(characterController.center, characterSettings.standingVector, (5f * Time.deltaTime));
        }
    }

    private void CeilingCheck()
    {
        if (isCrouching)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position + characterSettings.roofCheckOffset, 1f, transform.up, out hit, characterSettings.roofCheckDistance))
                obstacleOverhead = true;
            else
                obstacleOverhead = false;
        }
        else
            obstacleOverhead = false;
    }

    private void GroundCheck()
    {
        if (characterController.isGrounded)
        {
            jumpCounter = characterSettings.allowedJumps;
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

        float desiredSpeed = characterSettings.walkSpeed;

        if (isRunning)
        {
            if ((finalVector.x == 0f && finalVector.z == 0f) || (Input.GetKeyUp(KeyCode.W) && !isCrouching))
                isRunning = !isRunning;

            desiredSpeed = characterSettings.runSpeed;
        }

        if (isCrouching)
            desiredSpeed = characterSettings.crouchSpeed;

        if (isRunning && !isAiming)
            desiredSpeed = characterSettings.runSpeed;

        finalSpeed = desiredSpeed;
    }

    //speedometer
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
                var gizmoColor = characterController.isGrounded ? Color.green : Color.red;
                Debug.DrawRay(transform.position, -transform.up * (characterController.height/2 + 0.2f), gizmoColor, .1f);
            }
        }
    }
}  