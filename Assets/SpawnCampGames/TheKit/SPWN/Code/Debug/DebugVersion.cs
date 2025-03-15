using UnityEngine;
using SPWN;
public class DebugVersion : MonoBehaviour
{
    [SerializeField] public string versionString = "TheKit 2025";
    private GameObject objectThatShouldBeAssigned;

    private void Start()
    {
        Dbug.Log($"Version: {versionString}");
    }
}
