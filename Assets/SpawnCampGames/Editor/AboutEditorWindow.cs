using UnityEditor;
using UnityEngine;
using SPWN;
using System.Collections.Generic;

public class AboutEditorWindow : BaseEditorWindow
{
    private const float MaxLogoWidth = 307f;
    private const float DocumentationButtonWidth = 150f;
    private const string DocumentationURL = "https://github.com/SpawnCampGames/Documentation/";
    private const string LogoAssetPath = "Assets/SpawnCampGames/SPWN/Spwn_Graphics/Branding/Spwn_Gamepad.png";
    private const float IndentSpace = 20f;

    // Hardcoded window data
    private string windowTitle = "The-Kit";
    private string titleMessage = "SpawnCampGame's Official Unity Sandbox";
    private string descriptionMessage = "Basic Prototyping Kit";
    private string versionNumber = "-- Version 0.1";

    // Headers and their corresponding items
    private List<string> headers = new List<string>
    {
        "FEATURES",
        "PACKAGES"
    };

    private List<string[]> bulletPoints = new List<string[]>
    {
        new string[]
        {
            "Character Controller",
            "Useful Code Snippets",
            "Singletons & Audio Manager",
            "Prototype Graphics & 3D Models"
        },
        new string[]
        {
            "2D Unity Package",
            "Text Mesh Pro - Essentials",
            "Cinemachine"
        }
    };

    public Texture2D logo;  // You can still assign a logo from the Inspector

    [MenuItem("SpawnCampGames/About/The-Kit", false, 0)]
    public static void ShowAboutWindow()
    {
        var window = GetWindow<AboutEditorWindow>("The-Kit");
        window.minSize = new Vector2(400, 500);
        window.maxSize = new Vector2(400, 500);

        window.logo = AssetDatabase.LoadAssetAtPath<Texture2D>(LogoAssetPath);
    }

    [MenuItem("SpawnCampGames/About/Console Thank You")]
    public static void PrintThankYou()
    {
        Dbug.Log("🔥 Thanks for checking out the project!");
        Dbug.Extra($"SpawnCampGames: https://spawncampgames.github.io");
        string readme = "https://github.com/SpawnCampGames/Documentation/";
        Dbug.Green($"📗 The Kit: {readme}");
    }

    [MenuItem("SpawnCampGames/Help/Open Documentation")]
    public static void OpenDocumentation()
    {
        Application.OpenURL(DocumentationURL);
    }

    void OpenDocs() => Application.OpenURL(DocumentationURL);

    private void OnGUI()
    {
        // Draw the logo (if assigned in the Inspector)
        if (logo != null)
        {
            float logoWidth = Mathf.Min(position.width, MaxLogoWidth);
            float aspectRatio = (float)logo.height / logo.width;
            float logoHeight = logoWidth * aspectRatio;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(logo, GUILayout.Width(logoWidth), GUILayout.Height(logoHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(10);  // Add space after the logo
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        // Main content section, now hardcoded
        DrawIndentedLabel(windowTitle, EditorStyles.boldLabel);
        DrawIndentedLabel(versionNumber, EditorStyles.wordWrappedLabel);
        DrawIndentedLabel(titleMessage, EditorStyles.wordWrappedLabel);
        DrawIndentedLabel(descriptionMessage, EditorStyles.wordWrappedLabel);

        GUILayout.Space(20);

        // Draw headers and corresponding bullet points
        for (int i = 0; i < headers.Count; i++)
        {
            DrawIndentedLabel(headers[i], EditorStyles.boldLabel);
            foreach (string bullet in bulletPoints[i])
            {
                DrawIndentedLabel($"- {bullet}", EditorStyles.wordWrappedLabel);
            }
            GUILayout.Space(10); // Space between sections
        }
        GUILayout.EndScrollView();
        DrawTextAndCenteredButton("Documentation", DocumentationButtonWidth, "Need help? 📘", () => OpenDocs());
        DrawFooterText();
    }
}
