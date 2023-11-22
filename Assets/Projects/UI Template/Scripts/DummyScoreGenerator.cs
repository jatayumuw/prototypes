using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScoreGenerator : MonoBehaviour
{
    [System.Serializable]
    public class ScoreClass
    {
        public string className;
        public int score;
    }

    public ScoreClass[] scoreClassesArray;

    public int GenerateScore(string className)
    {
        foreach (var scoreClass in scoreClassesArray)
        {
            if (scoreClass.className == className)
            {
                return Random.Range(0, 100);
            }
        }

        return -1;
    }
}
