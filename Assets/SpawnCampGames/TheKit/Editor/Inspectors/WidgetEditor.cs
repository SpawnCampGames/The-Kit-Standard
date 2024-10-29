using UnityEngine;
using UnityEditor;
using SPWN;
using Codice.CM.Client.Gui;

[CustomEditor(typeof(Widget))]
public class WidgetEditor : Editor
{
    private const float MaxLogoWidth = 307f;
    private const float CopyrightWidth = 173f;
    private const string LogoAssetPath = "Assets/SpawnCampGames/TheKit/SPWN/Graphics/Branding/Spwn_Gamepad.png";
    private const string PopupImagePath = "Assets/SpawnCampGames/SPWN/Spwn_Graphics/Branding/SpawnCampGames.png";

    private GUIStyle logoButtonStyle;
    private int logoClickCount = 0;
    private Texture2D popupImage;

    private Texture2D transparentTexture;

    public override void OnInspectorGUI()
    {
        InitializeStyles();
        DrawCustomInspector();
    }
    private void InitializeStyles()
    {
        // Create a transparent texture if it doesn't exist
        if (transparentTexture == null)
        {
            transparentTexture = new Texture2D(1, 1);
            transparentTexture.SetPixel(0, 0, new Color(0, 0, 0, 0)); // Fully transparent
            transparentTexture.Apply();
        }

        if (logoButtonStyle == null)
        {
            // Initialize the button style if it doesn't exist
            logoButtonStyle = new GUIStyle(GUI.skin.button);
        }

        // Set the background to transparent and update other properties
        logoButtonStyle.normal.background = transparentTexture; // Set normal background to transparent
        logoButtonStyle.hover.background = transparentTexture; // Set hover background to transparent
        logoButtonStyle.active.background = transparentTexture; // Set active background to transparent
        logoButtonStyle.normal.textColor = Color.white; // Set text color
        logoButtonStyle.hover.textColor = Color.gray; // Hover text color
        logoButtonStyle.active.textColor = Color.yellow; // Active text color
        logoButtonStyle.alignment = TextAnchor.MiddleCenter; // Center align the text
        logoButtonStyle.fontSize = 14; // Set the font size
        logoButtonStyle.border = new RectOffset(0, 0, 0, 0); // Remove border
        logoButtonStyle.padding = new RectOffset(0, 0, 0, 0); // Remove padding
        logoButtonStyle.margin = new RectOffset(0, 0, 0, 0); // Remove margin

        if (popupImage == null)
        {
            popupImage = LoadTextureFromAssets(PopupImagePath);
        }
    }

    private void DrawCustomInspector()
    {
        Widget widget = (Widget)target;

        if (widget.widgetTex == null)
        {
            widget.widgetTex = LoadTextureFromAssets(LogoAssetPath);
        }

        float inspectorWidth = EditorGUIUtility.currentViewWidth;
        float logoWidth = Mathf.Min(inspectorWidth, MaxLogoWidth);
        float aspectRatio = widget.widgetTex != null ? (float)widget.widgetTex.height / widget.widgetTex.width : 0;
        float logoHeight = logoWidth * aspectRatio;

        if (widget.widgetTex != null && inspectorWidth >= MaxLogoWidth)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent(widget.widgetTex), logoButtonStyle, GUILayout.Width(logoWidth), GUILayout.Height(logoHeight)))
            {
                logoClickCount++;
                if (logoClickCount >= 5)
                {
                    ShowEasterEggPopup();
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
        else if (widget.widgetTex != null)
        {
            EditorGUILayout.HelpBox("Inspector window is too narrow to display the logo.", MessageType.Info);
            GUILayout.Space(10);
        }

        DrawLabelCentered("🕹️ Widget", EditorStyles.boldLabel, MaxLogoWidth);
        GUILayout.Space(2);
        DrawLabelCentered("• Singleton to access Editor Tools", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("• Holds important scripts for tools, debugging, etc.", EditorStyles.wordWrappedLabel, MaxLogoWidth);
        DrawLabelCentered("• Components can safely be removed if unwanted.", EditorStyles.wordWrappedLabel, MaxLogoWidth);

        GUILayout.Space(15);
        DrawLabelCentered("Copyright SpawnCampGames 2024", EditorStyles.miniLabel, CopyrightWidth);
    }
    private void ShowEasterEggPopup()
    {
        DeveloperPopupEasterEgg popup = CreateInstance<DeveloperPopupEasterEgg>();
        popup.ShowDeveloperPopup();
        logoClickCount = 0; // Reset count for next clicks
    }

    private Texture2D LoadTextureFromAssets(string path)
    {
        return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
    }

    private void DrawLabelCentered(string text, GUIStyle style, float width)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField(text, style, GUILayout.Width(width));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}

public class DeveloperPopupEasterEgg : EditorWindow
{
    private Texture2D popupImage;

    public void OnEnable()
    {
        popupImage = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/SpawnCampGames/SPWN/Spwn_Graphics/Branding/SpawnCampGames.png");
    }

    public void ShowDeveloperPopup()
    {
        // Set the window title and size
        this.titleContent = new GUIContent("Developer Easter Egg");
        this.position = new Rect(Screen.currentResolution.width / 2 - 200, Screen.currentResolution.height / 2 - 200, 400, 400);
        this.minSize = new Vector2(400, 400); // Lock the minimum size
        this.maxSize = new Vector2(400, 400); // Lock the maximum size
        this.ShowUtility(); // Show the window
    }
    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.FlexibleSpace(); // Push content to the center vertically

        // Center the image
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // Push content to the center horizontally

        if (popupImage != null)
        {
            // Calculate the height to maintain aspect ratio
            float aspectRatio = (float)popupImage.height / popupImage.width;
            float desiredWidth = 300f;
            float desiredHeight = desiredWidth * aspectRatio;

            GUILayout.Label(popupImage, GUILayout.Width(desiredWidth), GUILayout.Height(desiredHeight)); // Adjust size as needed
        }
        else
        {
            GUILayout.Label("Popup image failed to load.", EditorStyles.boldLabel);
        }

        GUILayout.FlexibleSpace(); // Push content to the center horizontally
        GUILayout.EndHorizontal();

        // Center the title
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // Push content to the center horizontally
        GUILayout.Label("You're a Developer Now!", EditorStyles.boldLabel);
        GUILayout.FlexibleSpace(); // Push content to the center horizontally
        GUILayout.EndHorizontal();

        // Center the button
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // Push content to the center horizontally
        if (GUILayout.Button("Hell yeah!", GUILayout.Width(75))) // Set button width to 75
        {
            this.Close();
        }
        GUILayout.FlexibleSpace(); // Push content to the center horizontally
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace(); // Push content to the center vertically
        GUILayout.EndVertical();
    }


}
