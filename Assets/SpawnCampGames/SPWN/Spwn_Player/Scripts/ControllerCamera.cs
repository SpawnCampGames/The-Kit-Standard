using UnityEngine;

namespace SPWN
{
    public class ControllerCamera : MonoBehaviour
    {
        public GameObject controller;
        [SerializeField] private float lookSensitivity = 1.5f;
        [SerializeField] private float verticalLookLimit = 80f; // Adjust this value to set the limit for looking up and down
        [SerializeField] private float smoothing = 1.5f;
        [SerializeField] private Vector2 smoothedVelocity;
        [SerializeField] private Vector2 currentLookingPos;

        public float playersYRotation;

        void Start()
        {
            LockCursors();
        }

        private void OnEnable()
        {
            currentLookingPos.x = controller.transform.eulerAngles.y;
        }

        void Update()
        {
            playersYRotation = controller.transform.eulerAngles.y;
            RotateCamera();
        }

        private void RotateCamera()
        {
            // get our input
            Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"));
            inputValues = Vector2.Scale(inputValues,new Vector2(lookSensitivity * smoothing,lookSensitivity * smoothing));

            smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x,inputValues.x,1f / smoothing);
            smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y,inputValues.y,1f / smoothing);

            currentLookingPos += smoothedVelocity;

            // Clamp the vertical rotation to limit the angle
            currentLookingPos.y = Mathf.Clamp(currentLookingPos.y,-verticalLookLimit,verticalLookLimit);

            // rotate the camera on its X axis (up and down)
            transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y,Vector3.right);

            // rotate the player on its Y axis (left and right)
            controller.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x,controller.transform.up);
        }

        private void LockCursors()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
