using SPWN;
using UnityEngine;
using UnityEngine.UI;

public class KeyCastWidget : MonoBehaviour
{
    public Image[] keyImages; // Array of Image components for WASD and additional keys
    public KeyCode[] keyCodes; // Array of KeyCodes for the corresponding keys (W, A, S, D, additional)

    private float lerpSpeed = 10f; // Lerp speed (how fast the alpha value changes)
    private float targetAlpha = 1f; // Target alpha value when key is pressed
    private float initialAlpha = 0f; // Initial alpha value (transparent)
    private float[] currentAlpha; // Array to store current alpha values for each key

    // Assuming the last two indices are for left and right mouse buttons
    private int leftMouseButtonIndex => keyImages.Length - 2;
    private int rightMouseButtonIndex => keyImages.Length - 1;

    void Start()
    {
        currentAlpha = new float[keyImages.Length];
        for(int i = 0; i < keyImages.Length; i++)
        {
            currentAlpha[i] = initialAlpha; // Set initial alpha to 0 (transparent)
        }
    }

    void Update()
    {
        for(int i = 0; i < keyCodes.Length; i++)
        {
            // Check if the key is being pressed
            bool isKeyPressed = Input.GetKey(keyCodes[i]);

            // If key is pressed, smoothly transition the alpha to 1 (visible)
            if(isKeyPressed && currentAlpha[i] < targetAlpha)
            {
                currentAlpha[i] = Mathf.Lerp(currentAlpha[i],targetAlpha,lerpSpeed * Time.deltaTime);
            }
            else if(!isKeyPressed && currentAlpha[i] > initialAlpha)
            {
                currentAlpha[i] = Mathf.Lerp(currentAlpha[i],initialAlpha,lerpSpeed * Time.deltaTime);
            }

            // Apply the alpha value to the Image component
            SetImageAlpha(keyImages[i],currentAlpha[i]);
        }

        // Handle left mouse button (Mouse Button 0)
        UpdateMouseButton(leftMouseButtonIndex,Input.GetMouseButton(0));

        // Handle right mouse button (Mouse Button 1)
        UpdateMouseButton(rightMouseButtonIndex,Input.GetMouseButton(1));
    }

    void UpdateMouseButton(int buttonIndex,bool isButtonPressed)
    {
        if(isButtonPressed && currentAlpha[buttonIndex] < targetAlpha)
        {
            currentAlpha[buttonIndex] = Mathf.Lerp(currentAlpha[buttonIndex],targetAlpha,lerpSpeed * Time.deltaTime);
        }
        else if(!isButtonPressed && currentAlpha[buttonIndex] > initialAlpha)
        {
            currentAlpha[buttonIndex] = Mathf.Lerp(currentAlpha[buttonIndex],initialAlpha,lerpSpeed * Time.deltaTime);
        }

        // Apply the alpha value to the Image component
        SetImageAlpha(keyImages[buttonIndex],currentAlpha[buttonIndex]);
    }

    void SetImageAlpha(Image image,float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
