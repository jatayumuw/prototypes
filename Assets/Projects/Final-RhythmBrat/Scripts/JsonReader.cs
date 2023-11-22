using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JsonReader : MonoBehaviour
{
    private string jsonFileName;

    [Header("Attachment")]
    public AudioSource anySource;
    public ObjectPoolManager objectPoolManager;

    IEnumerator Start()
    {
        yield return null;
        jsonFileName = Path.GetFileNameWithoutExtension(anySource.clip.name);

        AccessJsonFile();
        Debug.Log("JsonReader: Accessing Json");
    }

    public void AccessJsonFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, jsonFileName + ".json");

        if (File.Exists(filePath))
        {
            // Read and process the contents of the JSON file
            string jsonData = File.ReadAllText(filePath);
            SongData songData = JsonUtility.FromJson<SongData>(jsonData);

            // Access song data and beatmap data as needed

            Debug.Log("Song Name: " + songData.songName);
            Debug.Log("Date & Time Saved: " + songData.savedTime);

            foreach (BeatmapData beatmapData in songData.beatData)
            {
                StartCoroutine(EnableObjectsCoroutine(beatmapData.beatTimeStamp, beatmapData.beatIndex));
            }
        }
        else
        {
            Debug.LogWarning("JSON file not found.");
        }
    }

    IEnumerator EnableObjectsCoroutine(float beatTimeStamp, int beatIndex)
    {
        yield return null;
        Debug.Log("JsonReader: EnableObjectsCoroutine is started");

        while (true)
        {
            yield return new WaitForSeconds(beatTimeStamp);
            objectPoolManager.EnableObjectsBasedOnAmplitude(beatIndex);
            Debug.Log("JsonReader: spawn index");
        }
    }
}
