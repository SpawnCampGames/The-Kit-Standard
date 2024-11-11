using UnityEngine;

public class CameraBoundsCollider : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider2D topCollider, bottomCollider, leftCollider, rightCollider;

    void Start()
    {
        if (!mainCamera) mainCamera = Camera.main;

        topCollider = CreateCollider("TopCollider");
        bottomCollider = CreateCollider("BottomCollider");
        leftCollider = CreateCollider("LeftCollider");
        rightCollider = CreateCollider("RightCollider");

        UpdateColliders();
    }

    void Update()
    {
        UpdateColliders();
    }

    private BoxCollider2D CreateCollider(string name)
    {
        GameObject colliderObj = new GameObject(name);
        colliderObj.transform.parent = transform;
        var collider = colliderObj.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        return collider;
    }

    private void UpdateColliders()
    {
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Position the colliders based on screen size
        topCollider.size = new Vector2(screenWidth, 1f);
        topCollider.offset = new Vector2(0f, screenHeight / 2f + 0.5f);

        bottomCollider.size = new Vector2(screenWidth, 1f);
        bottomCollider.offset = new Vector2(0f, -screenHeight / 2f - 0.5f);

        leftCollider.size = new Vector2(1f, screenHeight);
        leftCollider.offset = new Vector2(-screenWidth / 2f - 0.5f, 0f);

        rightCollider.size = new Vector2(1f, screenHeight);
        rightCollider.offset = new Vector2(screenWidth / 2f + 0.5f, 0f);
    }
}
