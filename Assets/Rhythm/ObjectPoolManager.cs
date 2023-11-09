using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPool
    {
        public GameObject prefab;
        public int poolSize;
    }

    public List<ObjectPool> objectPools = new List<ObjectPool>();
    public KeyCode spawnKey = KeyCode.Space;
    public Transform spawnPoint; // Attach the transform where you want to spawn objects

    private Dictionary<GameObject, Queue<GameObject>> pooledObjects = new Dictionary<GameObject, Queue<GameObject>>();

    void Start()
    {
        foreach (var objectPool in objectPools)
        {
            CreateObjectPool(objectPool.prefab, objectPool.poolSize);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            GetObjectPool();
        }
    }

    void CreateObjectPool(GameObject prefab, int poolSize)
    {
        if (!pooledObjects.ContainsKey(prefab))
        {
            pooledObjects[prefab] = new Queue<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pooledObjects[prefab].Enqueue(obj);
        }
    }

    void GetObjectPool()
    {
        int randomIndex = Random.Range(0, objectPools.Count);
        GameObject selectedPrefab = objectPools[randomIndex].prefab;

        if (pooledObjects.ContainsKey(selectedPrefab))
        {
            if (pooledObjects[selectedPrefab].Count > 0)
            {
                GameObject obj = pooledObjects[selectedPrefab].Dequeue();
                obj.transform.position = spawnPoint.position; // Spawn at the attached transform
                obj.SetActive(true);
            }
        }
    }
}
