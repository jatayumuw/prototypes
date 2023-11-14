using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaikoGameManager : MonoBehaviour
{
    [Header("Player Data")]
    public string playerName; //player name data
    public int playerScore; //current player score data (need to turn in to string later)
    public int hitCount, missCount; //hit n miss data
    public int currentScoreMultipler, defaultScoreMultiplier, increementCount; //increement related data

    public void Start()
    {
        currentScoreMultipler = defaultScoreMultiplier;
    }

    void Update()
    {

    }

    public void PlayerHitNode()
    {
        playerScore += currentScoreMultipler;

        hitCount++;

        //animate & effect later
    }

    public void PlayerMissNode()
    {
        currentScoreMultipler += defaultScoreMultiplier;
        missCount++;

        //animate & effect later
    }
}
