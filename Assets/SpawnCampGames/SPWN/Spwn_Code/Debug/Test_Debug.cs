using UnityEngine;

public class Test_Debug : MonoBehaviour
{
    public string msg = "The-Kit (Standard Edition)";
    public Color color = Color.cyan;

    void Start()
    {
        string hexColor = ColorUtility.ToHtmlStringRGBA(color);
        Debug.Log($"<color=#{hexColor}>{msg}</color>");
    }
}
