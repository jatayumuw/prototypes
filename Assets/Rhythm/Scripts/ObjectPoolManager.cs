using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public AudioReader audioReader;

    [System.Serializable]
    public class ObjectPool
    {
        public GameObject prefab;
        public int poolSize;
    }

    public List<ObjectPool> objectPools = new List<ObjectPool>();
    public Transform spawnPoint;
    public Transform endPoint;

    public Action Reset;

    private Dictionary<GameObject, Queue<GameObject>> pooledObjects = new Dictionary<GameObject, Queue<GameObject>>();

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        foreach (var objectPool in objectPools)
        {
            CreateObjectPool(objectPool.prefab, objectPool.poolSize);
        }

        Reset += ResetObjectPool;

        StartCoroutine(LoopSpawn());
    }

    void Update()
    { }

    IEnumerator LoopSpawn()
    {
        for (int i = 0; i < 10; i++)
        {

               
            yield return new WaitForSeconds(1);
            SpawnObjectsBasedOnAmplitude(audioReader.amplitudeAsInt);
        }

        yield return null;
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
            obj.GetComponent<ObjectBehaviour>().objectPoolManager = this;
        }
    }

    public void SpawnObjectsBasedOnAmplitude(int prefabIndex)
    {
        GameObject selectedPrefab = objectPools[prefabIndex].prefab;
    
        if (pooledObjects.ContainsKey(selectedPrefab))
        {
            if (pooledObjects[selectedPrefab].Count > 0)
            {
                GameObject obj = pooledObjects[selectedPrefab].Dequeue();
                obj.transform.position = spawnPoint.position;
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);
            }
        }
    }

    void ResetObjectPool()
    {
        Debug.Log("Ter-reset 1");
        foreach (var objectPool in objectPools)
        {
            GameObject prefab = objectPool.prefab;
            Debug.Log("Ter-reset 1");

            if (pooledObjects.ContainsKey(prefab))
            {
                Debug.Log("Ter-reset 2");
                while (pooledObjects[prefab].Count > 0)
                {
                    Debug.Log("Ter-reset 3");
                    GameObject obj = pooledObjects[prefab].Dequeue();

                    obj.SetActive(false);
                }
            }
        }
    }
}
