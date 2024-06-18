using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObjectPosition : MonoBehaviour
{
    public Material material;
    public Transform targetObject;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;

    void Start()
    {
        if (material != null)
        {
            material.SetFloat("_MinDistance", minDistance);
            material.SetFloat("_MaxDistance", maxDistance);
            material.SetFloat("_MinAlpha", minAlpha);
            material.SetFloat("_MaxAlpha", maxAlpha);
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
