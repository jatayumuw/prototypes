using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectBehaviour : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager {get ; private set; } 
    public int poolID {get; private set;}
    public float noteMoveSpeed {get; private set;}
    public bool isActive { get => gameObject.activeSelf; }
    [SerializeField] List<Sprite> sprites;

    private SpriteRenderer spriteRenderer {get => GetComponent<SpriteRenderer>();} 
    private Coroutine noteRoutine;
    private Action onCompleteMove;

    private bool interactable;

    private void Start()
    {
        noteMoveSpeed = 5;
        onCompleteMove += () => {
            StopCoroutine(noteRoutine);
        };
    }

    public void SetID(int identity)
    {
        poolID = identity;
    }

    public void SetPoolManager(ObjectPoolManager poolManager)
    {
        objectPoolManager = poolManager;
    }

    public void InitiateNote(int spriteIndex)
    {
        spriteRenderer.sprite = sprites[spriteIndex];

        if (spriteIndex == 0)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        
        noteRoutine = StartCoroutine(MoveNodes());
    }

    public IEnumerator MoveNodes()
    {
        while(this.isActive)
        {
            if (transform.position.x > objectPoolManager.endPoint.position.x)
            {
                transform.Translate(-1 * noteMoveSpeed* Time.deltaTime,0,0);
                //Debug.Log(Time.deltaTime);
            }
            else
            {
                objectPoolManager.Reset(this); // Add this line to call the new method
                onCompleteMove();
            }
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteHandler"))
        {
            //Debug.Log("entering note handler");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteHandler"))
        {
            //Debug.Log("entering note handler");
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Debug.Log("called");

            //    objectPoolManager.Reset(this); // Add this line to call the new method
            //    onCompleteMove();
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteHandler"))
        {
            if (gameObject.activeSelf)
            {
                Debug.Log("missed a note");
                //miss count++ & score multiplier reset
            }
        }
    }
}
