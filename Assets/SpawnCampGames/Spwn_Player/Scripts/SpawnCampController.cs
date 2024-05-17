using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SpawnCampController : MonoBehaviour
{
    #region Variables

    [Header("Player Dynamic Floats")]
    [SerializeField] private float gravitySim;
    [SerializeField] private float jumpCounter;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float modifiedSpeed;
    [SerializeField] private float playersActualSpeed;
    [SerializeField] private float runningJumpModifier;

    [Header("Player Dynamic Vectors")]
    [SerializeField] private Vector3 groundVector;
    [SerializeField] private Vector3 airVector;
    [SerializeField] private Vector3 finalVector;
    [SerializeField] private Vector3 jump;
    [SerializeField] private Vector3 dash;
    [SerializeField] private Vector3 previousPlayerPosition;

    [Header("Player Dynamic Bools")]
    [SerializeField] private bool isAiming;
    public bool isCrouching;
    public bool isRunning;
    [SerializeField] private bool isSliding;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool obstacleOverhead;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isClimbing;

    [Header("Variables Assigned Via Script")]
    private AudioSource audioSource;
    private CharacterController characterController;

    [Header("Variables Assigned Via Editor")]
    [SerializeField] private ControllerSettings playerSettings;
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip landSound;
    public Camera mainCamera;

    private float horizontalInput;
    private float verticalInput;

    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        previousPlayerPosition = transform.position;

        gravitySim = playerSettings.gravity;
        jumpCounter = playerSettings.allowedJumps;
        runningJumpModifier = playerSettings.jumpBuffModifier;
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        if(!isClimbing)
        {
            GroundCheck();
            CeilingCheck();
            CrouchCheck();
            JumpAndClimbCheck();
            Move();
        }
        else
        {
            JumpAndClimbCheck();
        }
        GetPlayersSpeed();
    }

    private void Move()
    {
        ProcessPlayerInput();
        ApplyGravity();
        GetPlayersMoveSpeed();

        finalVector =
            (groundVector * initialSpeed) +
            (airVector * playerSettings.airSpeed) +
            (Vector3.up * gravitySim);

        if(jump.magnitude > playerSettings.jumpMagn)
        {
            finalVector += jump;
        }

        characterController.Move(finalVector * Time.deltaTime);
        jump = Vector3.Lerp(jump,Vector3.zero,playerSettings.jumpSmoothing * Time.deltaTime);
        dash = Vector3.Lerp(dash,Vector3.zero,playerSettings.dashSmoothing * Time.deltaTime);
    }

    private void ProcessPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(characterController.isGrounded)
        {
            airVector = Vector3.zero;

            if(isCrouching && isRunning)
            {
                //  isSliding = true;
                groundVector.x = Mathf.Lerp(groundVector.x,0,playerSettings.slideSlowSpeed * Time.deltaTime);
                groundVector.z = Mathf.Lerp(groundVector.z,0,playerSettings.slideSlowSpeed * Time.deltaTime);

                if(Mathf.Abs(groundVector.x) < .175f && Mathf.Abs(groundVector.z) < .175f)
                    isRunning = false;
            }
            else
            {
                // isSliding = false;
                if(horizontalInput != 0 || verticalInput != 0)
                {
                    groundVector = new Vector3(horizontalInput,0,verticalInput);
                    groundVector.Normalize();
                    groundVector = transform.TransformDirection(groundVector);
                }
                else
                    groundVector = Vector3.Lerp(groundVector,Vector3.zero,playerSettings.groundMomentumDampingSpeed * Time.deltaTime);
            }
        }
        else
        {
            Vector3 airControlVector = new Vector3(horizontalInput,0,verticalInput);
            airControlVector.Normalize();
            airControlVector = transform.TransformDirection(airControlVector);
            airVector = Vector3.Lerp(airVector,airVector + airControlVector,playerSettings.airControlStrength * Time.deltaTime);
            groundVector = Vector3.Lerp(groundVector,Vector3.zero,playerSettings.momentumDampingSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if(characterController.isGrounded && gravitySim < playerSettings.gravity)
            gravitySim = playerSettings.gravity;

        gravitySim += playerSettings.gravity * Time.deltaTime;
    }

    private void JumpAndClimbCheck()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isCrouching && jumpCounter > 0) //&& myCC.isGrounded
        {
            ResetVerticalVelocity();

            var calculatedJumpModifier = 1f;
            if(isRunning)
                calculatedJumpModifier = runningJumpModifier;

            // add jump here
            jump += Vector3.up.normalized * (playerSettings.jumpForce * calculatedJumpModifier) / playerSettings.mass;

            jumpCounter--;
            audioSource.PlayOneShot(jumpSound);
        }

        //if(!isClimbing)
        //{
        //    RaycastHit hit;
        //    if(Physics.Raycast(transform.position,transform.forward,out hit,playerSettings.wallRaycastDist) && !characterController.isGrounded)
        //    {
        //        if(hit.collider.GetComponent<Climbable>() != null)
        //        {
        //            StartCoroutine(Climb(hit.collider));
        //            //return;
        //        }
        //    }
        //}
    }

    private void CrouchCheck()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            // Height and Center deltaMaxs need to match
            characterController.height = Mathf.MoveTowards(characterController.height,1f,(7f * Time.deltaTime));
            characterController.center = Vector3.MoveTowards(characterController.center,playerSettings.crouchingVector,(7f * Time.deltaTime));
        }
        else if(!obstacleOverhead)
        {
            isCrouching = false;
            // Height and Center deltaMaxs need to match
            characterController.height = Mathf.MoveTowards(characterController.height,2f,(5f * Time.deltaTime));
            characterController.center = Vector3.MoveTowards(characterController.center,playerSettings.standingVector,(5f * Time.deltaTime));
        }
    }

    private void CeilingCheck()  //Debug Later
    {
        if(isCrouching)
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position + playerSettings.ceilingChkRaycastOffset,1f,transform.up,out hit,playerSettings.ceilingChkRaycastDist))
                obstacleOverhead = true;
            else
                obstacleOverhead = false;
        }
        else
            obstacleOverhead = false;
    }

    private void GroundCheck()
    {
        if(characterController.isGrounded)
        {
            jumpCounter = playerSettings.allowedJumps;
        }
    }

    public void ResetVerticalVelocity()
    {
        jump.y = 0f;
        gravitySim = 0f;
    }

    public void GetPlayersMoveSpeed()
    {
        isAiming = Input.GetMouseButton(1);

        if(Input.GetKeyDown(KeyCode.LeftShift) && !isRunning)
            isRunning = !isRunning;

        float desiredSpeed = playerSettings.walkSpeed;

        if(isRunning)
        {
            if((finalVector.x == 0f && finalVector.z == 0f) || (Input.GetKeyUp(KeyCode.W) && !isCrouching))
                isRunning = !isRunning;

            desiredSpeed = playerSettings.runSpeed;
        }

        if(isCrouching)
            desiredSpeed = playerSettings.crouchSpeed;

        if(isRunning && !isAiming)
            desiredSpeed = playerSettings.runSpeed;

        initialSpeed = desiredSpeed;
    }

    void GetPlayersSpeed()
    {
        playersActualSpeed = Vector3.Distance(previousPlayerPosition,transform.position) / Time.deltaTime;
        previousPlayerPosition = transform.position;
    }

    private IEnumerator Climb(Collider climbableCollider)
    {
        isClimbing = true;

        while(Input.GetKey(KeyCode.W))
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + playerSettings.wallRaycastOffset,transform.forward * playerSettings.wallRaycastDist,Color.blue,0.5f);
            if(Physics.Raycast(transform.position + playerSettings.wallRaycastOffset,transform.forward,out hit,playerSettings.wallRaycastDist))
            {
                if(hit.collider == climbableCollider)
                {
                    characterController.Move(new Vector3(0f,playerSettings.climbSpeed * Time.deltaTime,0f));
                    yield return null;
                }
                else
                    Debug.Log("yo");
                break;
            }
            else

                break;
        }

        jump.y = 100f;
        gravitySim = playerSettings.gravity;
        isClimbing = false;
    }
}