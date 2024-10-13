using UnityEngine;

[CreateAssetMenu(fileName = "NewWindowData", menuName = "SpawnCampGames/WindowData", order = 0)]
public class WindowData : ScriptableObject
{
    public string windowTitle;
    public string titleMessage;
    public string descriptionMessage;
    public string versionNumber;
    public string[] features;
    public Texture2D logo;
}
