using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithDelay : MonoBehaviour
{
    public GameObject targetGameObject;
    public float tolerance;
    public float speed = 5f;
    private bool shouldMove;

    private void Update()
    {
        CheckTarget();
        if (shouldMove)
        {
            MoveTowardsTarget();
        }
        else
        {
            shouldMove = CheckTolerance();
        }
    }

    private void MoveTowardsTarget()
    {
        // Move towards the target position
        Vector3 direction = (targetGameObject.transform.position - transform.position).normalized;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetGameObject.transform.position, step);

        // Check if we are within tolerance after moving
        if (Vector3.Distance(transform.position, targetGameObject.transform.position) <= tolerance)
        {
            shouldMove = false;
        }
    }

    private bool CheckTolerance()
    {
        return Vector3.Distance(transform.position, targetGameObject.transform.position) > tolerance;
    }

    private void CheckTarget()
    {
        if (targetGameObject == null)
        {
            Debug.LogError("Target GameObject is not set.");
            shouldMove = false;
        }
    }
}
