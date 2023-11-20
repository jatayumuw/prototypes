using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        noteRoutine = StartCoroutine(MoveNodes());
    }

    public IEnumerator MoveNodes()
    {
        while(this.isActive)
        {
            if (transform.position.x > objectPoolManager.endPoint.position.x)
            {
                transform.Translate(-1 * noteMoveSpeed* Time.deltaTime,0,0);
                Debug.Log(Time.deltaTime);
            }
            else
            {
                objectPoolManager.Reset(this); // Add this line to call the new method
                onCompleteMove();
            }
            yield return null;
        }
    }
}
