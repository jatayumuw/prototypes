using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SongData
{
    public string songName;
    public string timeStamp;
    public List<BeatmapDatas> beatmapData;
}

[System.Serializable]
public class BeatmapDatas
{
    public int beatIndex;
    public float beatTimeStamp;
}

public class JsonWriter : MonoBehaviour
{
    [SerializeField] int numberOfbeatmap;
    private string fileName;
    public PitchAnalyzer pitchAnalyzer;

    private void Start()
    {
        if (pitchAnalyzer.audioSource != null)
        {
            fileName = Path.GetFileNameWithoutExtension(pitchAnalyzer.audioSource.clip.name);
            Debug.Log("file will be saved as: " + fileName);
        }
        else
        {
            fileName = "Unknown." + System.DateTime.Now.ToString("HH:mm");
            Debug.LogError("your audiosource is missing");
            Debug.Log("file will be saved as:" + fileName + " instead");
        }

        InitializeJsonFile();
    }

    public void DataWriter(int index)
    {
        float timestamp = pitchAnalyzer.audioSource.time;

        BeatmapDatas data = new BeatmapDatas
        { beatTimeStamp = timestamp, beatIndex = index };

        SongData songData = LoadDataFromJson();
        songData.beatmapData.Add(data);

        SaveDataToJson(songData);

        numberOfbeatmap = songData.beatmapData.Count;
    }

    public void InitializeJsonFile()
    {
        SongData songData = new SongData { songName = pitchAnalyzer.audioSource.clip.name, timeStamp = System.DateTime.Now.ToString("dd/MM - HH:mm"), beatmapData = new List<BeatmapDatas>() };
        SaveDataToJson(songData);
    }

    public void SaveDataToJson(SongData songData)
    {
        string jsonData = JsonUtility.ToJson(songData, true);
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");
    }
    private SongData LoadDataFromJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SongData>(jsonData);
        }
        else
        {
            Debug.LogWarning("JSON file not found. Creating a new one.");
            return new SongData();
        }
    }
}
