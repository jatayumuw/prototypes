using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PitchAnalyzer : MonoBehaviour
{
    [Header ("Attachment")]
    public AudioSource audioSource;
    public JsonWriter jsonWriter;
    public TextMeshProUGUI currentPitchTMP, currentLoudnessTMP;
    public GameObject spawnIconA, spawnIconB;

    [Header("Variable (ReadOnly)")]
    public bool audioEnded = false;
    [SerializeField] private float lastSecondLoudness, loudnessThresholdA, loudnessThresholdB, loudnessThresholdC, maxPitch = 0, realHighNotes;
    public int spawnNoteACount, spawnNoteBCount, nullCount;
    public string songPositioning;

    void Start()
    {
        spawnIconA.SetActive(false);
        spawnIconB.SetActive(false);

        if (audioSource == null)
        {
            Debug.LogError("audioSource is empty");
            return;
        }
        else
        {
            StartCoroutine(AnalyzePitchCoroutine());
            int audioLength = Mathf.RoundToInt(audioSource.clip.length);

            Debug.Log("audioSource is found");
        }
    }

    IEnumerator AnalyzePitchCoroutine()
    {
        Debug.Log("Coroutine 1");
        while (true)
        {
            Debug.Log("Coroutine 2");
            yield return null;

            AudioPositionning();
            AnalyzePitchNLoudness();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void AnalyzePitchNLoudness()
    {
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        float maxFrequency = audioSource.clip.frequency * 2;
        float[] spectrum = new float[512];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        //analyze Pitch
        float maxIntensity = 0;
        int maxIndex = 0;

        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] > maxIntensity && i < maxFrequency)
            {
                maxIntensity = spectrum[i];
                maxIndex = i;
            }
        }

        float pitchFrequency = maxIndex * (maxFrequency / spectrum.Length);
        if (pitchFrequency > maxPitch)
        {
            maxPitch = pitchFrequency;
        }
        if (pitchFrequency > 4000)
        {
            realHighNotes++;
        }
        // analyze loudness
        float loudness = 0f;
        foreach (float value in spectrum)
        {
            loudness += value * 10;
        }
        

        //displaying data
        currentLoudnessTMP.text = loudness + " Db";
        if (pitchFrequency > 2000) //high pitch
        {
            currentPitchTMP.text = "High: " + pitchFrequency;

            if (loudness >= loudnessThresholdA)
            {
                jsonWriter.DataWriter(2);
                StartCoroutine(BlinkIconA());
                spawnNoteACount++;
            }
            else
            {
                nullCount++;
            }
        }
        else if (pitchFrequency >= 500 && pitchFrequency <= 2000) //mid pitch
        {
            currentPitchTMP.text = "Mid: " + pitchFrequency;

            if (loudness >= loudnessThresholdB)
            {
                jsonWriter.DataWriter(0);
                StartCoroutine(BlinkIconB());
                spawnNoteBCount++;
            }
            else
            {
                nullCount++;
            }
        }
        else //low pitch
        {
            currentPitchTMP.text = "Low: " + pitchFrequency;

            if (loudness >= loudnessThresholdC)
            {
                jsonWriter.DataWriter(1);
                StartCoroutine(BlinkIconB());
                spawnNoteBCount++;
            }
            else
            {
                nullCount++;
            }
        }
    }
    public void AudioPositionning()
    {
        int currentPosition = (int)audioSource.time;
        int totaAudiolLength = (int)audioSource.clip.length;
        songPositioning = currentPosition + " / " + totaAudiolLength;

        if (currentPosition == totaAudiolLength)
        {
            audioEnded = true;
        }
    }

    IEnumerator BlinkIconA()
    {
        yield return null;
        spawnIconA.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        spawnIconA.SetActive(false);
    }
    IEnumerator BlinkIconB()
    {
        yield return null;
        spawnIconB.SetActive(true);

        yield return new WaitForSeconds(0.25f);
        spawnIconB.SetActive(false);
    }
}
