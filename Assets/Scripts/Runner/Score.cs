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
    //public void AddScore(float time,int numberOfQuestion)
    //{
    //    score += time * numberOfQuestion;
    //}
    public void AddScore(float time, float x)   
    {
        score += time * x;
    }
    public int GetTotalScore()
    {
        return (int)(score);
    }
    

}
