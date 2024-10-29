using UnityEditor;
using UnityEngine;
using System.IO;

public class ProjectLegendEditorWindow : BaseEditorWindow
{
    private string markdownContent = "";
    private const string MarkdownFilePath = "Assets/SpawnCampGames/TheKit/Documentation/ProjectLegend.md";

    [MenuItem("SpawnCampGames/Help/Project Legend", false, 49)]
    public static void ShowProjectLegendWindow()
    {
        var window = GetWindow<ProjectLegendEditorWindow>(true, "", true);
        window.minSize = new Vector2(400, 450);
        window.LoadMarkdownContent(MarkdownFilePath);
        window.ShowUtility();
    }

    private void LoadMarkdownContent(string path)
    {
        if (File.Exists(path))
        {
            markdownContent = File.ReadAllText(path);
        }
        else
        {
            markdownContent = "Markdown file not found.";
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("", EditorStyles.boldLabel);
        GUILayout.Space(2);
        GUILayout.Label(markdownContent, EditorStyles.wordWrappedLabel);
    }
}
