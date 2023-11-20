using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class BeatMapGenerator : MonoBehaviour
{
    public AudioSource audioSource;
    public float beatThreshold = 0.5f; // Adjust this threshold based on your audio and preference

    private List<float> beatTimeline = new List<float>();

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("Audio Source not assigned!");
            return;
        }

        AnalyzeAudio();
        SaveBeatmapToJson();
    }

    void AnalyzeAudio()
    {
        float[] samples = new float[audioSource.clip.samples];

        audioSource.clip.GetData(samples, 0);

        float sampleRate = audioSource.clip.frequency;
        float clipLength = audioSource.clip.length;

        float[] spectrum = new float[1024];
        int sampleIndex = 0;

        while (sampleIndex < clipLength * sampleRate)
        {
            audioSource.clip.GetData(samples, Mathf.FloorToInt(sampleIndex));

            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

            float average = 0;
            for (int i = 0; i < spectrum.Length; i++)
            {
                average += spectrum[i];
            }
            average /= spectrum.Length;

            if (average > beatThreshold)
            {
                float time = sampleIndex / sampleRate;
                beatTimeline.Add(time);
            }

            sampleIndex += spectrum.Length;
        }
    }

    void SaveBeatmapToJson()
    {
        string songName = audioSource.clip.name;
        string fileName = $"{songName}.json";

        // Check if the file already exists and delete it
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        string json = JsonUtility.ToJson(new BeatmapData(beatTimeline));
        File.WriteAllText(fileName, json);
        Debug.Log($"Beatmap saved to: {fileName}");
    }
}

[System.Serializable]
public class BeatmapData
{
    public List<float> timeline;

    public BeatmapData(List<float> timeline)
    {
        this.timeline = timeline;
    }
}
