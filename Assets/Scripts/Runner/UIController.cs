using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack, Timer;
    public CanvasGroup CanvasVictorineZone, CanvasScoreZone, CanvasStartBack;
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
    public void InteractableCanvasOn()
    {
        
        CanvasStartBack.interactable = true;
        CanvasVictorineZone.interactable = true;        
    }
    public void InteractableCanvasOff()
    {
        CanvasScoreZone.interactable = false;
        CanvasStartBack.interactable = false;
        CanvasVictorineZone.interactable = false;
    }
    public void CanvasScoreZoneOn()
    {
        CanvasScoreZone.interactable = true;
    }
    public void CanvasScoreZoneOff()
    {
        CanvasScoreZone.interactable = false;
    }
    public void CanvasStartBackOn()
    {
        CanvasStartBack.interactable = true;
    }
    public void CanvasStartBackOff()
    {
        CanvasStartBack.interactable = false;
    }
    public void CanvasVictorineZoneOn()
    {
        CanvasVictorineZone.interactable = true;
    }
    public void CanvasVictorineZoneOff()
    {
        CanvasVictorineZone.interactable = false;
    }

    public void StartTheGame()
    {
        StartGame();        
        MainCameraOn();
        StartBackOff();
        CanvasStartBackOff();
    }
    public void RestartTheGame()
    {
        RestartGame();        
        MainCameraOn();
        ScoreZoneOff();
        CanvasStartBack.interactable = false;
        CanvasScoreZoneOff();
        CanvasStartBackOn();

    }
    public void BackToTheMenu()
    {
        BackToMenu();
        ScoreZoneOff();
        StartBackOn();
        CanvasScoreZoneOff();
        CanvasStartBackOn();
    }
    public void TimerOn()
    {
        Timer.SetTrigger("on");
    }
    public void TimerOff()
    {
        Timer.SetTrigger("off");
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
