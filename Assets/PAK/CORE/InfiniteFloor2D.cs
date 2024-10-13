using UnityEngine;
using System.Collections.Generic;

public class InfiniteFloor2D : MonoBehaviour
{
    public GameObject defaultTilePrefab; // The default tile prefab
    public float tileSize = 10f; // The size of each tile
    public float radius = 50f; // Radius of the circular grid

    private Transform player; // Reference to the player or GameObject to follow
    private Vector2Int currentTileIndex; // The current grid position of the player
    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>(); // Dictionary to store tiles

    void Start()
    {
        if (defaultTilePrefab == null)
        {
            Debug.LogError("Default tile prefab not assigned!");
            enabled = false;
            return;
        }

        player = this.transform; // Set the player to this GameObject (the one with the script attached)

        // Set the current tile index based on the player's starting position
        currentTileIndex = GetTileIndex(player.position);

        // Initially populate the grid
        UpdateGrid();
    }

    void Update()
    {
        Vector2Int newTileIndex = GetTileIndex(player.position);

        // Update the grid only if the player has moved to a new tile
        if (newTileIndex != currentTileIndex)
        {
            currentTileIndex = newTileIndex;
            UpdateGrid();
        }
    }

    Vector2Int GetTileIndex(Vector3 position)
    {
        // Calculate the grid index based on the position and tile size
        return new Vector2Int(Mathf.FloorToInt(position.x / tileSize), Mathf.FloorToInt(position.y / tileSize));
    }

    void UpdateGrid()
    {
        // Loop over a square grid around the player, but check distance for circular pattern
        int maxGridSize = Mathf.CeilToInt(radius / tileSize);

        for (int x = -maxGridSize; x <= maxGridSize; x++)
        {
            for (int y = -maxGridSize; y <= maxGridSize; y++)
            {
                Vector2Int tilePosition = new Vector2Int(currentTileIndex.x + x, currentTileIndex.y + y);

                // Calculate distance from the center (player's position) to the tile position
                float distance = Vector2.Distance(new Vector2(tilePosition.x, tilePosition.y), new Vector2(currentTileIndex.x, currentTileIndex.y));

                if (distance <= radius / tileSize)
                {
                    if (!tiles.ContainsKey(tilePosition))
                    {
                        // Instantiate the default tile prefab
                        GameObject tile = Instantiate(defaultTilePrefab, new Vector3(tilePosition.x * tileSize, tilePosition.y * tileSize, 0), Quaternion.identity);
                        tiles.Add(tilePosition, tile);
                    }
                }
            }
        }

        // Recycle tiles that are no longer within the circular grid
        List<Vector2Int> keysToRemove = new List<Vector2Int>();

        foreach (var tile in tiles)
        {
            float distance = Vector2.Distance(new Vector2(tile.Key.x, tile.Key.y), new Vector2(currentTileIndex.x, currentTileIndex.y));
            if (distance > radius / tileSize)
            {
                keysToRemove.Add(tile.Key);
                Destroy(tile.Value); // Remove the tile from the scene
            }
        }

        foreach (var key in keysToRemove)
        {
            tiles.Remove(key); // Remove the tile from the dictionary
        }
    }
}
