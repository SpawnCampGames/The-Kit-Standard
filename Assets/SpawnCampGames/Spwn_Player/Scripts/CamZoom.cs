using UnityEngine;

namespace SPWN
{
    public class CamZoom : MonoBehaviour
    {
        Camera playerCam;
        public SpawnCampController move;

        private float desiredFOV;
        public float zoomSpeed = 10f;

        void Start()
        {
            playerCam = GetComponent<Camera>();
            // move = FindObjectOfType<SpawnCampController>(); // deprecated
            move = FindFirstObjectByType<SpawnCampController>();
        }

        void Update()
        {

            if(Input.GetButton("Fire2"))
            {
                desiredFOV = 55f;
            }

            else
            {
                desiredFOV = 60f;
            }

            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView,desiredFOV,zoomSpeed * Time.deltaTime);
        }
    }
}
