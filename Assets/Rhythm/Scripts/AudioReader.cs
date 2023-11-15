using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AudioReader : MonoBehaviour
{
    [Header("Main Parameters")]
    [SerializeField] ObjectPoolManager poolManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] int sampleCount;
    [SerializeField] int amplitudeMappingMin;
    [SerializeField] int amplitudeMappingMax;
    [SerializeField] float sampleInterval;

    [Header("Game Audio Parameter related")]
    public int amplitudeDivider;
    public int middleTreshold;
    public int highTreshold;

    private float[] spectrumData;
    private float nextSampleTime;
    private int amplitudeAsInt;

    [SerializeField] private int beatCount;
    private int BeatCount { get => beatCount; set {
            if (beatCount > Random.Range(1, 10))
            {
                if (isHigh)
                {
                    isHigh = false;
                }
                else
                {
                    isHigh = true;
                }
                beatCount = 0;
            }
            else
            {
                beatCount = value;
            }
        } 
    }

    bool isHigh;

    private void OnEnable()
    {
        spectrumData = new float[sampleCount];
        nextSampleTime = Time.time;
    }

    private IEnumerator Start()
    {
        yield return null;
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
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        float totalAmplitude = 0;

        for (int i = 0; i < sampleCount; i++)
        {
            totalAmplitude += spectrumData[i];
        }

        float mappedAmplitude = Mathf.Lerp(amplitudeMappingMin, amplitudeMappingMax, totalAmplitude) / amplitudeDivider;

        amplitudeAsInt = Mathf.RoundToInt(mappedAmplitude);
        nextSampleTime = Time.time + sampleInterval;

        float targetFrequency = 234f;
        float hertzPerBin = (float)AudioSettings.outputSampleRate / 2f / sampleCount;
        int targetIndex = (int)(targetFrequency / hertzPerBin);


        LoopSpawn(targetIndex, hertzPerBin, amplitudeAsInt);
    }

    void LoopSpawn(int targetIndex, float hertzPerBin, float outputValue)
    {
        string outString = "";
        for (int i = targetIndex - 3; i <= targetIndex + 3; i++)
        {
            outString += string.Format("| Bin {0} : {1}Hz : {2} |   ", i, i * hertzPerBin, spectrumData[i]);
            //yield return new WaitForSeconds(0.01f);

            if (isHigh)
            {
                if (i == 6 && spectrumData[i] > .005f)
                {
                    poolManager.EnableObjectsBasedOnAmplitude(1, outputValue); // Spawn using element 1 for Mid
                     BeatCount++;
                }
            }
            else
            {
                if (i == 12 && spectrumData[i] > .005f)
                {
                    poolManager.EnableObjectsBasedOnAmplitude(2, outputValue); // Spawn using element 2 for High
                    BeatCount++;
                }
            }

        }
        Debug.Log(outString); 
    }
}
