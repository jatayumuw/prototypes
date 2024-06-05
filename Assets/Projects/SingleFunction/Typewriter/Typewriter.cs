using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Typewriter : MonoBehaviour
{

    public TextMeshProUGUI TMProGUI;
    public string textToWrite;
    public float typingSpeed = 0.05f, punctuationSpeed = 1f;

    private int currentCharacterIndex = 0; // Index of the character currently being displayed

    Coroutine typingCoroutine;

    private void Awake()
    {
        TMProGUI.text = "";
    }

    public void StartTyping()
    {
        TMProGUI.text = "";

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;

            currentCharacterIndex = 0;
        }

        typingCoroutine = StartCoroutine(typingCharByChar());
        Debug.Log($"clicked: {textToWrite}");
    }

    private IEnumerator typingCharByChar()
    {
        Debug.Log($"startTyping");
        yield return null;

        while (currentCharacterIndex < textToWrite.Length)
        {
            TMProGUI.text = textToWrite.Substring(0, currentCharacterIndex + 1); // Update displayed text

            char currentChar = textToWrite[currentCharacterIndex];
            float delay;

            if (currentChar == ',')
            {
                delay = typingSpeed * punctuationSpeed;
            }
            else if (currentChar == '.')
            {
                delay = typingSpeed * punctuationSpeed * 2;
            }
            else
            {
                delay = typingSpeed;
            }

            yield return new WaitForSeconds(delay);
            currentCharacterIndex++;
        }
    }
}
