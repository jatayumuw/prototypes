using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    [SerializeField] private string jsonFileNameToRead;
    public PitchAnalyzer pitchAnalyzer;
    public int jsonOutCounter;

    private void Start()
    {
        jsonFileNameToRead = Path.GetDirectoryName(pitchAnalyzer.audioSource.clip.name);

    }

    public void AccessJsonFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, jsonFileNameToRead + ".json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            SongData songData = JsonUtility.FromJson<SongData>(jsonData);


            foreach (BeatmapDatas beatmapDatas in songData.beatmapData)
            {
                {
                    Debug.Log($"Index: {beatmapDatas.beatIndex} at {beatmapDatas.beatTimeStamp}");
                    jsonOutCounter++;
                }
            }
        }
        else
        {
            Debug.LogError("Json file not found");
        }
    }
}
