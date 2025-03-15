using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI zoomPercentage;
    public TextMeshProUGUI worldMouse;
    public TextMeshProUGUI normalizedWorldMouse;

    protected override void DoAwake()
    {
        base.DoAwake();
        // Optionally initialize other UI components here if needed
    }

    // Update zoom percentage with two digits, ranging from 00 to 99
    public void UpdateZoomPercentage(float percentage)
    {
        // Convert the percentage to an integer and format it to always show two digits
        int clampedPercentage = Mathf.Clamp(Mathf.RoundToInt(percentage),0,100);
        zoomPercentage.text = $"ZOOM: {clampedPercentage:D2}%"; // D2 ensures two digits
    }

    public void UpdateWorldMouse(Vector3 world,Vector2 remappedWorld)
    {
        // Format each coordinate with three digits, including leading zeros for world position
        string formattedWorldPosition = $"WORLD : ({world.x:000}, {world.y:000}, {world.z:000})";
        worldMouse.text = formattedWorldPosition;

        // Format the normalized world coordinates from FollowMouse script
        string normalizedFormattedPosition = $"RMAP : ({remappedWorld.x:00.00} X, {remappedWorld.y:00.00} Z)";
        normalizedWorldMouse.text = normalizedFormattedPosition;
    }
}
