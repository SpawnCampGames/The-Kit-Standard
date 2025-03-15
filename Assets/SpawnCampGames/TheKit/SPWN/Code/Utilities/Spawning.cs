using UnityEngine;

    public static class Spawning
    {
        // Static method to spawn with position, rotation, and scale
        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject instance = Object.Instantiate(prefab, position, rotation);
            instance.transform.localScale = scale;
            return instance;
        }

        // Overloaded method for spawning with default rotation
        public static GameObject Spawn(GameObject prefab, Vector3 position, Vector3 scale)
        {
            return Spawn(prefab, position, Quaternion.identity, scale);
        }
    }