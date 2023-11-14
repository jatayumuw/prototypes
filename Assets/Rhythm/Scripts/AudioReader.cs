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

    private void Start()
    {
        spectrumData = new float[sampleCount];
        nextSampleTime = Time.time;
    }

    private void Update()
    {
        if (audioSource.isPlaying && Time.time >= nextSampleTime)
        {
            AudioSource();
        }
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
            ObjectPoolManager.Instance.SpawnObjectsBasedOnAmplitude(1); // Spawn using element 1 for positive values
        }
        else if (amplitudeAsInt < 0)
        {
            ObjectPoolManager.Instance.SpawnObjectsBasedOnAmplitude(2); // Spawn using element 2 for negative values
        }
        else
        {
            ObjectPoolManager.Instance.SpawnObjectsBasedOnAmplitude(0); // Spawn using element 2 for negative values
        }

        yield return null;
    }
}
