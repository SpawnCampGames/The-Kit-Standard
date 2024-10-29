using UnityEngine;
using SPWN;

public class ScreenWriter : MonoBehaviour
{
    public string msg = "THE-KIT - v0.0.1 - 2024";
    public Color textColor = Color.cyan;
    public int textSize = 24;

    public Vector2 offset = new Vector2(40, 40);
    public Color outlineColor = Color.black;
    public int outlineThickness = 2;
    public int padding = 4; // Padding around the text to prevent clipping

    private void OnGUI()
    {
        // Call the static method from the Utils class to display the message
        Utils.RealtimeDebug(msg, offset, textSize, textColor, outlineColor, outlineThickness, padding);
    }

    // Public method to update the message and other parameters
    public void UpdateMessage(string newMsg, Color? newTextColor = null, int? newTextSize = null, Vector2? newOffset = null, Color? newOutlineColor = null, int? newOutlineThickness = null, int? newPadding = null)
    {
        msg = newMsg;

        if (newTextColor.HasValue)
        {
            textColor = newTextColor.Value;
        }

        if (newTextSize.HasValue)
        {
            textSize = newTextSize.Value;
        }

        if (newOffset.HasValue)
        {
            offset = newOffset.Value;
        }

        if (newOutlineColor.HasValue)
        {
            outlineColor = newOutlineColor.Value;
        }

        if (newOutlineThickness.HasValue)
        {
            outlineThickness = newOutlineThickness.Value;
        }

        if (newPadding.HasValue)
        {
            padding = newPadding.Value;
        }
    }
}
