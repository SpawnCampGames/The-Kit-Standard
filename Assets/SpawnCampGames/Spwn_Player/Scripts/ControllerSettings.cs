using UnityEngine;

[CreateAssetMenu(menuName = "SpawnCampGames/Controller Settings")]
public class ControllerSettings : ScriptableObject
{
    [Header("Default Settings")]
    public float walkSpeed = 4.5f;
    public float runSpeed = 5.7f;
    public float airSpeed = 3.5f;
    public float crouchSpeed = 2.0f;

    [Header("Damping Settings")]
    public float slideDamp = 1.5f;
    public float airDamp = 0.5f;
    public float groundDamp = 8f;

    [Header("Jump Settings")]
    public float jumpForce = 20.0f;
    public float jumpMagn = 0.25f;
    public float allowedJumps = 2.0f;
    public float jumpBuffModifier = 1f;

    [Header("Air Control Settings")]
    public float airControlStrength = 1.5f;

    [Header("Crouching Settings")]
    public Vector3 standingVector = new Vector3(0,0,0);
    public Vector3 crouchingVector = new Vector3(0,0.5f,0);
    public Vector3 roofCheckOffset = new Vector3(0f,.5f,0f);
    public float roofCheckDistance = .5f;

    [Header("Physics Settings")]
    public float mass = 1.0f;
    public float jumpDamp = 7.0f;
    public float gravity = -11.5f;
}