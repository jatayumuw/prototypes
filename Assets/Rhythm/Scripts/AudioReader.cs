using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReader : MonoBehaviour
{
    [SerializeField] ObjectPoolManager poolManager;
    public AudioSource audioSource;
    public int sampleCount = 256;
    public int amplitudeMappingMin = -1;
    public int amplitudeMappingMax = 2;
    public float sampleInterval = 0.5f;
    public int amplitudeAsInt;

    private float[] spectrumData;
    private float nextSampleTime;

    private void OnEnable()
    {
        spectrumData = new float[sampleCount];
        nextSampleTime = Time.time;
    }

    private IEnumerator Start()
    {
        yield return null;
        while (true)
            if (audioSource.isPlaying && Time.time >= nextSampleTime)
            {
                AudioSource();
            }
    }

    private void Update()
    {

    }

    void AudioSource()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Blackman);

        float totalAmplitude = 0;

        for (int i = 0; i < sampleCount; i++)
        {
            totalAmplitude += spectrumData[i];
        }

        float mappedAmplitude = Mathf.Lerp(amplitudeMappingMin, amplitudeMappingMax, totalAmplitude);

        amplitudeAsInt = Mathf.RoundToInt(mappedAmplitude);
        nextSampleTime = Time.time + sampleInterval;
        Debug.Log("int Amplitude: " + amplitudeAsInt);

        StartCoroutine(LoopSpawn());
    }

    IEnumerator LoopSpawn()
    {
        yield return new WaitForSeconds(2);
        if (amplitudeAsInt > 0)
        {
            poolManager.EnableObjectsBasedOnAmplitude(1); // Spawn using element 1 for positive values
        }
        else if (amplitudeAsInt < 0)
        {
            poolManager.EnableObjectsBasedOnAmplitude(2); // Spawn using element 2 for negative values
        }
        else
        {
            poolManager.EnableObjectsBasedOnAmplitude(0); // Spawn using element 2 for negative values
        }

        yield return null;
    }
}
