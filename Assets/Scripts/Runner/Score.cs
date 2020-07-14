using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private float score;

    public void ResetScore()
    {
        score = 0;
    }
    public void AddScore(float time,int numberOfQuestion)
    {
        score += time * numberOfQuestion;
    }
    public int GetTotalScore()
    {
        return (int)(score * 100);        

    }

}
