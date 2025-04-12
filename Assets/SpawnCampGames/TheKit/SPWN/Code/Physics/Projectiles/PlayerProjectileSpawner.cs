using UnityEngine;

public class PlayerProjectileSpawner : MonoBehaviour
{
    [SerializeField] SimpleProjectile projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Camera mainCamera;
    [SerializeField] float velocity = 10;
    [SerializeField] LayerMask ignoredLayers; // Set this in the Inspector (e.g., Player + Projectile)
      
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            SpawnProjectile();
    }

    public void SpawnProjectile()
    {
        // Raycast from the camera through the mouse position, ignoring specified layers
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(100); // Default far point if nothing is hit

        if(Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,~ignoredLayers))
            targetPoint = hit.point; // Update target to hit position

        // Calculate direction
        Vector3 direction = (targetPoint - spawnPoint.position).normalized;

        // Spawn projectile facing the target direction
        SimpleProjectile spawnedProjectile = Instantiate(projectilePrefab,spawnPoint.position,Quaternion.LookRotation(direction));
        spawnedProjectile.DebugName();

        // Apply velocity in that direction
        var rb = spawnedProjectile.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * velocity;
    }
}
