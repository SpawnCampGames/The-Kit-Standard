using UnityEngine;

[CreateAssetMenu]
public class CharacterSettings_2D : ScriptableObject
{
    [Header("LAYERS")]
    [Tooltip("Assign this to the layer your player character resides on.")]
    public LayerMask PlayerLayer;

    [Header("INPUT")]
    [Tooltip("Enables input snapping to integers. This prevents gamepads from causing slow movement. It's recommended to keep this true for consistent behavior between gamepad and keyboard.")]
    public bool SnapInput = true;

    [Tooltip("The minimum input threshold required for mounting a ladder or climbing a ledge, preventing accidental climbing with controllers."), Range(0.01f, 0.99f)]
    public float VerticalDeadZoneThreshold = 0.3f;

    [Tooltip("The minimum input required to recognize left or right movement, avoiding drift with sticky controllers."), Range(0.01f, 0.99f)]
    public float HorizontalDeadZoneThreshold = 0.1f;

    [Header("MOVEMENT")]
    [Tooltip("The maximum horizontal movement speed.")]
    public float MaxSpeed = 14;

    [Tooltip("The player's ability to accelerate horizontally.")]
    public float Acceleration = 120;

    [Tooltip("The rate at which the player decelerates when stopping on the ground.")]
    public float GroundDeceleration = 60;

    [Tooltip("The rate of deceleration while in the air, only applicable after stopping input mid-air.")]
    public float AirDeceleration = 30;

    [Tooltip("A constant downward force applied while grounded, aiding in slope navigation."), Range(0f, -10f)]
    public float GroundingForce = -1.5f;

    [Tooltip("The distance used for detecting grounding and ceiling collisions."), Range(0f, 0.5f)]
    public float GrounderDistance = 0.05f;

    [Header("JUMP")]
    [Tooltip("The initial upward velocity applied when jumping.")]
    public float JumpPower = 36;

    [Tooltip("The maximum vertical speed the player can achieve while falling.")]
    public float MaxFallSpeed = 40;

    [Tooltip("The rate at which the player gains falling speed, also referred to as in-air gravity.")]
    public float FallAcceleration = 110;

    [Tooltip("The gravity multiplier applied when the jump button is released early.")]
    public float JumpEndEarlyGravityModifier = 3;

    [Tooltip("The duration of coyote time, allowing the player to jump shortly after leaving a ledge.")]
    public float CoyoteTime = .15f;

    [Tooltip("The time we allow for jump input before landing, enabling buffered jump actions.")]
    public float JumpBuffer = .2f;
}
