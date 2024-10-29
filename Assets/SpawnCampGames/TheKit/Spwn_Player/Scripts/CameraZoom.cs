using UnityEngine;

namespace SPWN
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoom : MonoBehaviour
    {
        Camera cam;
        SpawnCampController move;
        [SerializeField] float zoomSpeed = 10f;

        float desiredFOV;
        float defaultFOV;
        [SerializeField] float zoomedFOV = 50f;

        void Awake()
        {
            cam = GetComponent<Camera>();
            move = FindAnyObjectByType<SpawnCampController>();

            // cache starting field of view
            defaultFOV = cam.fieldOfView;
        }

        void Update()
        {
            if(Mouse.Right(true))
                desiredFOV = zoomedFOV;
            else
                desiredFOV = defaultFOV;

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,desiredFOV,zoomSpeed * Time.deltaTime);
        }
    }
}
