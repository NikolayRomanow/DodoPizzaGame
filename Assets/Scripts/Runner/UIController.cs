using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    public Animator MainCamera, VictorineZone, ScoreZone, StartBack, Timer, InfoPanel, NewStartPanel, DarkScreen, Result, ResultRecord, PrivetFirst, Ocenka, LanguagePanel, ResultRecordTop10;
    public CanvasGroup CanvasVictorineZone, CanvasScoreZone, CanvasStartBack;
    public Text BestRatingInMenu, CurrentRatingInRestartMenuResult, BestRatingInRestartMenuResultRecord, BestRatingInRestartMenuResult, RatingInMenu, RatingInRestartMenu, CurrentRatingInGame, DoHalyavnoyPizzaCountResult, DoHalyavnoyPizzaCountInMenu, DoHalyavnoyPizzaCountResultRecord, PositionCountResult, DoHalyavnoyPizzaCountInGame,ScoreRecordCount;
    public bool Start, NewRecord;
    public event Action StartGame;
    public event Action RestartGame;
    public event Action BackToMenu;
    public GameObject DodoIdle;
    public GameObject LoadServer;
    public GameObject RunnerDodo;
    public GameObject FirstVoprosTrigger;
    public GameObject House;
    public GameObject Spruces;
    public GameManager GameManager;
    public GameObject ConnectionOffinStartPanel, ConnectionOffinRestartPanel;
    private Animator Animator;

    //public void 
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
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.L))
    //    {
    //        OcenkaOn();
    //    }
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        PrivetFirstOn();
    //    }
    //}
    public void BadOcenka()
    {
        OcenkaOff();
    }
    public void GoodOcenka()
    {
        Application.OpenURL("https://dodopizza.ru/");
        OcenkaOff();
    }
    public void PrivetFirstOn()
    {
        PrivetFirst.SetTrigger("on");        
        InteractableCanvasOff();
    }
    public void PrivetFirstOff()
    {
        PrivetFirst.SetTrigger("off");
        GameManager.StartServer();
        InteractableCanvasOn();
    }
    public void OcenkaOn()
    {
        Ocenka.SetTrigger("on");
        InteractableCanvasOff();
    }
    public void OcenkaOff()
    {
        Ocenka.SetTrigger("off");
        InteractableCanvasOn();
    }
    public void ResultOn()
    {
        Result.SetTrigger("on");
    }
    public void ResultOff()
    {
        Result.SetTrigger("off");
    }
    public void ResultRecordOn()
    {
        ResultRecord.SetTrigger("on");
    }
    public void ResultRecordOff()
    {
        ResultRecord.SetTrigger("off");
    }
    public void ResultRecordTopTenOn()
    {
        ResultRecordTop10.SetTrigger("on");
    }
    public void ResultRecordTopTenOff()
    {
        ResultRecordTop10.SetTrigger("off");
    }
    public void DarkScreenOn()
    {
        DarkScreen.SetTrigger("on");
    }
    public void SetRatingInMenu(int rating, int placeCount)
    {
        RatingInMenu.text = rating.ToString()+" из "+ placeCount.ToString();
        RatingInRestartMenu.text = rating.ToString();
    }

    public void SetBestRating(int bestRating)
    {
        BestRatingInMenu.text = bestRating.ToString();
        BestRatingInRestartMenuResultRecord.text = bestRating.ToString();
        BestRatingInRestartMenuResult.text = bestRating.ToString();
        ScoreRecordCount.text = bestRating.ToString();
    }
    public void SetDoHalyavnoyPizzaCount(float doHalyavnoyPizzaCount)
    {
        if (doHalyavnoyPizzaCount > 0)
        {
            DoHalyavnoyPizzaCountInMenu.text = doHalyavnoyPizzaCount.ToString();
            DoHalyavnoyPizzaCountResult.text = doHalyavnoyPizzaCount.ToString();
            DoHalyavnoyPizzaCountResultRecord.text = doHalyavnoyPizzaCount.ToString();
            DoHalyavnoyPizzaCountInGame.text = doHalyavnoyPizzaCount.ToString();
        }
        else
        {
            DoHalyavnoyPizzaCountInMenu.text = "Ты в топе";
            DoHalyavnoyPizzaCountResult.text = "Ты в топе";
            DoHalyavnoyPizzaCountResultRecord.text = "Ты в топе";
            DoHalyavnoyPizzaCountInGame.text = "Ты в топе";
        }
    }    
    public void PositionCount(int positionCount)
    {
        //PositionCountResultRecord.text = positionCount.ToString();
       PositionCountResult.text = positionCount.ToString();
    }    
    public void SetCurrentRatingInGame(int currentRatingInGame)
    {
        CurrentRatingInGame.text = currentRatingInGame.ToString();
    }
    public void SetCurrentRating(int currentRating)
    {
        //CurrentRatingInRestartMenuResultRecord.text = currentRating.ToString();
        CurrentRatingInRestartMenuResult.text = currentRating.ToString();
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
    public void LanguageButtonOn()
    {
        StartBackOff();
        LanguageOn();
    }
    public void LanguageButtonOff()
    {
        LanguageOff();
        StartBackOn();
    }
    public void LanguageOn()
    {
        LanguagePanel.SetTrigger("on");
    }
    public void LanguageOff()
    {
        LanguagePanel.SetTrigger("off");
    }
    
    public void FirstLaunch()
    {
        PrivetFirstOff();
        NewStartPanel.SetTrigger("on");
    }

    public void StartTheGame()
    {
        //PrivetFirstOff();
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
        switch (NewRecord)
        {
            case true:
                ResultRecordOff();
                break;
            case false:
                ResultOff();
                break;
        }


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
        switch (NewRecord)
        {
            case true:
                ResultRecordOff();
                break;
            case false:
                ResultOff();
                break;
        }
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
        House.SetActive(true);
        Spruces.SetActive(false);
        Animator = RunnerDodo.GetComponent<Animator>();
        Animator.Play("Ride");
    }
    
    IEnumerator BackToMenuCorutine()
    {
        yield return new WaitForSeconds(1.0f);
        DodoIdle.SetActive(true);
        House.SetActive(true);
        Spruces.SetActive(false);
        Animator = RunnerDodo.GetComponent<Animator>();
        Animator.Play("Ride");
    }
}
