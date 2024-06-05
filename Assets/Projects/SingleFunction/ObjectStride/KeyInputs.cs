using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputs : MonoBehaviour
{
    public XRLocomotion _XRLocomotion;
    void Update()
    {
        // Check for key down events
        if (Input.GetKeyDown(KeyCode.W))
        {
            _XRLocomotion.MoveForward();
            Debug.Log("W key pressed down");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _XRLocomotion.TurnLeft();
            Debug.Log("A key pressed down");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _XRLocomotion.MoveBackward();
            Debug.Log("S key pressed down");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _XRLocomotion.TurnRight();
            Debug.Log("D key pressed down");
        }

        // Check for key up events
        if (Input.GetKeyUp(KeyCode.W))
        {
            _XRLocomotion.StopMoving();
            Debug.Log("W key released");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _XRLocomotion.StopTurning();
            Debug.Log("A key released");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _XRLocomotion.StopMoving();
            Debug.Log("S key released");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _XRLocomotion.StopTurning();
            Debug.Log("D key released");
        }
    }
}
