using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TheKit))]
public class TheKitEditor : Editor
{
    // Maximum width for the logo
    private const float MaxLogoWidth = 307f;

    // Width of the documentation button
    private const float DocumentationButtonWidth = 150f;

    // URL for the documentation
    private const string DocumentationURL = "https://github.com/SpawnCampGames/The-Kit/blob/main/readme.md";

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
        TheKit spawnScript = (TheKit)target;

        // Display the logo texture at the top of the inspector
        if (spawnScript.logoTexture != null)
        {
            // Get the current inspector width
            float inspectorWidth = EditorGUIUtility.currentViewWidth;

            // Determine the width for the logo (limited to MaxLogoWidth)
            float logoWidth = Mathf.Min(inspectorWidth, MaxLogoWidth);

            // Calculate the height to maintain aspect ratio
            float aspectRatio = (float)spawnScript.logoTexture.height / spawnScript.logoTexture.width;
            float logoHeight = logoWidth * aspectRatio;

            // Draw the logo texture centered and stretched to the calculated width
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(spawnScript.logoTexture, GUILayout.Width(logoWidth), GUILayout.Height(logoHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10); // Add some space after the logo
        }

        // Display project information with the same width as the image
        DrawLabelCentered("The-Kit", EditorStyles.boldLabel, MaxLogoWidth);
        DrawLabelCentered("SpawnCampGame's Official Unity Sandbox", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("Basic Prototyping Kit", EditorStyles.wordWrappedLabel, MaxLogoWidth);

        GUILayout.Space(10); // Add some space between sections

        // Display Features
        DrawLabelCentered("FEATURES", EditorStyles.boldLabel, MaxLogoWidth);
        DrawLabelCentered("- Character Controller", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("- Useful Code Snippets", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("- Singletons & Audio Manager", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("- Prototype Graphics & 3DModels", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        // Add more features as needed

        GUILayout.Space(10); // Add some space between sections

        // Display Packages Included/Needed
        DrawLabelCentered("INCLUDED PACKAGES", EditorStyles.boldLabel, MaxLogoWidth);
        DrawLabelCentered("- 2D Unity Package", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("- Text Mesh Pro - Essentials", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("- Cinemachine", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        // Add more packages as needed

        GUILayout.Space(10); // Add some space between sections

        // Display documentation link
        DrawLabelCentered("Need help? 📘", EditorStyles.wordWrappedLabel, DocumentationButtonWidth);

        // Make the documentation button clickable with a fixed width
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Documentation", GUILayout.Width(DocumentationButtonWidth)))
        {
            Application.OpenURL(DocumentationURL);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
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

    // Method to draw an indented label
    private void DrawLabelIndented(string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(20); // Indentation space
        EditorGUILayout.LabelField(text);
        GUILayout.EndHorizontal();
    }
}
