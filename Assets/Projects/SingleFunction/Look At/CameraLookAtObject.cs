using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtObject : MonoBehaviour
{
    public Transform target;  // Reference to Object-B's transform

    void Update()
    {
        // Ensure the target is assigned
        if (target != null)
        {
            // Make Object-A look at Object-B
            transform.LookAt(target);
        }
    }
}
