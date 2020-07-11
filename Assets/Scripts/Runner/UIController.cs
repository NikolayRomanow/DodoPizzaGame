using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack;
    public bool Start;

    public void StartTheGame()
    {
        Statistic.Score = 0;
        Statistic.Speed = 3f;
        MainCameraOn();
        //VictorineZoneOn();
        StartBackOff();
        Statistic.BOOL = true;
        
    }
    public void RestartTheGame()
    {
        Statistic.Score = 0;
        Statistic.Speed = 3f;
        MainCameraOff();
        ScoreZoneOff();
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
