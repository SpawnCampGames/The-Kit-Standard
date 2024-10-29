using UnityEngine;
using UnityEditor;

/// <summary>
/// Debug.Log System Specs
/// </summary>
/// <para>For documentation, see <a href="https://github.com/SpawnCampGames/TheKit/Documentation/">SPWN DOCS</a>.</para>
/// </summary>
/// <remarks>
/// Version 9.30
/// </remarks>
public class SystemSpecsDebug : MonoBehaviour
{
    [MenuItem("SpawnCampGames/Debug/System Specs", false, 18)]
    public static void DebugSystemSpecs()
    {
        // 1 stacks
        Debug.Log($"System Specs:\n");

        Debug.Log($"Time.time = {Time.time}");
        Debug.Log($"Time.fixedDeltaTime = {Time.fixedDeltaTime}");
        Debug.Log($"Physics.gravity = {Physics.gravity}");
        Debug.Log($"Screen.width = {Screen.width}");
        Debug.Log($"Screen.height = {Screen.height}");
        Debug.Log($"Screen.dpi = {Screen.dpi}");
        Debug.Log($"Application.targetFrameRate = {Application.targetFrameRate}");
        Debug.Log($"Random.Range = {Random.Range(0f, 1f)}");
        Debug.Log($"SystemInfo.operatingSystem = {SystemInfo.operatingSystem}");
        Debug.Log($"SystemInfo.processorType = {SystemInfo.processorType}");
        Debug.Log($"SystemInfo.systemMemorySize = {SystemInfo.systemMemorySize}");
        Debug.Log($"SystemInfo.graphicsDeviceName = {SystemInfo.graphicsDeviceName}");
        Debug.Log($"SystemInfo.graphicsMemorySize = {SystemInfo.graphicsMemorySize}");
    }
}
