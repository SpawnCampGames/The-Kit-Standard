using UnityEngine;

namespace SPWN
{
    public class Oscillate : MonoBehaviour
    {
        [Header("Oscillation Settings")]
        public float oscillationSpeed = 1f;
        public float maxValue = 50f;

        [HideInInspector] public float oscillationValue = 0f;

        private float time;

        private void Start()
        {
            // Offset time so the oscillation starts at 0 instead of snapping
            time = 0.5f;
        }

        void Update()
        {
            // Increment time based on speed
            time += Time.deltaTime * oscillationSpeed;

            // Oscillation value smoothly moves between -maxValue and +maxValue
            oscillationValue = (Mathf.PingPong(time,1f) * 2f - 1f) * maxValue;

            // Apply the oscillating value to the X rotation
            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.x = oscillationValue;
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }
}