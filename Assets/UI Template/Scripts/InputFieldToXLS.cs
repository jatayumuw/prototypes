using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputField2XML : MonoBehaviour
{
    public TMP_InputField playerNameField;
    public TextMeshProUGUI errorText;
    public GameObject errorIcon;

    public GameObject mainMenu, inGameMenu;

    void Start()
    {
        errorIcon.SetActive(false);
        mainMenu.SetActive(true);
        inGameMenu.SetActive(false);
    }

    public void DebugInput()
    {
        string inputData = playerNameField.text;

        if (!string.IsNullOrEmpty(inputData))
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            mainMenu.SetActive(false);
            inGameMenu.SetActive(true);

            Debug.Log(inputData);
            Debug.Log(timestamp);
        }
        else
        {
            errorText.text = "Name Must Be Filled";
            errorText.color = Color.red;

            errorIcon.SetActive(true);
        }
    }
}
