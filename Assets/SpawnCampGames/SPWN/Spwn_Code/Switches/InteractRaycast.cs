using UnityEngine;
using Camera = UnityEngine.Camera;
using SPWN;

public class InteractRaycast : MonoBehaviour
{
    [SerializeField]
    float distance;

    Ray ray;
    bool draw;
    [SerializeField]
    LayerMask mask;

    Camera cam;

    public KeyCode interactKey = KeyCode.E;


    [SerializeField] GameObject cachedInteractable;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        ray = new Ray(cam.transform.position,cam.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit,distance, mask))
        {
            if(hit.collider.transform.TryGetComponent(out IInteractable interactable))
            {
                cachedInteractable = hit.collider.gameObject;
                if(Input.GetKeyDown(interactKey))
                {
                    interactable.Interact();
                    Dbug.Italic($"Interacted with {cachedInteractable.name}");
                }
            }
            else
            {
                cachedInteractable = null;
            }
            draw = true;

        }
        else
        {
            cachedInteractable = null;
            draw = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(draw)
        {
            if(cachedInteractable)
            {
                Gizmos.color = Color.cyan;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }
           
            Gizmos.DrawRay(ray.origin,ray.direction * distance);
        }
    }
}
