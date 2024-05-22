using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnCampGames/Controller Settings")]
public class ControllerSettings : ScriptableObject
{
    [Header("Default Settings")]
    public float walkSpeed = 4.5f;
    public float runSpeed = 5.7f;

    public float speedChangeSmoothness = 4f;

    public float crouchSpeed = 2.0f;
    public float jumpForce = 20.0f;
    public float dashForce = 15.0f;
    public float airSpeed = 3.5f;
    public float climbSpeed = 4.5f;
    public float slideSlowSpeed = 1.5f;
    public float momentumDampingSpeed = 0.5f;
    public float groundMomentumDampingSpeed = 8f;
    public float groundDashStaminaCost = 33.0f;
    public float airDashStaminaCost = 66.0f;
    public float sprintStaminaCost = 30.0f;

    [Header("Jump Settings")]
    public float jumpMagn = 0.25f;
    public float dashMagn = 0.25f;
    public float allowedJumps = 2.0f;
    public float jumpBuffModifier = 1f;
    public float airControlStrength = 1.5f;


    [Header("Crouching Settings")]
    public Vector3 standingVector = new Vector3(0,0,0);
    public Vector3 crouchingVector = new Vector3(0,0.5f,0);
    public Vector3 wallRaycastOffset = new Vector3(0f,-0.8f,0f);
    public Vector3 ceilingChkRaycastOffset = new Vector3(0f,.5f,0f);
    public float ceilingChkRaycastDist = .5f;
    public float wallRaycastDist = 1f;

    [Header("Physics Settings")]
    public float mass = 1.0f;
    public float jumpSmoothing = 7.0f;
    public float dashSmoothing = 7.0f;
    public float gravity = -11.5f;
}