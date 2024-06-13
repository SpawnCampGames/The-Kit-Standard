using UnityEngine;
using UnityEditor;
using SPWN;

[CustomEditor(typeof(Widget))]
public class WidgetEditor : Editor
{
    // Maximum width for the logo
    private const float MaxLogoWidth = 307f;

    // Boolean variable to control custom inspector drawing
    private bool drawCustomInspector = true;

    public override void OnInspectorGUI()
    {
        // Check if the custom inspector should be drawn
        if (!drawCustomInspector)
        {
            // Draw the default inspector
            DrawDefaultInspector();
        }
        else
        {
            // Draw the custom inspector
            DrawCustomInspector();
        }
    }

    private void DrawCustomInspector()
    {
        // Get reference to the target script
        Widget widget = (Widget)target;

        // Get the current inspector width
        float inspectorWidth = EditorGUIUtility.currentViewWidth;

        // Determine the width for the logo (limited to MaxLogoWidth)
        float logoWidth = Mathf.Min(inspectorWidth, MaxLogoWidth);

        // Calculate the height to maintain aspect ratio
        float aspectRatio = widget.widgetTex != null ? (float)widget.widgetTex.height / widget.widgetTex.width : 0;
        float logoHeight = logoWidth * aspectRatio;

        // Display the logo texture at the top of the inspector if there's enough space
        if (widget.widgetTex != null && inspectorWidth >= MaxLogoWidth)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(widget.widgetTex, GUILayout.Width(logoWidth), GUILayout.Height(logoHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10); // Add some space after the logo
        }
        else if (widget.widgetTex != null)
        {
            EditorGUILayout.HelpBox("Inspector window is too narrow to display the logo.", MessageType.Info);
            GUILayout.Space(10); // Add some space after the logo
        }

        // Display project information with the same width as the image
        DrawLabelCentered("Widget | SPWN Widget", EditorStyles.boldLabel, MaxLogoWidth);
        //DrawLabelCentered("SPWN Widget", EditorStyles.wordWrappedLabel, MaxLogoWidth);

        GUILayout.Space(5);

        // Align the buttons with the text, only clickable in play mode
        DrawButtonAligned("SPWN FUNC 1", MaxLogoWidth, () => {
            Debug.Log("Execute FUNC 1");
        });

        DrawButtonAligned("SPWN FUNC 2", MaxLogoWidth, () => {
            Debug.Log("Execute FUNC 2");
        });

        DrawButtonAligned("SPWN FUNC 3", MaxLogoWidth, () => {
            Debug.Log("Execute FUNC 3");
        });

        // Add some space at the bottom
        GUILayout.Space(20);

        // Draw the copyright text centered
        DrawLabelCentered("Copyright SpawnCampGames 2024", EditorStyles.miniLabel, MaxLogoWidth);
    }

    // Method to draw a centered label with a specific width
    private void DrawLabelCentered(string text, GUIStyle style, float width)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField(text, style, GUILayout.Width(width));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    // Method to draw an aligned button with a specific width, only clickable in play mode
    private void DrawButtonAligned(string text, float width, System.Action onClick)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        
        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying); // Disable button if not in play mode
        if (GUILayout.Button(text, GUILayout.Width(width)))
        {
            if (EditorApplication.isPlaying)
            {
                onClick?.Invoke();
            }
        }
        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    // Method to draw an indented label
    private void DrawLabelIndented(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(20); // Indentation space
        EditorGUILayout.LabelField(text);
        GUILayout.EndHorizontal();
    }
}
