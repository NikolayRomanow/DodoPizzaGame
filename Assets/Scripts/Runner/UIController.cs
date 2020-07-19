using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack, Timer, WinZone, InfoPanel, NewStartPanel, LoadPanel;
    public CanvasGroup CanvasVictorineZone, CanvasScoreZone, CanvasStartBack;
    public Text BestRatingInMenu, CurrentRatingInRestartMenu,BestRatingInRestartMenu, RatingInMenu, RatingInRestartMenu;
    public bool Start;
    public event Action StartGame;
    public event Action RestartGame;
    public event Action BackToMenu;
    public GameObject DodoIdle;
    public GameObject RunnerDodo;
    public GameObject FirstVoprosTrigger;
    public GameObject ConnectionOffinStartPanel, ConnectionOffinRestartPanel;
    private Animator Animator;

    public void InternetErorr()
    {
        NewStartPanel.gameObject.SetActive(true);
        NewStartPanel.Play("on");
        LoadPanel.gameObject.SetActive(false);
        ConnectionOffinStartPanel.SetActive(true);
        ConnectionOffinRestartPanel.SetActive(true);
        RatingInMenu.text = "";
        RatingInRestartMenu.text = "";
        StartBackOn();
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
        DodoIdle.SetActive(false);
        StartGame();        
        MainCameraOn();
        StartBackOff();
        CanvasStartBackOff();
    }
    public void RestartTheGame()
    {
        FirstVoprosTrigger.SetActive(true);
        Animator = RunnerDodo.GetComponent<Animator>();
        Animator.Play("Ride");
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
        DodoIdle.SetActive(true);
        RunnerDodo.SetActive(false);
        StartCoroutine(StayRunner());
        MainCamera.Play("ToHomeCamera");
        FirstVoprosTrigger.SetActive(true);

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
        RunnerDodo.SetActive(true);
        RunnerDodo.GetComponent<Animator>().Play("Ride");
    }
}
