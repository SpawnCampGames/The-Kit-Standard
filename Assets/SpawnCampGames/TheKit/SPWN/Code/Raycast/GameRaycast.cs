using UnityEngine;
using SPWN;

public class GameRaycast3D : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] LayerMask mask;
    [SerializeField] bool clickToInteract;
    [SerializeField] KeyCode interactKey = KeyCode.E;

    Camera cam;
    Ray ray;
    bool draw;

    IInteractable cachedInteractable;
    Transform cachedTransform;
    Vector3 cachedHitPoint;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        ray = new Ray(cam.transform.position,cam.transform.forward);

        if(Physics.Raycast(ray,out RaycastHit hit,distance,mask))
        {
            if(hit.collider.TryGetComponent(out IInteractable interactable))
            {
                cachedInteractable = interactable;
                cachedTransform = hit.transform;
                cachedHitPoint = hit.point;

                if(Input.GetKeyDown(interactKey))
                {
                    cachedInteractable.Interact();
                    Dbug.Italic($"Interacted with {cachedTransform.name}");
                }
            }
            else
            {
                ClearCache();
            }

            draw = true;
        }
        else
        {
            ClearCache();
            draw = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(!draw) return;

        Gizmos.color = cachedInteractable != null ? Color.cyan : Color.yellow;
        Gizmos.DrawRay(ray.origin,ray.direction * distance);
    }

    void ClearCache()
    {
        cachedInteractable = null;
        cachedTransform = null;
    }
}
