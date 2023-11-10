using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehaviour : MonoBehaviour
{
    public Vector3 childMovement;

    void Update()
    {
        gameObject.transform.position += childMovement * Time.deltaTime;
    }
}
