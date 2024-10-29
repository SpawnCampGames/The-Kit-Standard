using UnityEngine;
using UnityEditor;
using SPWN;

[InitializeOnLoad]
public class HierarchyIconEditor : MonoBehaviour
{
    static HierarchyIconEditor()
    {
        // Subscribe to hierarchy window GUI events
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        // Only proceed during the Repaint event
        if (Event.current.type != EventType.Repaint) return;

        // Get the GameObject from the instanceID
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        // Check if the GameObject has the HierarchyIcon component
        if (obj != null && obj.GetComponent<HierarchyIcon>() != null)
        {
            // Use reflection to get the custom icon
            Texture2D icon = GetGameObjectIcon(obj);

            if (icon != null)
            {
                // Adjust icon size by modifying the Rect size (e.g., 12x12 instead of 16x16)
                Rect iconRect = new Rect(selectionRect.xMax - 18, selectionRect.yMin + 2, 12, 12); // Adjust size and position
                GUI.DrawTexture(iconRect, icon);
            }
        }
    }

    static Texture2D GetGameObjectIcon(GameObject obj)
    {
        // Use EditorGUIUtility.ObjectContent to get the icon
        GUIContent iconContent = EditorGUIUtility.ObjectContent(obj, obj.GetType());

        if (iconContent != null)
        {
            if (iconContent.image is Texture2D iconTexture)
            {
                // Check if the image is one of Unity's default icons
                if (IsDefaultIcon(iconTexture))
                {
                    return null;
                }
                return iconTexture;
            }
        }
        // If no custom icon was found, return null
        return null;
    }

    // Method to determine if an icon is a default Unity icon
    static bool IsDefaultIcon(Texture2D icon)
    {
        // Check for default icon names or specific texture properties
        // This is a placeholder; adjust based on your observations
        return icon.name.StartsWith("d_");
    }
}
