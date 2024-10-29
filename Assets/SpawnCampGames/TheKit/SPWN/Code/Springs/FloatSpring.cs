using UnityEngine;
using System.Collections;

namespace SPWN
{
    public class FloatSpring : MonoBehaviour
    {
        [Header("Test Settings")]
        [SerializeField]
        public Transform testTarget;
        [SerializeField]
        public bool testRandomize = false;

        [Header("Float Target Settings")]
        [SerializeField]
        public float targetValue = 0f; // The target value for the spring to reach

        [Header("Spring Settings")]
        public float Stiffness = 100f; // The stiffness of the spring (highly dependent on use case)
        public float Damping = 10f; // The damping factor of the spring (0 is no damping)
        public float KnockbackMagnitude = 5f; // The magnitude of the knockback force applied to the spring

        private float currentValue;
        private float currentVelocity;
        private float knockbackForce = 0f;

        float valueThresh = 0.01f;
        float velocityThresh = 0.01f;

        private void Start()
        {
            currentValue = targetValue; // Set the initial value of the spring
            StartCoroutine(UpdateTargetValue());
        }

        void FixedUpdate()
        {
            float dampingFactor = Mathf.Max(0, 1 - Damping * Time.fixedDeltaTime);
            float acceleration = (targetValue - currentValue) * Stiffness * Time.fixedDeltaTime;

            currentVelocity = currentVelocity * dampingFactor + acceleration + knockbackForce;
            currentValue += currentVelocity * Time.fixedDeltaTime;

            testTarget.localPosition = new Vector3(testTarget.localPosition.x, currentValue, testTarget.localPosition.z);

            // Check if the spring has reached the target value and has negligible velocity
            if (Mathf.Abs(currentValue - targetValue) < valueThresh && Mathf.Abs(currentVelocity) < velocityThresh)
            {
                currentValue = targetValue;
                currentVelocity = 0f;
                knockbackForce = 0f;
            }

            knockbackForce = Mathf.Lerp(knockbackForce, 0f, Damping * Time.fixedDeltaTime);
        }

        // Apply knockback force to the spring in a specified direction and magnitude
        public void ApplyKnockback(float magnitude)
        {
            knockbackForce += magnitude;
        }

        private IEnumerator UpdateTargetValue()
        {
            while (testRandomize)
            {
                targetValue = Random.Range(1.5f, 8.5f);
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
