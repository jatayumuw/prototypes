using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongData
{
    public string songName;
    public string savedTime;
    public List<BeatmapData> beatData;
}

[System.Serializable]
public class BeatmapData
{
    public int beatIndex;
    public float beatTimeStamp;
}

public class JsonWriter : MonoBehaviour
{
    [SerializeField] int numberOfBeatMap;
    private string fileName;
    public PitchAnalyzer pitchNLoudness;

    void Start()
    {
        if (pitchNLoudness.audioSource != null)
        {
            fileName = Path.GetFileNameWithoutExtension(pitchNLoudness.audioSource.clip.name);
            Debug.Log("file saved as: " + fileName);
        }
        else
        {
            fileName = "Unknown Song " + DateTime.Now.ToString("HH:mm");
            Debug.LogError("audiosource is not available");
            Debug.Log("file saved as: " + fileName);
        }

        InitializeJsonFile();
    }

    public void InitializeJsonFile()
    {
        SongData songData = new SongData
        {
            songName = pitchNLoudness.audioSource.clip.name,
            savedTime = DateTime.Now.ToString("HH:mm, dd/MM/yyyy"),
            beatData = new List<BeatmapData>()
        };
        SaveDataToJson(songData);
    }

    public void DataWriter(int index)
    {
        float timestamp = pitchNLoudness.audioSource.time;

        BeatmapData data = new BeatmapData
        {
            beatTimeStamp = timestamp,
            beatIndex = index
        };

        SongData songData = LoadDataFromJson();
        songData.beatData.Add(data);

        SaveDataToJson(songData);

        numberOfBeatMap = songData.beatData.Count;
    }

    void SaveDataToJson(SongData songData)
    {
        string jsonData = JsonUtility.ToJson(songData, true);
        string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");

        File.WriteAllText(filePath, jsonData);
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
