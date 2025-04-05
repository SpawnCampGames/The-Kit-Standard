using UnityEngine;

namespace SPWN
{
    public class RotationToggle : MonoBehaviour
    {
        public Vector3 minRotation; // Rotation when OFF
        public Vector3 maxRotation; // Rotation when ON
        public float speed = 5f; // Rotation speed

        private bool isOn;
        private Quaternion targetRotation;

        public bool IsOn
        {
            get => isOn;
            set
            {
                if(isOn == value) return; // Skip if already set
                isOn = value;
                targetRotation = Quaternion.Euler(isOn ? maxRotation : minRotation);
            }
        }

        private void Start()
        {
            targetRotation = Quaternion.Euler(isOn ? maxRotation : minRotation);
            transform.rotation = targetRotation;
        }

        private void Update()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,speed * Time.deltaTime);
        }
    }
}
