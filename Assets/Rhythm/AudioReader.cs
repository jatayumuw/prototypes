using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReader : MonoBehaviour
{
    public AudioSource audioSource;
    public int sampleCount = 256; // You can adjust this based on your needs
    public int amplitudeMappingMin = -1; // The minimum mapped value
    public int amplitudeMappingMax = 2; // The maximum mapped value
    public float sampleInterval = 0.5f; // Interval between amplitude samples

    private float[] spectrumData;
    private float nextSampleTime;

    private void Start()
    {
        spectrumData = new float[sampleCount];
        nextSampleTime = Time.time;
    }

    private void Update()
    {
        if (audioSource.isPlaying && Time.time >= nextSampleTime)
        {
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

            float totalAmplitude = 0;

            for (int i = 0; i < sampleCount; i++)
            {
                totalAmplitude += spectrumData[i];
            }

            float mappedAmplitude = Mathf.Lerp(amplitudeMappingMin, amplitudeMappingMax, totalAmplitude);

            int amplitudeAsInt = Mathf.RoundToInt(mappedAmplitude);

            Debug.Log("Amplitude as Integer: " + amplitudeAsInt);

            // Set the next sample time
            nextSampleTime = Time.time + sampleInterval;
        }
    }
}
