using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class PrintDirectoryEditorWindow : BaseEditorWindow
{
    private string folderPath = "SpawnCampGames/Documentation";
    private string fileName = "PrintedDirectory.md";
    private List<string> folderStructure = new List<string>();

    private const float DocumentationButtonWidth = 150f;

    [MenuItem("SpawnCampGames/Tools/Print Project's Directory")]
    public static void ShowProjectDirectoryEditorWindow()
    {
        var window = GetWindow<PrintDirectoryEditorWindow>("Directory Viewer");
        window.InitializeWindow(new Vector2(400, 510));
        window.LoadFolderStructure("Assets");
    }

    private void OnGUI()
    {
        GUILayout.Label("Folder Structure", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (string line in folderStructure)
        {
            GUILayout.Label(line, EditorStyles.label);
        }

        GUILayout.EndScrollView();
        DrawDivider(1f);

        GUILayout.Space(5f);

        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);
        GUILayout.Space(1f);
        fileName = EditorGUILayout.TextField("File Name", fileName);

        GUILayout.Space(2f);

        DrawTextAndCenteredButton("Refresh", DocumentationButtonWidth, "Explore Your Assets 📂", () => LoadFolderStructure("Assets"));
        DrawTextAndCenteredButton("Save to File", DocumentationButtonWidth, "", SaveToFile);

        DrawDivider(1f);
        DrawFooterText();
    }

    private void LoadFolderStructure(string folderPath)
    {
        folderStructure.Clear();
        PrintFolder(folderPath, 0);
    }

    private void PrintFolder(string folderPath, int indentLevel)
    {
        foreach (string subFolder in Directory.GetDirectories(folderPath))
        {
            string folderName = Path.GetFileName(subFolder);
            folderStructure.Add($"{new string('-', indentLevel * 2)} {folderName}/");
            PrintFolder(subFolder, indentLevel + 1);
        }

        foreach (string file in Directory.GetFiles(folderPath))
        {
            if (Path.GetExtension(file) != ".meta")
            {
                folderStructure.Add($"{new string(' ', indentLevel * 2)}- {Path.GetFileName(file)}");
            }
        }
    }

    private void SaveToFile()
    {
        string outputPath = Path.Combine(Application.dataPath, folderPath, fileName).Replace('\\', '/');

        try
        {
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("# Folder Structure\n");
                foreach (var line in folderStructure)
                {
                    writer.WriteLine(line);
                }
            }
            Debug.Log($"Folder structure saved to: {outputPath}");
            AssetDatabase.Refresh();
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error writing to file: {ex.Message}");
        }
    }
}
