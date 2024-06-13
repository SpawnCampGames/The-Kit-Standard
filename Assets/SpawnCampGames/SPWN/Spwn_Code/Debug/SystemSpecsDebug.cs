using UnityEngine;
using UnityEditor;

public class SystemSpecsDebug : MonoBehaviour
{
    [MenuItem("SpawnCampGames/Debug/System Debug")]
    public static void DropDebug()
    {
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
