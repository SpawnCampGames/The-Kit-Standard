using UnityEngine;

namespace SPWN
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class SpawnButtonAttribute : PropertyAttribute
    {
        public string ButtonName { get; private set; }
        public bool CanPressOutsidePlayMode { get; private set; }

        public SpawnButtonAttribute(string buttonName, bool canPressOutsidePlayMode = false)
        {
            ButtonName = buttonName;
            CanPressOutsidePlayMode = canPressOutsidePlayMode;
        }
    }
}