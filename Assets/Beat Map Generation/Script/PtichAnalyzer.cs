using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PtichAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI currentPitchTMP;
    public TextMeshProUGUI currentLoudnessTMP;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("audioSource is empty");
            return;
        }
        else
        {
            StartCoroutine(AnalyzePitchCoroutine());
            Debug.Log("audioSource is found");
        }
    }

    IEnumerator AnalyzePitchCoroutine()
    {
        Debug.Log("Coroutine 1");
        while (true)
        {
            Debug.Log("Coroutine 2");
            //yield return null;

            AnalyzePitch();
            AnalyzeLoudness();
            //yield return new WaitForSeconds(0.5f);
        }
    }

    public float GetPitchData()
    {
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        audioSource.clip.GetData(samples, 0);

        float maxFrequency = audioSource.clip.frequency / 2;
        float[] spectrum = new float[512];  // Adjust the size based on your needs

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

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

        return pitchFrequency;
    }

    public void AnalyzePitch()
    {
        float pitchFrequency = GetPitchData();

        if (pitchFrequency < 250)
        {
            Debug.Log("Low Pitch");
            currentPitchTMP.text = "Low: " + pitchFrequency;
        }
        else if (pitchFrequency >= 250 && pitchFrequency <= 4000)
        {
            Debug.Log("Mid Pitch");
            currentPitchTMP.text = "Mid: " + pitchFrequency;
        }
        else
        {
            Debug.Log("High Pitch");
            currentPitchTMP.text = "High: " + pitchFrequency;
        }
    }

    public void AnalyzeLoudness()
    {
        float[] spectrumData = new float[256];
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        float loudness = 0f;

        foreach (float value in spectrumData)
        {
            loudness += value;
        }


        currentLoudnessTMP.text = ""+loudness;
    }
}
