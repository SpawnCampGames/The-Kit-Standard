using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateScriptMenuItem : MonoBehaviour
{
    [MenuItem("SpawnCampGames/Tools/New C# File", false, 25)]
    public static void CreateNewScript()
    {
        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/NewScript.cs");
        File.WriteAllText(path, "");
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}