using System.Collections;
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
        QuizView.CorrectAnswer += QuizView_CorrectAnswer;
        QuizView.InCorrectAnswer += QuizView_InCorrectAnswer;
        UIController.StartGame += UIController_StartGame;
        UIController.RestartGame += UIController_RestartGame;
        UIController.BackToMenu += UIController_BackToMenu;
        Run.QuizView = QuizView;
        QuizView.Quiz = Quiz;
        GameScore = new Score();

    }
    //TODO: Разобраться с проблемой двойной анимации на тригере Prep.



    //public static Action myStart = () => { Debug.Log("myStart"); };
    //public static Action myUpdate = () => { Debug.Log("myUpdate"); };

    private async void Start()
    {
        GameScore.ResetScore();
        //myStart();
        SaveRating();
        SoundController.SoundInMenuOn();
        _dispatcher = UnityMainThreadDispatcher.Instance();
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
                user.Rating = 0;
                user.Score = 0;
                user.Name = "Player";
            }
        }
    }

    //private void Update()
    //{
    //    myUpdate();
    //}    
    public void SetSpeed(float speed)
    {
        Statistic.Speed = speed;
    }

    private void UIController_RestartGame()
    {
        GameScore.ResetScore();
        user.Score = 0;
        Quiz.ResetQuiz();
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();
    }
    private void UIController_BackToMenu()
    {
        GameScore.ResetScore();
        Quiz.ResetQuiz();
        SoundController.SoundOfPressedButton();
        SoundController.SoundInMenuOn();
    }

    private void UIController_StartGame()
    {
        GameScore.ResetScore();
        Statistic.isGameStart = true;
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();
        SoundController.SoundInMenuOff();
    }

    private void QuizView_InCorrectAnswer()
    {   UIController.VictorineZoneOff();
        SoundController.SoundOfInCorrectAnswer();
        UIController.CanvasVictorineZoneOff();
        SetSpeed(9);
    }

    private void QuizView_CorrectAnswer(float deltaTime)
    {
        UIController.VictorineZoneOff();
        GameScore.AddScore(deltaTime, Quiz.currentQuestion + 1);
        SoundController.SoundOfCorrectAnswer();
        UIController.CanvasVictorineZoneOff();

        SetSpeed(9);
    }

    private void Run_NewVopros()
    {
        Quiz.NextQuestion();
        QuizView.QuestinIsTrueOn();
        UIController.CanvasVictorineZoneOn();
    }

    private async void Run_Death()
    {
        SetSpeed(0);
        SoundController.SoundInGameOff();
        UIController.CanvasScoreZoneOn();
        if (QuizView.QuestionIsOn == true)
        {
            UIController.VictorineZoneOff();
        }
        UIController.ScoreZoneOn();
        SaveRating();
        UIController.SetBestRating(bestRating);
        UIController.SetCurrentRating(GameScore.GetTotalScore());
        UIController.MainCameraOff();
        user.Score = GameScore.GetTotalScore();
        await hubConnection.InvokeAsync("SetScore", Newtonsoft.Json.JsonConvert.SerializeObject(user));
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

}
