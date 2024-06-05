using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAtWithIgnore : MonoBehaviour
{
    public GameObject targetToLookAt;
    public bool ignoreX, ignoreY, ignoreZ;

    void Update()
    {
        Vector3 newTarget = GenerateNewTarget(targetToLookAt);
        transform.LookAt(newTarget);
    }

    private Vector3 GenerateNewTarget(GameObject targetToLookAt)
    {
        float newX = ignoreX ? transform.position.x : targetToLookAt.transform.position.x;
        float newY = ignoreY ? transform.position.y : targetToLookAt.transform.position.y;
        float newZ = ignoreZ ? transform.position.z : targetToLookAt.transform.position.z;

        return new Vector3(newX, newY, newZ); 
    }
}
