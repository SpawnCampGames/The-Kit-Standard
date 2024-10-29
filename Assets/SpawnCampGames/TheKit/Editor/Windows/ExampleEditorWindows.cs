using UnityEngine;
using UnityEditor;

public class ExampleEditorWindow : BaseEditorWindow
{
    private string titleMessage = "";
    private string descriptionMessage = "";
    private string[] features = new string[0];
    private const float LeftPadding = 10f;

    [MenuItem("SpawnCampGames/Windows/Basic Utility Window", false, 21)]
    public static void SpawnUtilityWindowFromMenu()
    {
        Texture2D logo = AssetDatabase.LoadAssetAtPath<Texture2D>(
            "Assets/SpawnCampGames/TheKit/SPWN/Graphics/Branding/Spwn_Gamepad.png"
        );

        SpawnUtilityWindow(
            title: "Utility Window",
            minSize: new Vector2(300, 400),
            logo: logo,
            titleMessage: "You found a rare Utility Window!",
            descriptionMessage: "This window can float and has no tabs.",
            features: new string[]
            {
                "• Quick access to useful tools.",
                "• Float freely for easy access without tabs.",
                "• Use it to streamline your workflow.",
                "• Great for enhancing your productivity in Unity."
            }
        );
    }

    [MenuItem("SpawnCampGames/Windows/Basic Dockable Window", false, 22)]
    public static void SpawnWindowFromMenu()
    {
        Texture2D logo = AssetDatabase.LoadAssetAtPath<Texture2D>(
            "Assets/SpawnCampGames/TheKit/SPWN/Graphics/Branding/Spwn_Gamepad.png"
        );

        SpawnDockableWindow(
            title: "Basic Window",
            minSize: new Vector2(300, 400),
            logo: logo,
            titleMessage: "You found a common, Window!",
            descriptionMessage: "This window has no functionality other than displaying a message.",
            features: new string[]
            {
                "• Interact with tools in the editor.",
                "• Add custom features to enhance functionality.",
                "• Float or dock this window for easy access.",
                "• Learn the basics of window design in Unity.",
                "• Enjoy a simple user interface for beginners."
            }
        );
    }

    public static void SpawnUtilityWindow(string title, Vector2 minSize, Texture2D logo, string titleMessage, string descriptionMessage, string[] features)
    {
        var window = CreateInstance<ExampleEditorWindow>();
        window.titleContent = new GUIContent(title);
        window.minSize = minSize;
        window.SetLogo(logo);
        window.titleMessage = titleMessage;
        window.descriptionMessage = descriptionMessage;
        window.features = features;

        window.ShowUtility();
    }

    public static void SpawnDockableWindow(string title, Vector2 minSize, Texture2D logo, string titleMessage, string descriptionMessage, string[] features)
    {
        var window = CreateInstance<ExampleEditorWindow>();
        window.titleContent = new GUIContent(title);
        window.minSize = minSize;
        window.SetLogo(logo); // Set the logo in the base class
        window.titleMessage = titleMessage;
        window.descriptionMessage = descriptionMessage;
        window.features = features;

        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        DrawLogo();

        // Display the title message
        GUILayout.BeginHorizontal();
        GUILayout.Space(LeftPadding);
        GUILayout.Label(titleMessage, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        // Display the description message
        GUILayout.BeginHorizontal();
        GUILayout.Space(LeftPadding);
        GUILayout.Label(descriptionMessage, GUILayout.Width(position.width - LeftPadding * 2));
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        // Draw a horizontal divider
        GUILayout.BeginHorizontal();
        GUILayout.Space(LeftPadding);
        GUILayout.Box(GUIContent.none, GUILayout.Width(position.width - LeftPadding * 2), GUILayout.Height(2));
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        foreach (var point in features)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(LeftPadding);
            GUILayout.Label(point, GUILayout.Width(position.width - LeftPadding * 2));
            GUILayout.EndHorizontal();
        }

        DrawFooterText();
        GUILayout.EndVertical();
    }
}
