using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public Vector3 noteMoveSpeed;

    // Update is called once per frame
    void Update()
    {
        NoteMovement();
    }

    public void NoteMovement()
    {
        gameObject.transform.position += noteMoveSpeed * Time.deltaTime;
    }    
}
