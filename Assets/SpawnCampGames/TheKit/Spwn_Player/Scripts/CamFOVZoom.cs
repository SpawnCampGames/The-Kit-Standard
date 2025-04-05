using UnityEngine;

namespace SPWN
{
    [RequireComponent(typeof(Camera))]
    public class CamFOVZoom : MonoBehaviour
    {
        Camera cam;
        [SerializeField] float zoomSpeed = 10f;

        float desiredFOV;
        float defaultFOV;
        [SerializeField] float zoomedFOV = 50f;

        // doesn't work with Cinemachine

        void Awake()
        {
            cam = GetComponent<Camera>();
            defaultFOV = cam.fieldOfView;
        }

        void Update()
        {
            if(Mouse.Right(true))
                desiredFOV = zoomedFOV;
            else
                desiredFOV = defaultFOV;

            //desiredFOV = Input.GetMouseButton(1) ? zoomedFOV : defaultFOV;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,desiredFOV,zoomSpeed * Time.deltaTime);
        }
    }
}
