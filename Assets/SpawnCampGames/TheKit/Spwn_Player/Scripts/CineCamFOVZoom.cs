using Unity.Cinemachine;
using UnityEngine;
namespace SPWN
{
[RequireComponent(typeof(CinemachineCamera))]
public class CineCamFOVZoom : MonoBehaviour
{
    CinemachineCamera cam;
    [SerializeField] float zoomSpeed = 10f;

    float desiredFOV;
    float defaultFOV;
    [SerializeField] float zoomedFOV = 50f;

    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        defaultFOV = cam.Lens.FieldOfView;
    }

    void Update()
    {
        if(Mouse.Right(true))
            desiredFOV = zoomedFOV;
        else
            desiredFOV = defaultFOV;

        //desiredFOV = Input.GetMouseButton(1) ? zoomedFOV : defaultFOV;
        cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView,desiredFOV,zoomSpeed * Time.deltaTime);
    }
}
}
