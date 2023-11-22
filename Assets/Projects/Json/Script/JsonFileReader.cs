using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JsonFileReader : MonoBehaviour
{
    public string jsonFilenameToRead;

    public TextMeshProUGUI newText;
    public AudioSource audioSource;

    void Start()
    {
        jsonFilenameToRead = Path.GetFileNameWithoutExtension(audioSource.clip.name);
        StartCoroutine(simpeCoroutine());
    }

    IEnumerator simpeCoroutine()
    {
        yield return null;
        Debug.Log("coroutine is called");
        newText.text = "Loading...";

        yield return new WaitForSeconds(5f);
        AccessJsonFile();
    }

    void AccessJsonFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, jsonFilenameToRead + ".json");
        string jsonData = File.ReadAllText(filePath);

        newText.text = jsonData;
    }
}
