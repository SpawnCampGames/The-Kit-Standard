using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace SPWN
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class SpawnButtonEditor : Editor
    {
        private GUIStyle smallLabelStyle;
        private GUIStyle buttonStyle;

        private void InitializeStyles()
        {
            if (smallLabelStyle == null)
            {
                smallLabelStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 10, // Set font size smaller than default
                    alignment = TextAnchor.MiddleCenter, // Center align the text
                };
            }

            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    fontSize = 10, // Set font size (adjust as needed)
                    alignment = TextAnchor.MiddleCenter, // Center align the text
                    fixedHeight = 20, // Adjust height as needed
                    fixedWidth = 300, // Adjust width as needed
                    padding = new RectOffset(0, 0, 0, 0) // Ensure padding is zero
                };
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var monoBehaviour = target as MonoBehaviour;
            var methods = monoBehaviour.GetType().GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            bool hasSpawnButtonMethods = false;

            foreach (var method in methods)
            {
                var buttonAttribute = (SpawnButtonAttribute)method.GetCustomAttribute(typeof(SpawnButtonAttribute));

                if (buttonAttribute != null)
                {
                    hasSpawnButtonMethods = true;
                    break;
                }
            }

            if (hasSpawnButtonMethods)
            {
                // Draw separator and label
                DrawSeparator("SPWN DEBUG BUTTONS");

                foreach (var method in methods)
                {
                    var buttonAttribute = (SpawnButtonAttribute)method.GetCustomAttribute(typeof(SpawnButtonAttribute));

                    if (buttonAttribute != null)
                    {
                        GUILayout.Space(1);
                        string buttonName = string.IsNullOrEmpty(buttonAttribute.ButtonName) ? method.Name : buttonAttribute.ButtonName;
                        bool enabled = buttonAttribute.CanPressOutsidePlayMode || Application.isPlaying;
                        DrawCenteredButton(buttonName, () => method.Invoke(monoBehaviour, null), enabled);
                    }
                }
            }
        }
        #region CORE
        /// <summary>
        /// CORE FUNCTIONS
        /// </summary>
        /// <param name="label">Text to display on the separator line.</param>
        private void DrawSeparator(string label)
        {
            GUILayout.Space(10); // Space above the separator
            InitializeStyles();

            // Center the label with smaller font size
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(label, smallLabelStyle); // Apply smaller font style
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Draw separator line in the darker color
            Rect rect = GUILayoutUtility.GetRect(0, 3);
            Color originalColor = GUI.color;
            GUI.color = new Color(0.2f, 0.2f, 0.2f); // Darker gray color for the separator
            GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);
            GUI.color = originalColor; // Restore the original color

            GUILayout.Space(3); // Space below the separator
        }

        private void DrawCenteredButton(string buttonName, System.Action onClick, bool enabled)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Disable the button if not allowed outside of play mode
            GUI.enabled = enabled;
            if (GUILayout.Button(buttonName, buttonStyle))
            {
                onClick?.Invoke();
            }
            GUI.enabled = true; // Re-enable GUI

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}
