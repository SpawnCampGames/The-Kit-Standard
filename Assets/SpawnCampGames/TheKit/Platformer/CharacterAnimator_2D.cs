using UnityEngine;

namespace SPWN
{
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer sprite;

        [Header("Settings")]
        [SerializeField, Range(1f, 3f)] private float maxIdleSpeed = 2;
        [SerializeField] private float maxTilt = 5;
        [SerializeField] private float tiltSpeed = 20;

        [Header("Particles")]
        [SerializeField] private ParticleSystem jumpParticles;
        [SerializeField] private ParticleSystem launchParticles;
        [SerializeField] private ParticleSystem moveParticles;
        [SerializeField] private ParticleSystem landParticles;

        private CharacterController_2D playerMovement;
        private bool grounded;
        private ParticleSystem.MinMaxGradient currentGradient;

        private void Awake()
        {
            playerMovement = GetComponentInParent<CharacterController_2D>();
        }

        private void OnEnable()
        {
            playerMovement.Jumped += OnJumped;
            playerMovement.GroundedChanged += OnGroundedChanged;

            moveParticles.Play();
        }

        private void OnDisable()
        {
            playerMovement.Jumped -= OnJumped;
            playerMovement.GroundedChanged -= OnGroundedChanged;

            moveParticles.Stop();
        }

        private void Update()
        {
            if (playerMovement == null) return;

            DetectGroundColor();
            HandleSpriteFlip();
            HandleIdleSpeed();
            HandleCharacterTilt();
        }

        private void HandleSpriteFlip()
        {
            if (playerMovement.FrameInput.x != 0) sprite.flipX = playerMovement.FrameInput.x < 0;
        }

        private void HandleIdleSpeed()
        {
            var inputStrength = Mathf.Abs(playerMovement.FrameInput.x);
            anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, maxIdleSpeed, inputStrength));
            moveParticles.transform.localScale = Vector3.MoveTowards(moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);
        }

        private void HandleCharacterTilt()
        {
            var runningTilt = grounded ? Quaternion.Euler(0, 0, maxTilt * playerMovement.FrameInput.x) : Quaternion.identity;
            anim.transform.up = Vector3.RotateTowards(anim.transform.up, runningTilt * Vector2.up, tiltSpeed * Time.deltaTime, 0f);
        }

        private void OnJumped()
        {
            anim.SetTrigger(JumpKey);
            anim.ResetTrigger(GroundedKey);

            if (grounded)
            {
                SetColor(jumpParticles);
                SetColor(launchParticles);
                jumpParticles.Play();

                // Play jump/launch audio here
            }
        }

        private void OnGroundedChanged(bool isGrounded, float impact)
        {
            grounded = isGrounded;

            if (isGrounded)
            {
                DetectGroundColor();
                SetColor(landParticles);

                anim.SetTrigger(GroundedKey);
                moveParticles.Play();

                landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                landParticles.Play();

                // Play landing audio here
            }
            else
            {
                moveParticles.Stop();
            }
        }

        private void DetectGroundColor()
        {
            var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);

            if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) return;
            var color = r.color;
            currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
            SetColor(moveParticles);
        }

        private void SetColor(ParticleSystem ps)
        {
            var main = ps.main;
            main.startColor = currentGradient;
        }

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
    }
}
