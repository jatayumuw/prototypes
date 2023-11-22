using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGameManager : MonoBehaviour
{
    [Header("Game")]
    public bool gameStart = false;

    [Header("AudioPlayer")]
    public int delayTime;
    public AudioSource anySource;

    [Header("Data")]
    //string filename = "";
    public string playerName;
    public string loginTime, playerScore; //player basic data 
    public int hitCount, missedCount; //player statistic related data
    public int currentScoreMultiplier, increementScoreMultiplier, maxScoreMultiplier, defaultScoreMultiplier = 10; //score multiplier
    public int currentCombo, maxCombo; //player combo

    IEnumerator Start()
    {
        yield return null;
        currentScoreMultiplier = defaultScoreMultiplier;
        Debug.Log("GameManager: Coroutine Start Is Called");

        //Waiting until game start is true
        yield return new WaitUntil(() => gameStart);
        yield return new WaitForSeconds(3f);
        anySource.Play();
    }
}
