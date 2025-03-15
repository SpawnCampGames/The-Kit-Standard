using UnityEngine;
using UnityEditor;

public class BaseEditorWindow : EditorWindow
{
    protected Vector2 scrollPosition;
    protected Texture2D logoTexture;

    protected void InitializeWindow(Vector2 minSize)
    {
        this.minSize = minSize;
    }

    protected void SetLogo(Texture2D logo)
    {
        logoTexture = logo;
    }

    protected void DrawLogo()
    {
        if (logoTexture != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(logoTexture);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
    }

    protected virtual string CopyrightText => "Copyright SpawnCampGames 2025";
    protected virtual void DrawFooterText(string footerText = default)
    {
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(footerText == default ? CopyrightText : footerText, EditorStyles.miniLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    protected void DrawTitle(string title)
    {
        GUILayout.Label(title, EditorStyles.boldLabel, GUILayout.Height(20));
    }

    protected void DrawDescription(string description)
    {
        GUILayout.Label(description, EditorStyles.wordWrappedLabel, GUILayout.Height(40));
    }

    protected void BeginScrollView()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height - 50)); // Adjust height as needed
    }

    protected void EndScrollView()
    {
        GUILayout.EndScrollView();
    }


    protected void DrawTextAndCenteredButton(string buttonText, float buttonSize = default, string buttonLabelAbove = "", System.Action onClick = null)
    {
        // Optional space for separation
        GUILayout.Space(1f);

        // Begin a vertical layout for the label and button
        GUILayout.BeginVertical();

        // Draw the label above the button if provided
        if (!string.IsNullOrEmpty(buttonLabelAbove))
        {
            GUILayout.BeginHorizontal(); // Start horizontal layout for the label
            GUILayout.FlexibleSpace(); // Adds flexible space to the left
            GUILayout.Label(buttonLabelAbove, EditorStyles.wordWrappedLabel, GUILayout.Width(buttonSize == default ? 150f : buttonSize));
            GUILayout.FlexibleSpace(); // Adds flexible space to the right
            GUILayout.EndHorizontal(); // End horizontal layout
        }

        // Determine the button size
        var finalButtonSize = buttonSize == default ? new Vector2(150f, 20f) : new Vector2(buttonSize, 20f);

        // Center the button
        GUILayout.BeginHorizontal(); // Start horizontal layout for the button
        GUILayout.FlexibleSpace(); // Adds flexible space to the left
        if (GUILayout.Button(buttonText, GUILayout.Width(finalButtonSize.x), GUILayout.Height(finalButtonSize.y)))
        {
            onClick?.Invoke(); // Invoke the action if not null
        }
        GUILayout.FlexibleSpace(); // Adds flexible space to the right
        GUILayout.EndHorizontal(); // End horizontal layout

        GUILayout.EndVertical(); // End the vertical layout
    }

    protected void DrawIndentedLabel(string text, GUIStyle style, float indentSpace = 20f)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(indentSpace);
        GUILayout.Label(text, style);
        GUILayout.EndHorizontal();
    }

    protected void DrawCenteredButton(string buttonText, float width, System.Action onClick)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(buttonText, GUILayout.Width(width)))
            onClick?.Invoke();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    protected void DrawDivider(float height = 1f)
    {
        GUILayout.Box("", GUILayout.Height(height), GUILayout.ExpandWidth(true));
    }
}
