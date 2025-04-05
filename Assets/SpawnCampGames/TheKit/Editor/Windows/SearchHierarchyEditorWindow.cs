using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SearchHierarchyEditorWindow : BaseEditorWindow
{
    private const string LogoAssetPath = "Assets/SpawnCampGames/TheKit/SPWN/Graphics/Branding/Spwn_Gamepad.png";
    private List<GameObject> foundObjects = new List<GameObject>(); // Store GameObjects
    private string searchTag = "";
    private int searchLayer = 0;
    private bool isTagSearch = true;

    [MenuItem("SpawnCampGames/Search/By Layer", false, 19)]
    public static void ShowLayerSearchWindow()
    {
        Texture2D logo = AssetDatabase.LoadAssetAtPath<Texture2D>(LogoAssetPath);
        var window = GetWindow<SearchHierarchyEditorWindow>("Search Hierarchy");
        window.InitializeWindow(new Vector2(400, 555));
        window.SetLogo(logo);
        window.isTagSearch = false;
    }

    [MenuItem("SpawnCampGames/Search/By Tags", false, 20)]
    public static void ShowTagSearchWindow()
    {
        Texture2D logo = AssetDatabase.LoadAssetAtPath<Texture2D>(LogoAssetPath);
        var window = GetWindow<SearchHierarchyEditorWindow>("Search Hierarchy");
        window.InitializeWindow(new Vector2(400, 555));
        window.SetLogo(logo);
        window.isTagSearch = true;
    }

    private void OnGUI()
    {
        DrawLogo(); // Draw the logo at the top
        GUILayout.Space(10); // Add a bit of space below the logo

        if(foundObjects.Count > 0)
        {
            GUILayout.Label("🔎 Found Objects:",EditorStyles.boldLabel);
        }
        else
        {
            GUILayout.Label("❌ No objects found.",EditorStyles.wordWrappedLabel);
        }

        // Start scroll view
        float scrollViewHeight = position.height - 300f; // Set space above and below the scroll view (adjust as needed)
        scrollPosition = GUILayout.BeginScrollView(scrollPosition,GUILayout.Width(position.width),GUILayout.Height(scrollViewHeight));

        if(foundObjects.Count > 0)
        {
            foreach(var obj in foundObjects)
            {
                GUILayout.BeginHorizontal(); // Start a horizontal layout group
                GUILayout.Space(20); // Add indentation (adjust this value as needed)
                if(GUILayout.Button(obj.name,EditorStyles.label))
                {
                    SelectObjectInHierarchy(obj); // Select and ping the GameObject
                }
                GUILayout.EndHorizontal(); // End horizontal layout group
            }
        }

        GUILayout.EndScrollView(); // End scroll view

        // Now, we push the buttons to the bottom of the window
        GUILayout.FlexibleSpace(); // This will push the search buttons to the bottom

        // Adjust the rest of your UI below the scroll view
        GUILayout.Label(isTagSearch ? "Search Tag" : "Search Layer",EditorStyles.boldLabel);

        if(isTagSearch)
        {
            searchTag = EditorGUILayout.TextField("Tag to Search",searchTag);
        }
        else
        {
            searchLayer = EditorGUILayout.LayerField("Layer to Search",searchLayer);
        }

        GUILayout.Space(1); // Add space before buttons

        DrawDivider(1f); // Draw a thin divider line

        // Adjust button sizes and centering
        GUILayout.Space(1f);

        // Centered buttons at the bottom
        DrawTextAndCenteredButton("Search",150f,"",() => Search());
        DrawTextAndCenteredButton("Clear",150f,"",ClearResults);

        GUILayout.Space(5f); // Slight space before footer

        DrawFooterText(); // Draw copyright or footer text
    }


    void Search()
    {
        if (isTagSearch) SearchByTag();
        else SearchByLayer();
    }

    // Method to search by tag
    private void SearchByTag()
    {
        foundObjects.Clear(); // Clear previous results

        if (!string.IsNullOrEmpty(searchTag))
        {
            SPWN.Dbug.Test("Searching by tag: " + searchTag);

            // Get all GameObjects with the specified tag
            var allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            foreach (var obj in allObjects)
            {
                if (obj.CompareTag(searchTag))
                {
                    foundObjects.Add(obj); // Store the GameObject instead of its name
                }
            }
        }
        else
        {
            SPWN.Dbug.Fail("Tag field is empty.");
        }

        Repaint();
    }

    // Method to search by layer
    private void SearchByLayer()
    {
        foundObjects.Clear(); // Clear previous results

        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (var obj in allObjects)
        {
            if (obj.layer == searchLayer)
            {
                foundObjects.Add(obj); // Store the GameObject instead of its name
            }
        }

        Repaint();
    }

    // Clear results method
    private void ClearResults()
    {
        foundObjects.Clear();
        searchTag = "";
        searchLayer = 0; // Reset search layer
        Repaint(); // Repaint to update the UI
    }

    // Method to select and ping the GameObject in the hierarchy
    private void SelectObjectInHierarchy(GameObject obj)
    {
        Selection.activeGameObject = obj; // Select the GameObject
        EditorGUIUtility.PingObject(obj); // Highlight the GameObject in the hierarchy
    }
}
