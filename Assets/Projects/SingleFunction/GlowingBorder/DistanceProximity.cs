using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceProximity : MonoBehaviour
{
    public GameObject obj1, obj2;
    public float distanceBetweenOBJ;
    public bool x, y, z;

    // Update is called once per frame
    void Update()
    {
        distanceBetweenOBJ = GetNewValueByAxis();
    }

    private float GetNewValueByAxis()
    {
        float NewValue;

        if (x)
        {
            NewValue = Mathf.Abs(obj1.transform.position.x - obj2.transform.position.x);
        }
        else if (y)
        {
            NewValue = Mathf.Abs(obj1.transform.position.y - obj2.transform.position.y);
        }
        else
        {
            NewValue = Mathf.Abs(obj1.transform.position.z - obj2.transform.position.z);
        }

        return NewValue;
    }
}
