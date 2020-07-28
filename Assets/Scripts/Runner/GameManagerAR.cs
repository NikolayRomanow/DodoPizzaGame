using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAR : MonoBehaviour
{
    private int BestScoreInThreeDGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CheckBestScore()
    {
        BestScoreInThreeDGame = PlayerPrefs.GetInt("BestScore");
    }
    public void SetSitingsDodo()
    {
        if (50 < BestScoreInThreeDGame) 
        {
            //Что-то включать
        }
        if (100 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (150 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (200 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (250 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (250 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (300 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (350 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
        if (400 < BestScoreInThreeDGame)
        {
            //Что-то включать
        }
    }
    private void Awake()
    {
        CheckBestScore();
        SetSitingsDodo();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
