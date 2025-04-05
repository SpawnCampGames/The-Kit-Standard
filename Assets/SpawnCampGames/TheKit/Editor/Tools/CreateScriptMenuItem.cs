using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateScriptMenuItem : MonoBehaviour
{
    [MenuItem("SpawnCampGames/Tools/New C# Script",false,25)]
    public static void CreateNewScript()
    {
        // open save window
        string scriptName = EditorUtility.SaveFilePanelInProject("Enter the new script name","NewScript","cs","Enter the name for the new script","Assets/Scripts");

        // check if user is dumb
        if(string.IsNullOrEmpty(scriptName)) return;

        // get filename
        string fileName = Path.GetFileName(scriptName);

        if(!fileName.EndsWith(".cs"))
        {
            fileName += ".cs";
            scriptName = Path.Combine(Path.GetDirectoryName(scriptName),fileName);
        }

        if(File.Exists(scriptName))
        {
            EditorUtility.DisplayDialog("Error","Script with this name already exists!","OK");
            return;
        }

        // dynamically create script
        string scriptTemplate = $@"
using UnityEngine;

namespace SPWN
{{
    /// <summary>
    /// <para><c>{fileName.Replace(".cs","")} Script</c></para>
    /// <para> - CodeSkeleton for {fileName.Replace(".cs","")}...</para>
    /// <list type='bullet'>
    /// <item>SPWN Namespace</item>
    /// <item>XML Summary</item>
    /// <item>Ready to Edit Skeleton</item>
    /// </list>
    /// <para>For Documentation, see <a href=''>SPWN DOCS</a>.</para>
    /// </summary>
    /// <remarks>
    /// </remarks> 
    public class {fileName.Replace(".cs","")} : MonoBehaviour
    {{
        // LETS GO!
    }}
}}";

        // write to file (trim beginning whitespace)
        File.WriteAllText(scriptName,scriptTemplate.TrimStart()); // Trim any leading white space

        // import
        AssetDatabase.ImportAsset(scriptName,ImportAssetOptions.ForceUpdate);

        // rephresh
        AssetDatabase.Refresh();

        // make selected
        Object newScriptAsset = AssetDatabase.LoadAssetAtPath<Object>(scriptName);
        Selection.activeObject = newScriptAsset;

        // ping the 304
        EditorGUIUtility.PingObject(newScriptAsset);

        // now its up to you
    }
}
