using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class CharacterController_2D : MonoBehaviour
{
    [SerializeField] CharacterSettings2D characterSettings_2D;

    Rigidbody2D rb;
    CapsuleCollider2D col;

    FrameInput input;
    Vector2 velocity;

    bool isGrounded;
    bool cachedQueryStartInColliders;

    float time;
    float lastGroundedTime;
    float timeJumpPressed;

    bool jumpRequested;
    bool canUseBufferedJump;
    bool endedJumpEarly;
    bool canUseCoyoteTime;

    public Vector2 FrameInput => input.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    void Update()
    {
        time += Time.deltaTime;
        GatherInput();
    }

    void FixedUpdate()
    {
        CheckGroundCollision();
        ProcessJump();
        HandleMovement();
        ApplyVelocity();
    }

    void GatherInput()
    {
        input = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        if (characterSettings_2D.SnapInput)
            SnapInput();

        if (input.JumpDown)
        {
            jumpRequested = true;
            timeJumpPressed = time;
        }
    }

    void SnapInput()
    {
        input.Move.x = Mathf.Abs(input.Move.x) < characterSettings_2D.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(input.Move.x);
        input.Move.y = Mathf.Abs(input.Move.y) < characterSettings_2D.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(input.Move.y);
    }

    void CheckGroundCollision()
    {
        Physics2D.queriesStartInColliders = false;

        bool isGroundHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.down, characterSettings_2D.GrounderDistance, ~characterSettings_2D.PlayerLayer);
        bool isCeilingHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.up, characterSettings_2D.GrounderDistance, ~characterSettings_2D.PlayerLayer);

        if (isCeilingHit) velocity.y = Mathf.Min(0, velocity.y);

        if (!isGrounded && isGroundHit) OnGrounded();
        else if (isGrounded && !isGroundHit) OnLeftGround();

        Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
    }

    void OnGrounded()
    {
        isGrounded = true;
        canUseCoyoteTime = true;
        canUseBufferedJump = true;
        endedJumpEarly = false;
        GroundedChanged?.Invoke(true, Mathf.Abs(velocity.y));
    }

    void OnLeftGround()
    {
        isGrounded = false;
        lastGroundedTime = time;
        GroundedChanged?.Invoke(false, 0);
    }

    void ProcessJump()
    {
        if (endedJumpEarly || velocity.y <= 0 || !input.JumpHeld)
            EndJumpEarly();

        if (!jumpRequested && !HasBufferedJump()) return;

        if (isGrounded || CanUseCoyoteTime()) ExecuteJump();

        jumpRequested = false;
    }

    bool HasBufferedJump() => canUseBufferedJump && time < timeJumpPressed + characterSettings_2D.JumpBuffer;
    bool CanUseCoyoteTime() => canUseCoyoteTime && !isGrounded && time < lastGroundedTime + characterSettings_2D.CoyoteTime;

    void EndJumpEarly()
    {
        if (!endedJumpEarly && !isGrounded && velocity.y > 0) endedJumpEarly = true;
    }

    void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpPressed = 0;
        canUseBufferedJump = false;
        canUseCoyoteTime = false;
        velocity.y = characterSettings_2D.JumpPower;
        Jumped?.Invoke();
    }

    void HandleMovement()
    {
        if (input.Move.x == 0)
            Decelerate();
        else
            Accelerate();
    }

    void Decelerate()
    {
        float deceleration = isGrounded ? characterSettings_2D.GroundDeceleration : characterSettings_2D.AirDeceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
    }

    void Accelerate()
    {
        velocity.x = Mathf.MoveTowards(velocity.x, input.Move.x * characterSettings_2D.MaxSpeed, characterSettings_2D.Acceleration * Time.fixedDeltaTime);
    }

    void ApplyVelocity()
    {
        if (isGrounded && velocity.y <= 0f)
            velocity.y = characterSettings_2D.GroundingForce;
        else
            ApplyGravity();

        rb.linearVelocity = velocity;
    }

    void ApplyGravity()
    {
        float gravity = characterSettings_2D.FallAcceleration;
        if (endedJumpEarly && velocity.y > 0) gravity *= characterSettings_2D.JumpEndEarlyGravityModifier;
        velocity.y = Mathf.MoveTowards(velocity.y, -characterSettings_2D.MaxFallSpeed, gravity * Time.fixedDeltaTime);
    }

    void OnDrawGizmos()
    {
        if (col == null) return;

        Vector2 groundCheckPosition = (Vector2)col.bounds.center + Vector2.down * characterSettings_2D.GrounderDistance;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckPosition, 0.1f);
        Gizmos.DrawLine(col.bounds.center, groundCheckPosition);
    }
}

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
}
