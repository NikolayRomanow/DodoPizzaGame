using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack, Timer, WinZone, InfoPanel, NewStartPanel,DarkScreen;
    public CanvasGroup CanvasVictorineZone, CanvasScoreZone, CanvasStartBack;
    public Text BestRatingInMenu, CurrentRatingInRestartMenu, BestRatingInRestartMenu, RatingInMenu, RatingInRestartMenu, CurrentRatingInGame, BestRatingInGame;
    public bool Start;
    public event Action StartGame;
    public event Action RestartGame;
    public event Action BackToMenu;
    public GameObject DodoIdle;
    public GameObject LoadServer;
    public GameObject RunnerDodo;
    public GameObject FirstVoprosTrigger;
    public GameObject ConnectionOffinStartPanel, ConnectionOffinRestartPanel;
    private Animator Animator;

    public void InternetErorr()
    {
        NewStartPanel.gameObject.SetActive(true);
        NewStartPanel.Play("on");
        LoadServer.SetActive(false);
        ConnectionOffinStartPanel.SetActive(true);
        ConnectionOffinRestartPanel.SetActive(true);
        RatingInMenu.text = "";
        RatingInRestartMenu.text = "";
        NewStartPanel.Play("on");
    }
    public void DarkScreenOn()
    {
        DarkScreen.SetTrigger("on");
    }
    public void SetRatingInMenu(int rating)
    {
        RatingInMenu.text = rating.ToString();
        RatingInRestartMenu.text = rating.ToString();
    }

    public void SetBestRating(int bestRating)
    {
        BestRatingInMenu.text = bestRating.ToString();
        BestRatingInRestartMenu.text = bestRating.ToString();
    }

    public void SetBestRatingInGame(int bestRatingInGame)
    {
        BestRatingInGame.text = bestRatingInGame.ToString();        
    }
    public void SetCurrentRatingInGame(int currentRatingInGame)
    {
        CurrentRatingInGame.text = currentRatingInGame.ToString();       
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
    public void ARScene()
    {
        Application.LoadLevel(1);
    }

    public void InfoButtonOn()
    {
        StartBackOff();
        InfoPanelOn();
    }
    public void InfoButtonOff()
    {
        InfoPanelOff();
        StartBackOn();
        
    }
    public void StartTheGame()  
    {
        DarkScreenOn();
        StartCoroutine(WaitIdle());
        StartGame();        
        MainCameraOn();
        StartBackOff();
        CanvasStartBackOff();
    }
    public void RestartTheGame()
    {
        DarkScreenOn(); 
        FirstVoprosTrigger.SetActive(true);
        StartCoroutine(WaitRun());
        RestartGame();        
        MainCameraOn();
        ScoreZoneOff();
        CanvasStartBack.interactable = false;
        CanvasScoreZoneOff();
        CanvasStartBackOn();
    }
    public void BackToTheMenu()
    {
        DarkScreenOn();
        BackToMenu();
        ScoreZoneOff();
        StartBackOn();
        CanvasScoreZoneOff();
        CanvasStartBackOn();
        StartCoroutine(StayRunner());
        MainCamera.Play("ToHomeCamera");
        FirstVoprosTrigger.SetActive(true);
        StartCoroutine(BackToMenuCorutine());
    }
    public void InfoPanelOn()
    {
        InfoPanel.SetTrigger("on");
    }
    public void InfoPanelOff()
    {
        InfoPanel.SetTrigger("off");
    }
    public void TimerOn()
    {
        Timer.SetTrigger("on");
    }
    public void TimerOff()
    {
        Timer.SetTrigger("off");
    }
    public void WinZoneOn()
    {
        WinZone.SetTrigger("on");
    }
    public void WinZoneOff()
    {
        WinZone.SetTrigger("off");
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

    IEnumerator StayRunner()
    {
        yield return new WaitForSeconds(1);
        //RunnerDodo.SetActive(true);
        //RunnerDodo.GetComponent<Animator>().Play("Ride");
    }

    IEnumerator WaitIdle()
    {
        yield return new WaitForSeconds(1.0f);
        DodoIdle.SetActive(false);
    }

    IEnumerator WaitRun()
    {
        yield return new WaitForSeconds(1.0f);
        Animator = RunnerDodo.GetComponent<Animator>();
        Animator.Play("Ride");
    }

    IEnumerator BackToMenuCorutine()
    {
        yield return new WaitForSeconds(1.0f);
        DodoIdle.SetActive(true);
        Animator = RunnerDodo.GetComponent<Animator>();
        Animator.Play("Ride");
    }
}
