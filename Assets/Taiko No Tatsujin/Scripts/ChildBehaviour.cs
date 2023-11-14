using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehaviour : MonoBehaviour
{
    public bool alive = true;
    public bool interactable = false;
    public Vector3 childMovement;
    public TaikoGameManager taikoGameManager;

    [Header("Parameter")]
    public KeyCode ClickKey = KeyCode.Space;

    private void Start()
    {
        taikoGameManager = FindObjectOfType<TaikoGameManager>();

        if (taikoGameManager == null)
        {
            Debug.LogError("TaikoGameManager cannot be found in the scene.");
        }
    }


    void Update()
    {
        gameObject.transform.position += childMovement * Time.deltaTime;

        if (interactable)
        {
            Interaction();
        }
    }

    public void Interaction()
    {
        if (Input.GetKeyDown(ClickKey))
        {
            taikoGameManager.PlayerHitNode();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NoteHandler"))
        {
            interactable = true;
        }


        if (collision.gameObject.CompareTag("EndPoint"))
        {
            taikoGameManager.PlayerMissNode();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NoteHandler"))
        {
            taikoGameManager.PlayerMissNode();

            interactable = false;
        }
    }
}
