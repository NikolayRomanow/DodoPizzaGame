﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SupremumStudio;
using System;
using DodoDataModel;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEditor;
using System.Net.Sockets;

public class GameManager : MonoBehaviour
{
    private float Rating;
    public UIController UIController;
    public Run Run;
    public QuizView QuizView;
    public Quiz Quiz;
    public Score GameScore;
    public SpeedController SpeedController;
    public SoundController SoundController;
    public int bestRating, currentRating;


    //Серверная инициализация
    private bool isConnect = false;
    public float timeTenSec = 10f;
    //private string url = "http://localhost:5001/hello";
    private string url = "http://89.223.126.195:80/hello";
    private HubConnection hubConnection = null;
    private UnityMainThreadDispatcher _dispatcher;
    public User user = new User();
    private async void Awake()
    {
        Run.SoundOfDeath += Run_SoundOfDeath;
        Run.Death += Run_Death;
        Run.NewVopros += Run_NewVopros;
        Run.CheckArrived += SetSpeed;
        Run.WinOrNot += Run_WinOrNot;
        QuizView.CorrectAnswer += QuizView_CorrectAnswer;
        QuizView.InCorrectAnswer += QuizView_InCorrectAnswer;
        UIController.StartGame += UIController_StartGame;
        UIController.RestartGame += UIController_RestartGame;
        UIController.BackToMenu += UIController_BackToMenu;
        Run.QuizView = QuizView;
        QuizView.Quiz = Quiz;
        GameScore = new Score();

    }

    private void Run_WinOrNot()
    {
        if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        {
            SetSpeed(0);
            UIController.WinZoneOn();
            SoundController.SoundInGameOff();
            UIController.CanvasScoreZoneOn();
            UIController.ScoreZoneOn();
            SaveRating();
            UIController.SetBestRating(bestRating);
            UIController.SetCurrentRating(GameScore.GetTotalScore());
            UIController.MainCameraOff();
        }
    }

    //TODO: Разобраться с проблемой двойной анимации на тригере Prep.



    //public static Action myStart = () => { Debug.Log("myStart"); };
    //public static Action myUpdate = () => { Debug.Log("myUpdate"); };

    private void Start()
    {
        UIController.NewStartPanel.gameObject.SetActive(false);
        //PlayerPrefs.SetString("GUID", String.Empty);
        //PlayerPrefs.SetInt("BestScore", 0);
        GameScore.ResetScore();
        UIController.SetBestRating(PlayerPrefs.GetInt("BestScore"));
        //myStart();
        SaveRating();
        SoundController.SoundInMenuOn();
        _dispatcher = UnityMainThreadDispatcher.Instance();
        StartServer();

    }

    private async void StartServer()
    {
        await this.StartSignalRAsync();
    }

    async Task StartSignalRAsync()
    {
        if (this.hubConnection == null)
        {
            // create hub and settings
            this.hubConnection = new HubConnectionBuilder()
                .WithUrl(url, options => { })
                .Build();

            // start server

            await this.hubConnection.StartAsync();


        }

    }

    private async void Update()
    {        
        if (isConnect == false)
        {
            if (timeTenSec >= 0)
            {
                if (this.hubConnection.State.ToString() == "Connected")
                {
                    //Debug.Log("Подключен");
                    isConnect = true;
                    timeTenSec = -3;
                    user.guid = PlayerPrefs.GetString("GUID");
                    if (PlayerPrefs.GetString("GUID", String.Empty) == String.Empty)
                    {
                        PlayerPrefs.SetString("GUID", Guid.NewGuid().ToString());
                        user.guid = PlayerPrefs.GetString("GUID");
                        user.Name = "Player";
                        await hubConnection.InvokeAsync("Registration", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                    }
                    else
                    {
                        user.guid = PlayerPrefs.GetString("GUID");
                        user.Name = "Player";
                        string temp1 = await hubConnection.InvokeAsync<string>("CheckUser", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                        if (temp1 == "false")
                        {
                            user.guid = PlayerPrefs.GetString("GUID");
                            user.Name = "Player";
                            user.Score = PlayerPrefs.GetInt("BestScore");
                            await hubConnection.InvokeAsync("Registration", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                        }
                    }
                    int temp = await hubConnection.InvokeAsync<int>("GetRating", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                    UIController.SetRatingInMenu(temp);
                    UIController.NewStartPanel.gameObject.SetActive(true);
                    UIController.NewStartPanel.Play("on");
                    UIController.LoadServer.SetActive(false);
                }

                timeTenSec -= Time.deltaTime;
            }
            if (timeTenSec < 0 && timeTenSec > -3 && isConnect == false)
            {
                Debug.Log("Нет конекта");
                UIController.InternetErorr();
                isConnect = true;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        Statistic.Speed = speed;
    }

    private void UIController_RestartGame()
    {
        if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        {
            UIController.WinZoneOff();
        }
        GameScore.ResetScore();
        user.Score = 0;
        Quiz.ResetQuiz();
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();

    }
    private void UIController_BackToMenu()
    {
        if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        {
            UIController.WinZoneOff();
        }
        GameScore.ResetScore();
        Quiz.ResetQuiz();
        SoundController.SoundOfPressedButton();
        SoundController.SoundInMenuOn();

    }

    private void UIController_StartGame()
    {
        UIController.SetBestRatingInGame(bestRating);
        GameScore.ResetScore();
        Statistic.isGameStart = true;
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();
        SoundController.SoundInMenuOff();
    }

    private void QuizView_InCorrectAnswer()
    {

        //UIController.VictorineZoneOff();
        //UIController.TimerOff();
        SoundController.SoundOfInCorrectAnswer();
        UIController.CanvasVictorineZoneOff();
        SetSpeed(9);
    }

    private void QuizView_CorrectAnswer(float deltaTime)
    {
        //UIController.VictorineZoneOff();
        GameScore.AddScore(deltaTime, Quiz.currentQuestion + 1);
        BestRatingInGame();
        UIController.SetCurrentRatingInGame(GameScore.GetTotalScore());
        UIController.SetBestRatingInGame(bestRating);
        SoundController.SoundOfCorrectAnswer();
        UIController.CanvasVictorineZoneOff();
        //UIController.TimerOff();
        SetSpeed(9);
        //if(Quiz.CurrentQuestion==Quiz.questions.Count-1)
        //{
        //    SetSpeed(0);
        //    SoundController.SoundInGameOff();
        //    UIController.CanvasScoreZoneOn();
        //    UIController.ScoreZoneOn();
        //    SaveRating();
        //    UIController.SetBestRating(bestRating);
        //    UIController.SetCurrentRating(GameScore.GetTotalScore());
        //    UIController.MainCameraOff();
        //}

    }

    private void Run_NewVopros()
    {
        //if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        //{
        //    SetSpeed(0);
        //    SoundController.SoundInGameOff();
        //    UIController.CanvasScoreZoneOn();
        //    UIController.ScoreZoneOn();
        //    SaveRating();
        //    UIController.SetBestRating(bestRating);
        //    UIController.SetCurrentRating(GameScore.GetTotalScore());
        //    UIController.MainCameraOff();
        //}
        //else
        //{
        Statistic.Speed = 3f;
        if (QuizView.QuestionIsOn == false)
        {
            UIController.VictorineZoneOn();
            UIController.TimerOn();
        }
        QuizView.ResetColors();
        QuizView.IsCorrectAnswerFalse();
        QuizView.ResetTime();
        QuizView.ResetSliderValue();
        Quiz.NextQuestion();
        QuizView.QuestinIsTrueOn();
        UIController.CanvasVictorineZoneOn();
        
        //}
    }

    private async void Run_Death()
    {
        
        SetSpeed(0);
        SoundController.SoundInGameOff();
        UIController.CanvasScoreZoneOn();
        if (QuizView.QuestionIsOn == true)
        {
            UIController.VictorineZoneOff();
            UIController.TimerOff();
        }
        QuizView.QuestinIsTrueOff();
        UIController.ScoreZoneOn();
        SaveRating();
        UIController.SetBestRating(bestRating);
        UIController.SetCurrentRating(GameScore.GetTotalScore());
        UIController.MainCameraOff();
        user.Score = PlayerPrefs.GetInt("BestScore");
        await hubConnection.InvokeAsync("CheckRating", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        user.Score = GameScore.GetTotalScore();
        await hubConnection.InvokeAsync("SetScore", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        int temp = await hubConnection.InvokeAsync<int>("GetRating", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        UIController.SetRatingInMenu(temp);
    }
    public void BestRatingInGame()
    {
        if (GameScore.GetTotalScore() > bestRating)
        {
            bestRating = GameScore.GetTotalScore();
        }
    }

    private void Run_SoundOfDeath()
    {
        SoundController.SoundOfCrash();
    }
    public void SaveRating()
    {
        bestRating = PlayerPrefs.GetInt("BestScore");
        if (GameScore.GetTotalScore() > bestRating)
        {
            PlayerPrefs.SetInt("BestScore", GameScore.GetTotalScore());
        }
        bestRating = PlayerPrefs.GetInt("BestScore");
    }

    //private void Update()
    //{
    //    Debug.Log(GameScore.GetTotalScore());
    //}
    private void OnDestroy()
    {
        hubConnection.StopAsync();
    }

    IEnumerator ZaderjkaInterfeicov()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
