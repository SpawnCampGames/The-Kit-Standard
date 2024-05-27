using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawn))]
public class SpawnEditor : Editor
{
    private const float MaxLogoWidth = 300f; // Maximum width for the logo

    public override void OnInspectorGUI()
    {
        // Get reference to the target script
        Spawn spawnScript = (Spawn)target;

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
        }

        // Draw the default inspector below the logo texture
        DrawDefaultInspector();
    }
}
