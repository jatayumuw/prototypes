using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    ControlInput controlInput;

    private void Start()
    {
        controlInput = new ControlInput();
        controlInput.Enable();

        controlInput.Interactions.ControlA.performed += ControlA_performed;
        controlInput.Interactions.ControlB.performed += ControlB_performed;
        controlInput.Interactions.Pause.performed += Pause_performed;
    }

    private void ControlA_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Control A Performed");
    }

    private void ControlB_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Control B Performed");
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Pause Performed");
    }
}

