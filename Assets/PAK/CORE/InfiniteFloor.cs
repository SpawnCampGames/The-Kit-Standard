using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class InfiniteFloor : MonoBehaviour
{
    public GameObject defaultTilePrefab; // The default tile prefab
    public GameObject[] rareTilePrefabs; // Array of less common tile prefabs
    public float[] rareTileProbabilities; // Array of probabilities for each rare tile
    public float tileSize = 1.0f; // The size of each tile
    public float radius = 5.0f; // Radius of the circular grid
    public float noiseScale = 10.0f; // Scale for Perlin noise

    private Transform player; // Reference to the player or GameObject to follow
    private Vector2Int currentTileIndex; // The current grid position of the player
    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<Vector2Int, int> tileTypes = new Dictionary<Vector2Int, int>(); // Store the type of each tile

    void Start()
    {
        if (defaultTilePrefab == null)
        {
            UnityEngine.Debug.LogError("Default tile prefab not assigned!");
            enabled = false;
            return;
        }

        if (rareTilePrefabs == null || rareTileProbabilities == null || rareTilePrefabs.Length != rareTileProbabilities.Length)
        {
            UnityEngine.Debug.LogError("Rare tile prefabs and probabilities must be assigned and of equal length!");
            enabled = false;
            return;
        }

        player = this.transform;

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
        return new Vector2Int(Mathf.FloorToInt(position.x / tileSize), Mathf.FloorToInt(position.z / tileSize));
    }

    void UpdateGrid()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Loop over a square grid around the player, but check distance for circular pattern
        int maxGridSize = Mathf.CeilToInt(radius / tileSize);

        for (int x = -maxGridSize; x <= maxGridSize; x++)
        {
            for (int z = -maxGridSize; z <= maxGridSize; z++)
            {
                Vector2Int tilePosition = new Vector2Int(currentTileIndex.x + x, currentTileIndex.y + z);

                // Calculate distance from the center (player's position) to the tile position
                float distance = Vector2.Distance(new Vector2(tilePosition.x, tilePosition.y), new Vector2(currentTileIndex.x, currentTileIndex.y));

                if (distance <= radius / tileSize)
                {
                    if (!tiles.ContainsKey(tilePosition))
                    {
                        // Check if the tile type has already been assigned
                        if (!tileTypes.ContainsKey(tilePosition))
                        {
                            // Determine which tile to use based on noise
                            tileTypes[tilePosition] = GetTileTypeFromNoise(tilePosition);
                        }

                        // Get the corresponding tile prefab
                        GameObject selectedTilePrefab = tileTypes[tilePosition] == 0 ? defaultTilePrefab : rareTilePrefabs[tileTypes[tilePosition] - 1];

                        // Calculate world position for the new tile
                        Vector3 worldPosition = new Vector3(tilePosition.x * tileSize, 0, tilePosition.y * tileSize);
                        GameObject tile = Instantiate(selectedTilePrefab, worldPosition, Quaternion.identity);
                        tiles.Add(tilePosition, tile);
                    }
                }
            }


            stopwatch.Stop();
            SPWN.Dbug.Yellow($"UpdateGrid took: {stopwatch.ElapsedMilliseconds} ms");
        }

        // Recycle tiles that are no longer within the circular grid
        List<Vector2Int> keysToRemove = new List<Vector2Int>();

        foreach (var tile in tiles)
        {
            float distance = Vector2.Distance(new Vector2(tile.Key.x, tile.Key.y), new Vector2(currentTileIndex.x, currentTileIndex.y));
            if (distance > radius / tileSize)
            {
                keysToRemove.Add(tile.Key);
                Destroy(tile.Value);
            }
        }

        foreach (var key in keysToRemove)
        {
            tiles.Remove(key);
        }
    }

    int GetTileTypeFromNoise(Vector2Int tilePosition)
    {
        // Generate Perlin noise value based on tile position
        float noiseValue = Mathf.PerlinNoise(tilePosition.x / noiseScale, tilePosition.y / noiseScale);

        // Normalize the noise value to be in the range [0, 1]
        float normalizedValue = Mathf.InverseLerp(0, 1, noiseValue);

        float cumulativeProbability = 0f;

        // Check if the normalized noise value falls within the probability range of any rare tile
        for (int i = 0; i < rareTileProbabilities.Length; i++)
        {
            cumulativeProbability += rareTileProbabilities[i];
            if (normalizedValue < cumulativeProbability)
            {
                return i + 1; // +1 because 0 is reserved for the default tile
            }
        }

        // If no rare tile was selected, return 0 for the default tile
        return 0;
    }
}
