using UnityEngine;

namespace SPWN
{
    [RequireComponent(typeof(Camera))]
    public class CamZoom : MonoBehaviour
    {
        Camera playerCam;
        public SpawnCampController move;

        private float desiredFOV;
        public float zoomSpeed = 10f;

        float minFOV = 50f;
        public float maxFOV = 60f;

        void Start()
        {
            playerCam = GetComponent<Camera>();
            move = FindAnyObjectByType<SpawnCampController>();
        }

        void Update()
        {
            if(Input.GetButton("Fire2"))
            {
                desiredFOV = minFOV;
            }

            else
            {
                desiredFOV = maxFOV;
            }

            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView,desiredFOV,zoomSpeed * Time.deltaTime);
        }
    }
}
