using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(PrintDirectory))]
public class PrintDirectoryEditor : Editor
{
    // Maximum width for the logo
    private const float MaxLogoWidth = 307f;

    // Width of the buttons
    private const float ButtonWidth = 307f;

    // Boolean variable to control custom inspector drawing
    private bool drawCustomInspector = true;

    public override void OnInspectorGUI()
    {
        // Draw the logo and custom inspector if enabled
        if (drawCustomInspector)
        {
            DrawCustomInspector();
        }
        else
        {
            // Draw default inspector if custom inspector is disabled
            DrawDefaultInspector();
        }
    }

    private void DrawCustomInspector()
    {
        // Get reference to the target script
        PrintDirectory printDir = (PrintDirectory)target;

        // Display the logo texture at the top of the inspector
        if (printDir.PrintDirTex != null)
        {
            float inspectorWidth = EditorGUIUtility.currentViewWidth;
            float logoWidth = Mathf.Min(inspectorWidth, MaxLogoWidth);
            float aspectRatio = (float)printDir.PrintDirTex.height / printDir.PrintDirTex.width;
            float logoHeight = logoWidth * aspectRatio;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(printDir.PrintDirTex, GUILayout.Width(logoWidth), GUILayout.Height(logoHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(10); // Add some space after the logo
        }

        // Display project information centered
        DrawLabelCentered("Print Directory | SPWN PrintDir", EditorStyles.boldLabel, MaxLogoWidth);

        GUILayout.Space(5); // Add some space

        // Button to print folder structure
        DrawButtonAligned("Print Folder Structure", ButtonWidth, () => PrintFolderStructure(printDir.FilePath));

        GUILayout.Space(10); // Add some space

        // Copyright notice
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

    // Method to print folder structure based on given file path
    void PrintFolderStructure(string filePath)
    {
        string outputPath = Path.Combine(Application.dataPath, filePath);

        // Replace backslashes with forward slashes in outputPath
        outputPath = outputPath.Replace('\\', '/');

        // Create or overwrite the Markdown file
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine("# Folder Structure");
            writer.WriteLine();

            // Start printing folder structure from Assets folder
            PrintFolderToFile("Assets", 0, writer);
        }

        Debug.Log($"Folder structure saved to: {outputPath}");
        AssetDatabase.Refresh();
        Debug.Log($"Refreshing assets...");
    }

    // Recursive method to print folders and subfolders
    void PrintFolderToFile(string folderPath, int indentLevel, StreamWriter writer)
    {
        string[] subFolders = Directory.GetDirectories(folderPath);

        foreach (string subFolder in subFolders)
        {
            string folderName = Path.GetFileName(subFolder);
            writer.WriteLine($"{new string('-', indentLevel * 2)} {folderName}");
            PrintFolderToFile(subFolder, indentLevel + 1, writer);
        }
    }
}
