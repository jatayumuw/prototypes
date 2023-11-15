using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPoolManager : MonoBehaviour
{
    [Header("Main Parameters")]
    ///[SerializeField] AudioReader audioReader;
    [SerializeField] ObjectBehaviour targetObject;
    [SerializeField] List<ObjectBehaviour> instantiatedObjects;
    [SerializeField] int poolSize;
    //[SerializeField] float spawnOffset;

    [Header("Spawn Parameters")]
    public Transform spawnPoint;
    public Transform endPoint;

    [HideInInspector] public Action<ObjectBehaviour> Reset;

    private int currentIndex;
    private int currentPoolIndex {get => currentIndex ; 
        set {
        if(currentIndex >= instantiatedObjects.Count - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex = value;
        }
    }}

    IEnumerator Start()
    {
        yield return null;
        Reset += ResetObjectPool;

        //yield return new WaitForSeconds(delaySpawn);

        for (int i = 0; i < poolSize; i++)
        {
            CreateObjectPool(targetObject, i);
        }

    }

    void CreateObjectPool(ObjectBehaviour target, int currentIndex)
    {
        ObjectBehaviour obj = Instantiate(target, Vector3.zero, Quaternion.identity);
        obj.gameObject.SetActive(false);
        obj.SetPoolManager(this);
        obj.SetID(currentIndex);
        instantiatedObjects.Add(obj);
    }   

    public void EnableObjectsBasedOnAmplitude(int spriteIndex, float outputValue)
    {
        instantiatedObjects[currentPoolIndex].gameObject.SetActive(true);
        instantiatedObjects[currentPoolIndex].transform.position = spawnPoint.position;
        instantiatedObjects[currentPoolIndex].transform.rotation = Quaternion.identity;
        instantiatedObjects[currentPoolIndex].InitiateNote(spriteIndex, outputValue); 
        currentPoolIndex++;
    }

    public void ResetObjectPool(ObjectBehaviour targetObject)
    {
        targetObject.transform.position = spawnPoint.position;
        targetObject.gameObject.SetActive(false);
    }
}
