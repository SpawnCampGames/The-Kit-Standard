using UnityEngine;

public class MousePanOrthoCam : MonoBehaviour
{
    [SerializeField] private Camera orthoCamera;
    [SerializeField] private float parallaxRange = 5f;
    [SerializeField] private float smoothing = 5f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        if (orthoCamera == null)
        {
            Debug.LogError("OrthoCamera not set in the Inspector! Please assign an Orthographic Camera.");
            enabled = false;
            return;
        }

        // Store the initial camera position
        initialPosition = orthoCamera.transform.position;
    }

    private void Update()
    {
        // Get normalized mouse position between -0.5 and 0.5
        float normalizedMouseX = (Input.mousePosition.x / Screen.width) - 0.5f;

        // Calculate the target position based on the parallax range
        targetPosition = initialPosition + new Vector3(normalizedMouseX * parallaxRange, 0f, 0f);

        // Smoothly interpolate the camera position towards the target position
        orthoCamera.transform.position = Vector3.Lerp(orthoCamera.transform.position, targetPosition, Time.deltaTime * smoothing);
    }
}
