using UnityEngine;
using TMPro;

public class HideWhenBehind : MonoBehaviour
{
    private TextMeshPro textElement;
    private Camera mainCamera;
    private Color originalColor;

    void Start()
    {
        mainCamera = Camera.main;
        textElement = GetComponent<TextMeshPro>();
        originalColor = textElement.color; // store original color
    }

    void Update()
    {
        // Get the vector from the camera to the text element
        Vector3 toCamera = mainCamera.transform.position - textElement.transform.position;

        // Check if the front of the text is facing the camera
        bool isFacingCamera = Vector3.Dot(textElement.transform.forward, toCamera) < 0;

        // Show text if it's facing the camera, otherwise hide it
        textElement.color = isFacingCamera
            ? originalColor
            : new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
}
