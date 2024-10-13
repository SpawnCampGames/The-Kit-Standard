using UnityEngine;
using System.Collections.Generic;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float minX = -100f, maxX = 100f;
    public float minY = -100f, maxY = 100f;
    public float minZ = -100f, maxZ = 100f;

    public int numberOfPrefabs = 15;
    public int maxAttempts = 100;
    public float distanceToOthers = 10f;

    private HashSet<Vector3> cachedPositions = new HashSet<Vector3>();
    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private int notSpawnedCount = 0; // Counter for prefabs that were not spawned

    void Start()
    {
        SpawnPrefabs(numberOfPrefabs);
        Debug.Log($"{notSpawnedCount} prefabs could not be spawned.");
    }

    void SpawnPrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            if (spawnPos != Vector3.zero)
            {
                GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
                GameObject newPrefab = Instantiate(randomPrefab, spawnPos, Quaternion.identity);
                spawnedPrefabs.Add(newPrefab);
                cachedPositions.Add(spawnPos);
                Debug.Log($"Prefab {i + 1} spawned at {spawnPos} using prefab {randomPrefab.name}");
            }
            else
            {
                notSpawnedCount++; // Increment the count for each prefab that fails to spawn
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        int attemptCount = 0;

        while (attemptCount < maxAttempts)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                Random.Range(minZ, maxZ)
            );

            bool tooCloseToOtherSticks = false;
            float totalDistance = distanceToOthers;
            Collider randomPrefabCollider = GetPrefabCollider();
            if (randomPrefabCollider != null)
            {
                totalDistance += randomPrefabCollider.bounds.extents.magnitude;
            }

            float squaredDistanceThreshold = totalDistance * totalDistance;

            foreach (Vector3 pos in cachedPositions)
            {
                if ((randomPos - pos).sqrMagnitude < squaredDistanceThreshold)
                {
                    tooCloseToOtherSticks = true;
                    break;
                }
            }

            if (!tooCloseToOtherSticks)
            {
                Debug.Log($"Valid position found: {randomPos}");
                return randomPos;
            }
            attemptCount++;
        }

        Debug.LogWarning("Could not find a valid position after maximum attempts!");
        return Vector3.zero;
    }

    Collider GetPrefabCollider()
    {
        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
        return randomPrefab.GetComponent<Collider>();
    }
}
