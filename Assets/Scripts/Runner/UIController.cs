using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack;
    public Text BestRatingInMenu, CurrentRatingInRestartMenu,BestRatingInRestartMenu;
    public bool Start;
    public event Action StartGame;
    public event Action RestartGame;
    public event Action BackToMenu;

    public void SetBestRating(int bestRating)
    {
        BestRatingInMenu.text = bestRating.ToString();
        BestRatingInRestartMenu.text = bestRating.ToString();
    }
    public void SetCurrentRating(int currentRating)
    {
        CurrentRatingInRestartMenu.text = currentRating.ToString();
    }
    public void StartTheGame()
    {
        StartGame();        
        MainCameraOn();
        StartBackOff();        
    }
    public void RestartTheGame()
    {
        RestartGame();        
        MainCameraOn();
        ScoreZoneOff();
    }
    public void BackToTheMenu()
    {
        BackToMenu();
        ScoreZoneOff();
        StartBackOn();
    }
    public void ScoreZoneOn()
    {
        ScoreZone.SetTrigger("on");
    }
    public void ScoreZoneOff()
    {
        ScoreZone.SetTrigger("off");
    }
    public void VictorineZoneOn()
    {
        VictorineZone.SetTrigger("on");
    }
    public void VictorineZoneOff()
    {
        VictorineZone.SetTrigger("off");
    }
    public void MainCameraOn()
    {
        MainCamera.SetTrigger("Menu");
    }
    public void MainCameraOff()
    {
        MainCamera.SetTrigger("Start");
    }
    public void StartBackOn()
    {
        StartBack.SetTrigger("on");
    }
    public void StartBackOff()
    {
        StartBack.SetTrigger("off");
    }
}
