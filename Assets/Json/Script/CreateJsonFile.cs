using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJsonFile : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        string fileName = Path.GetFileNameWithoutExtension(audioSource.clip.name);
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");

        string jsonData = "{\"name\":\"John\",\"age\":25,\"city\":\"Unityville\"}";

        try
        {
            // Write the JSON data to the file.
            File.WriteAllText(filePath, jsonData);

            Debug.Log("JSON file created successfully at: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error creating JSON file: " + e.Message);
        }
    }
}
