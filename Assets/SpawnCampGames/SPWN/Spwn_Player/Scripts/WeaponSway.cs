using UnityEngine;
namespace SPWN
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] private float swayAmount = 1f;

        //higher number means smoother sway
        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private float clampAmount = 2f;

        private Vector3 initialPosition;

        private void Start()
        {
            initialPosition = transform.localPosition;
        }

        private void Update()
        {
            Vector2 mouseMovement = new Vector2(-Input.GetAxis("Mouse X"),-Input.GetAxis("Mouse Y"));
            Vector2 clampedMovement = Vector2.ClampMagnitude(mouseMovement * swayAmount,clampAmount);
            Vector3 swayPosition = new Vector3(clampedMovement.x,clampedMovement.y,0f);
            Vector3 targetPosition = initialPosition + swayPosition;

            transform.localPosition = Vector3.Lerp(transform.localPosition,targetPosition,smoothSpeed * Time.deltaTime);
        }
    }
}