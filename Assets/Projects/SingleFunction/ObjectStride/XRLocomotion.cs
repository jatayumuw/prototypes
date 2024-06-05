using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRLocomotion : MonoBehaviour
{
    [SerializeField] private float strideDistance, strideDelay;
    [SerializeField] private float rotDegree, rotDelay;

    [SerializeField] private GameObject XROrigin;
    [SerializeField] Coroutine moveToFront, moveToBack, rotToRight, rotToLeft;

    public void MoveForward()
    {
        StopMoving();
        moveToFront = StartCoroutine(MoveXR(1));
    }

    public void MoveBackward()
    {
        StopMoving();
        moveToBack = StartCoroutine(MoveXR(-1));
    }

    public void TurnRight()
    {
        StopTurning();
        rotToRight = StartCoroutine(RotateXR(1));
    }

    public void TurnLeft()
    {
        StopTurning();
        rotToLeft = StartCoroutine(RotateXR(-1));
    }

    public void StopMoving()
    {
        Debug.Log("StopMoving");
        if (moveToFront != null)
        {
            StopCoroutine(moveToFront);
            moveToFront = null;
            Debug.Log("Stopped moving forward");
        }

        if (moveToBack != null)
        {
            StopCoroutine(moveToBack);
            moveToBack = null;
            Debug.Log("Stopped moving backward");
        }
    }

    public void StopTurning()
    {
        Debug.Log("StopTurning");
        if (rotToRight != null)
        {
            StopCoroutine(rotToRight);
            rotToRight = null;
            Debug.Log("Stopped turning right");
        }

        if (rotToLeft != null)
        {
            StopCoroutine(rotToLeft);
            rotToLeft = null;
            Debug.Log("Stopped turning left");
        }
    }

    private IEnumerator MoveXR(int modifierValue)
    {
        while (true)
        {
            XROrigin.transform.Translate(Vector3.forward * (strideDistance * modifierValue) * Time.deltaTime);

            yield return new WaitForSeconds(strideDelay);
        }
    }

    private IEnumerator RotateXR(int modifierValue)
    {
        while (true)
        {
        float snapAngle = rotDegree * modifierValue;
        XROrigin.transform.Rotate(Vector3.up * snapAngle);

            yield return new WaitForSeconds(rotDelay);
        }
    }
}
