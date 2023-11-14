using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class PoolSettings
    {
        public GameObject parentObject;
        public GameObject prefabToSpawn;
        public int numberOfPrefabsToSpawn;

        [Header("Do not modify these values")]
        public GameObject[] childObjects;
        public List<GameObject> allSpawnedObjects = new List<GameObject>();
    }

    public PoolSettings[] poolSettingsArray;
    public Transform spawnCoordinate;

    void Start()
    {
        foreach (PoolSettings settings in poolSettingsArray)
        {
            if (settings.parentObject != null && settings.prefabToSpawn != null)
            {
                // Spawn the pool of objects.
                SpawnObjectPool(settings);
                UpdateChildCounts(settings);
            }
            else
            {
                Debug.LogError("Parent object or prefab to spawn not assigned!");
            }
        }
    }

    void Update()
    {
        foreach (PoolSettings settings in poolSettingsArray)
        {
            // Check and update counts in the Update method.
            if (settings.parentObject != null)
            {
                UpdateChildCounts(settings);
            }
        }
    }

    void UpdateChildCounts(PoolSettings settings)
    {
        settings.childObjects = settings.parentObject.GetComponentsInChildren<Transform>(true)
            .Where(t => t.gameObject != settings.parentObject.gameObject)
            .Select(t => t.gameObject)
            .ToArray();

        settings.childObjects = settings.childObjects.Where(x => x != null).ToArray();

        // Advanced Debug Logs
        //Debug.Log($"[{name}] - Total Child Count: {settings.childObjects.Length}");
    }

    void SpawnObjectPool(PoolSettings settings)
    {
        for (int i = 0; i < settings.numberOfPrefabsToSpawn; i++)
        {
            GameObject spawnedPrefab = Instantiate(settings.prefabToSpawn, settings.parentObject.transform);
            spawnedPrefab.SetActive(false); // Set the spawned object as inactive.
            settings.allSpawnedObjects.Add(spawnedPrefab); // Add to the list of all spawned objects.
        }
    }

    public void SpawnFromPool(PoolSettings settings)
    {
        if (settings.allSpawnedObjects.Count > 0)
        {
            GameObject objectToSpawn = settings.allSpawnedObjects[0];

            // Move the object to the spawn coordinate, reset rotation, and set active.
            objectToSpawn.transform.position = spawnCoordinate.position;
            objectToSpawn.transform.rotation = Quaternion.identity;
            objectToSpawn.SetActive(true);

            UpdateChildCounts(settings); // Update counts after spawning.
        }
        else
        {
            Debug.LogWarning("No more objects in the pool to spawn!");
        }
    }

    public void FetchToPool(PoolSettings settings, GameObject objectToFetch)
    {
        // Move the object back to default position, reset rotation, and set inactive.
        objectToFetch.transform.position = Vector3.zero;
        objectToFetch.transform.rotation = Quaternion.identity;
        objectToFetch.SetActive(false);

        UpdateChildCounts(settings); // Update counts after fetching.
    }
}
