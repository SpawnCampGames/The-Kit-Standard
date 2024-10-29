using UnityEngine;
namespace SPWN
{
    public class FrameCapper : MonoBehaviour
    {
        public int frameCap;

        void Start()
        {
            Application.targetFrameRate = frameCap;
        }
    }
}