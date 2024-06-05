using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ChattingSystem : MonoBehaviour
{
    public class ChatMessage
    {
        public int identityIndex;
        public string textMessages;
    }
    public ChatMessage[] chatMessages;

    public GameObject[] charName;
    public GameObject[] charImage;
    public TextMeshProUGUI chatText;
    public float typingSpeed = 0.05f, punctuationSpeed = 1f;


    int currentCharacterIndex = 0;
    int currentChat = 0;
    Coroutine typingCoroutine;

    public void StartChatting()
    {
        currentChat = 0;
        DoChatting();
    }

    public void DoChatting()
    {
        if (charName.Length == charImage.Length)
        {
            SetImageAndName(currentChat);
            StartTyping();
        }
        else
        {
            Debug.LogWarning($"make sure Name: {charName.Length}, and Profile: {charImage.Length} is same");
        }

        currentChat += 1;
    }

    private void SetImageAndName(int indexToActivate)
    {
        for (int i = 0; i < charName.Length; i++)
        {
            if (i == indexToActivate)
            {
                charImage[i].SetActive(true);
                charName[i].SetActive(true);
            }
            else
            {
                charImage[i].SetActive(false);
                charName[i].SetActive(false);
            }
        }
    }

    public void StartTyping()
    {
        chatText.text = "";

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;

            currentCharacterIndex = 0;
        }

        typingCoroutine = StartCoroutine(typingCharByChar());
    }

    private IEnumerator typingCharByChar()
    {
        Debug.Log($"startTyping");
        yield return null;

        while (currentCharacterIndex < chatMessages[currentChat].textMessages.Length)
        {
            chatText.text = chatMessages[currentChat].textMessages.Substring(0, currentCharacterIndex + 1); // Update displayed text

            char currentChar = chatMessages[currentChat].textMessages[currentCharacterIndex];
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
