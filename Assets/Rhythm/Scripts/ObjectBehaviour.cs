using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager;

    public Vector3 noteMoveSpeed;

    private void Start()
    {
        objectPoolManager = FindObjectOfType<ObjectPoolManager>();

        if (objectPoolManager == null)
        {
            Debug.LogError("ObjectPoolManager not found in the scene. Make sure it's present or handle it accordingly.");
        }
    }

    void Update()
    {
        NoteMovement();
    }

    public void NoteMovement()
    {
        if (transform.position.x > objectPoolManager.endPoint.position.x)
        {
            gameObject.transform.position += noteMoveSpeed * Time.deltaTime;
        }
        else
        {
            ObjectPoolManager.Instance.Reset(); // Add this line to call the new method
        }
    }
}
