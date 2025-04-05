using UnityEngine;

public class T322 : MonoBehaviour
{
    public float mouseSensitivity = 1.5f;
    public float smoothing = 4.5f;
    public Transform playerBody;

    [Header("X Rotation Limits")]
    public float minXRotation = -90f;
    public float maxXRotation = 90f;

    private float xRotation = 0f;
    private float smoothMouseX, smoothMouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float targetMouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float targetMouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // Normalize smoothing to feel proportional
        float smoothFactor = smoothing * 10f * Time.deltaTime;

        smoothMouseX = Mathf.MoveTowards(smoothMouseX,targetMouseX,smoothFactor);
        smoothMouseY = Mathf.MoveTowards(smoothMouseY,targetMouseY,smoothFactor);

        // Apply vertical rotation (clamped)
        xRotation -= smoothMouseY;
        xRotation = Mathf.Clamp(xRotation,minXRotation,maxXRotation);
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        // Apply horizontal rotation
        playerBody.localRotation *= Quaternion.Euler(0f,smoothMouseX,0f);
    }
}
