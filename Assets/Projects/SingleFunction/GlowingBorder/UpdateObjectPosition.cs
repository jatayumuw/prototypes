using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObjectPosition : MonoBehaviour
{
    public Material material;
    public Transform targetObject;
    public float maxDistance = 10f;

    void Start()
    {
        if (material != null)
        {
            material.SetFloat("_MaxDistance", maxDistance);
        }
    }

    void Update()
    {
        if (material != null && targetObject != null)
        {
            material.SetVector("_ObjectPosition", targetObject.position);
        }
    }
}

