using UnityEngine;
using System.IO;
using UnityEditor;

namespace SPWN
{
    public class FileCreator : MonoBehaviour
    {
        public static string DirectoryPath = "SpawnCampGames/TheKit/Documentation";
        public static string FileName = "CreatedFile.spwn";

        // Menu item to save a blank file
        [MenuItem("SpawnCampGames/Tools/Save Blank File", false, 24)]
        public static void SaveBlankFile()
        {
            // Use the static properties to get the path and file name
            string outputPath = Path.Combine(DirectoryPath, FileName);
            SaveToFile(outputPath);
        }

        // Method to save a blank file
        private static void SaveToFile(string outputPath)
        {
            outputPath = Path.Combine(Application.dataPath, outputPath).Replace('\\', '/');

            try
            {
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Optionally write default content or leave it blank
                }
                Debug.Log($"Blank file created at: {outputPath}");
                AssetDatabase.Refresh();
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error writing to file: {ex.Message}");
            }
        }
    }
}
