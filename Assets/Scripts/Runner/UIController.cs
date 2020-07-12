using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack;
    public bool Start;
    public event Action StartGame;
    public event Action RestartGame;
    public event Action BackToMenu;
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
