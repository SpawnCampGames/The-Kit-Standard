using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class FindMissingScriptsWindow : BaseEditorWindow
{
    private const float MaxLogoWidth = 307f;
    private const string LogoAssetPath = "Assets/SpawnCampGames/TheKit/SPWN/Graphics/Branding/Spwn_Gamepad.png";
    private const float DocumentationButtonWidth = 150f;

    private string windowTitle = "Missing/Broken Script Search";
    private string titleMessage = "🔍 Find Missing Scripts";
    private string descriptionMessage = "- Select GameObjects and click Find.";
    private string resultsMessage = "🔎 Found Missing Scripts:";
    private List<GameObject> foundObjects = new List<GameObject>();

    [MenuItem("SpawnCampGames/Tools/Find Missing Scripts",false,26)]
    public static void ShowWindow()
    {
        Texture2D logo = AssetDatabase.LoadAssetAtPath<Texture2D>(LogoAssetPath);
        var window = CreateInstance<FindMissingScriptsWindow>();
        window.InitializeWindow(new Vector2(400,350));
        window.titleContent = new GUIContent("Find Missing Scripts");
        window.SetLogo(logo);
        window.Show();
    }

    public void OnGUI()
    {
        DrawLogo();
        GUILayout.Space(10);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        DrawContent();
        GUILayout.Space(20);
        DrawButtons();
        GUILayout.EndScrollView();
        DrawFooterText();
    }

    private void DrawContent()
    {
        DrawIndentedLabel(windowTitle,EditorStyles.boldLabel);
        DrawIndentedLabel(titleMessage,EditorStyles.wordWrappedLabel);
        DrawIndentedLabel(descriptionMessage,EditorStyles.wordWrappedLabel);
        GUILayout.Space(20);

        if(foundObjects.Count > 0)
        {
            GUILayout.Label(resultsMessage,EditorStyles.boldLabel);
            foreach(var obj in foundObjects)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                if(GUILayout.Button(obj.name,EditorStyles.label))
                    SelectObjectInHierarchy(obj);
                GUILayout.EndHorizontal();
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.Label("❌ No missing scripts found.",EditorStyles.wordWrappedLabel);
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);
    }

    private void DrawButtons()
    {
        DrawCenteredButton("Find Missing Scripts",DocumentationButtonWidth,() => FindInSelected());
        DrawCenteredButton("Clear Results",DocumentationButtonWidth,() => ClearResults());
    }

    private void FindInSelected()
    {
        foundObjects.Clear();
        foreach(GameObject g in Selection.gameObjects)
            FindInGO(g);
        Debug.Log($"Searched {Selection.gameObjects.Length} GameObjects, found {foundObjects.Count} with missing scripts.");
        Repaint();
    }

    private void FindInGO(GameObject g)
    {
        Component[] components = g.GetComponents<Component>();
        for(int i = 0; i < components.Length; i++)
        {
            if(components[i] == null)
            {
                foundObjects.Add(g);
                Debug.Log($"{g.name} has a missing script at index {i}.",g);
            }
        }
        foreach(Transform child in g.transform)
            FindInGO(child.gameObject);
    }

    private void ClearResults()
    {
        foundObjects.Clear();
        Debug.Log("Results cleared.");
        Repaint();
    }

    private void SelectObjectInHierarchy(GameObject obj)
    {
        Selection.activeGameObject = obj;
        EditorGUIUtility.PingObject(obj);
    }
}
