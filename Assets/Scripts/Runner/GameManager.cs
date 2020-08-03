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
using System.Net.Sockets;

public class GameManager : MonoBehaviour
{
    private float Rating;
    private bool WinnerYes;//Если да то что-то делать;
    private uint time;
    public UIController UIController;
    public NewMoveimentPlatform NewMoveimentPlatform;
    public Run Run;
    public QuizView QuizView;
    public Quiz Quiz;
    public Score GameScore;
    public SpeedController SpeedController;
    public SoundController SoundController;
    public RandomChislo RandomChislo;
    public Notifications Notifications;
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
        Run.ProvR += Run_ProvR;
        Run.ProvL += Run_ProvL;
        QuizView.CorrectAnswer += QuizView_CorrectAnswer;
        QuizView.InCorrectAnswer += QuizView_InCorrectAnswer;
        UIController.StartGame += UIController_StartGame;
        UIController.RestartGame += UIController_RestartGame;
        UIController.BackToMenu += UIController_BackToMenu;
        Run.QuizView = QuizView;
        QuizView.Quiz = Quiz;
        GameScore = new Score();

    }
    public async void SendButton()
    {
        Winner Player = new Winner();
        Player.Name = UIController.NameOfWinner.text;
        Player.City = UIController.TownOfWinner.text;
        Player.Number = UIController.NumberOfWinner.text;
        await hubConnection.InvokeAsync("SetWinner", Newtonsoft.Json.JsonConvert.SerializeObject(Player));
    }

    private void Run_ProvL()
    {
        UIController.SetCurrentRatingInGame(GameScore.GetTotalScore());
    }


    public void SendLenght()
    {
        switch (UIController.CurrentRatingInGame.text.Length)
        {
            case 1:
                RandomChislo.lenghtOfScore = 1;
                break;
            case 2:
                RandomChislo.lenghtOfScore = 2;
                break;
            case 3:
                RandomChislo.lenghtOfScore = 3;
                break;
            case 4:
                RandomChislo.lenghtOfScore = 4;
                break;
            case 5:
                RandomChislo.lenghtOfScore = 5;
                break;
            case 6:
                RandomChislo.lenghtOfScore = 6;
                break;
            case 7:
                RandomChislo.lenghtOfScore = 7;
                break;
            case 8:
                RandomChislo.lenghtOfScore = 8;
                break;
            case 9:
                RandomChislo.lenghtOfScore = 9;
                break;
            default:
                RandomChislo.lenghtOfScore = 0;
                break;
        }

    }
    private async void CountPlayers()
    {
        float CountOfPlayers = await hubConnection.InvokeAsync<float>("CountUsers");
    }

    private async void Run_ProvR()
    {
        UIController.SetCurrentRatingInGame(GameScore.GetTotalScore());
        float DoHalyavnoyPizzaCount = await hubConnection.InvokeAsync<float>("TOPScore");
        DoHalyavnoyPizzaCount += 1.0f;
        //DoHalyavnoyPizzaCount = DoHalyavnoyPizzaCount - PlayerPrefs.GetInt("BestScore");
        UIController.SetDoHalyavnoyPizzaCount(DoHalyavnoyPizzaCount);
    }

    private void Run_WinOrNot()
    {
        if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        {
            SetSpeed(0);
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


        //PlayerPrefs.DeleteAll();
        UIController.LoadServer.gameObject.SetActive(false);
        var g = PlayerPrefs.GetInt("FirstLanguageInTheGame");
        if (g == 0)
        {
            UIController.LanguageOn();
            PlayerPrefs.SetInt("FirstLanguageInTheGame", 1);
            UIController.NewStartPanel.gameObject.SetActive(false);
            //PlayerPrefs.SetString("GUID", String.Empty);
            //PlayerPrefs.SetInt("BestScore", 0);
            GameScore.ResetScore();
            UIController.SetBestRating(PlayerPrefs.GetInt("BestScore"));
            //myStart();
            SaveRating();
            SoundController.SoundInMenuOn();
            _dispatcher = UnityMainThreadDispatcher.Instance();
        }
        else
        {
            var i = PlayerPrefs.GetInt("FirstStartTheGame");
            if (i == 0)
            {
                UIController.NewStartPanel.gameObject.SetActive(false);
                UIController.PrivetFirstOn();
                PlayerPrefs.SetInt("FirstStartTheGame", 1);
                UIController.NewStartPanel.gameObject.SetActive(false);
                ////PlayerPrefs.SetString("GUID", String.Empty);
                ////PlayerPrefs.SetInt("BestScore", 0);
                //GameScore.ResetScore();
                //UIController.SetBestRating(PlayerPrefs.GetInt("BestScore"));
                ////myStart();
                //SaveRating();
                //SoundController.SoundInMenuOn();
                //_dispatcher = UnityMainThreadDispatcher.Instance();
            }
            else
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
        }
    }

    public async void StartServer()
    {
        UIController.LoadServer.gameObject.SetActive(true);
        await this.StartSignalRAsync();
        WinnerYes = await hubConnection.InvokeAsync<bool>("CheckWinner", Newtonsoft.Json.JsonConvert.SerializeObject(user));
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

            time = await hubConnection.InvokeAsync<uint>("GetTime");
        }

    }

    private async void Update()
    {
        time -= 17;
        var ts = TimeSpan.FromMilliseconds(time);
        UIController.SetTimerInMenu(ts.Days, ts.Hours, ts.Minutes, ts.Seconds);


        if (Statistic.RandomNumbersOn == true)
        {
            UIController.SetCurrentRatingInGame(RandomChislo.Chislo());
        }
        if (isConnect == false)
        {
            if (timeTenSec >= 0)
            {
                if (this.hubConnection.State.ToString() == "Connected")
                {



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
                    float CountOfPlayers = await hubConnection.InvokeAsync<float>("CountUsers");
                    UIController.SetRatingInMenu(temp, (int)CountOfPlayers);
                    float DoHalyavnoyPizzaCount = await hubConnection.InvokeAsync<float>("TOPScore");
                    DoHalyavnoyPizzaCount += 1.0f;
                    //DoHalyavnoyPizzaCount = DoHalyavnoyPizzaCount - PlayerPrefs.GetInt("BestScore");
                    UIController.SetDoHalyavnoyPizzaCount(DoHalyavnoyPizzaCount);
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
        //UIController.RunnerDodo.GetComponent<Collider>().enabled = true;
        //if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        //{
        //    UIController.WinZoneOff();
        //}
        GameScore.ResetScore();
        SetRatingInGame();
        user.Score = 0;
        Quiz.ResetQuiz();
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();

    }
    private void UIController_BackToMenu()
    {
        GameScore.ResetScore();
        SetRatingInGame();
        //if (Quiz.CurrentQuestion == Quiz.questions.Count - 1)
        //{
        //    UIController.WinZoneOff();
        //}
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
    {
        RandomNumbersOn();
        SendLenght();
        //UIController.VictorineZoneOff();
        //UIController.TimerOff();
        SoundController.SoundOfInCorrectAnswer();
        UIController.CanvasVictorineZoneOff();
        //SetSpeed(9);
    }

    private void QuizView_CorrectAnswer(float deltaTime)
    {
        RandomNumbersOn();
        SendLenght();
        //UIController.VictorineZoneOff();
        //GameScore.AddScore(deltaTime, Quiz.currentQuestion + 1);
        GameScore.AddScore(deltaTime, Quiz.Coefficient);
        BestRatingInGame();
        //UIController.SetCurrentRatingInGame(GameScore.GetTotalScore());
        //UIController.SetBestRatingInGame(bestRating);
        SoundController.SoundOfCorrectAnswer();
        UIController.CanvasVictorineZoneOff();
        //UIController.TimerOff();
        //SetSpeed(9);
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
        //SetSpeed(9);
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
    public void RandomNumbersOn()
    {
        Statistic.RandomNumbersOn = true;
    }
    public void RandomNumbersOff()
    {
        Statistic.RandomNumbersOn = false;
    }
    private async void Run_Death()
    {
        UIController.MainCameraOff();


        // else
        //{
        RandomNumbersOff();
        UIController.SetCurrentRatingInGame(GameScore.GetTotalScore());
        //UIController.RunnerDodo.GetComponent<CapsuleCollider>().isTrigger = true;
        SetSpeed(0);
        UIController.House.SetActive(false);
        UIController.Spruces.SetActive(true);
        //UIController.Spruces.SetActive(true);
        SoundController.SoundInGameOff();
        user.Score = GameScore.GetTotalScore();
        WinnerYes = await hubConnection.InvokeAsync<bool>("CheckWinner", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        float DoHalyavnoyPizzaCount = await hubConnection.InvokeAsync<float>("TOPScore");
        await hubConnection.InvokeAsync("CheckRating", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        await hubConnection.InvokeAsync("SetScore", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        int temp = await hubConnection.InvokeAsync<int>("GetRating", Newtonsoft.Json.JsonConvert.SerializeObject(user));
        DoHalyavnoyPizzaCount += 1.0f;
        //DoHalyavnoyPizzaCount = DoHalyavnoyPizzaCount - PlayerPrefs.GetInt("BestScore");
        UIController.SetDoHalyavnoyPizzaCount(DoHalyavnoyPizzaCount);
        NewRecordOrNot();
        //print(PlayerPrefs.GetInt("FirstDeath"));
        //print(UIController.NewRecord);
        //print(GameScore.GetTotalScore());
        //print(bestRating);
        var i = PlayerPrefs.GetInt("FirstDeath");
        if (i == 0)
        {
            switch (UIController.NewRecord)
            {
                case true:
                    if (GameScore.GetTotalScore() > DoHalyavnoyPizzaCount && GameScore.GetTotalScore() > 0)
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordTopTenOn();
                        //Notifications.CreateNotification();

                    }
                    else
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordOn();
                        PlayerPrefs.SetInt("FirstDeath", 1);
                    }
                    break;
                case false:
                    UIController.CanvasScoreZoneOn();
                    UIController.ResultOn();
                    break;
            }
        }
        if (i == 1)
        {
            switch (UIController.NewRecord)
            {
                case true:
                    if (GameScore.GetTotalScore() > DoHalyavnoyPizzaCount)
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordTopTenOn();

                    }
                    else
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordOn();
                        PlayerPrefs.SetInt("FirstDeath", 2);
                    }
                    break;
                case false:
                    UIController.CanvasScoreZoneOn();
                    UIController.ResultOn();
                    break;
            }
        }
        if (i == 2)
        {
            switch (UIController.NewRecord)
            {
                case true:
                    if (GameScore.GetTotalScore() > DoHalyavnoyPizzaCount)
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordTopTenOn();

                    }
                    else
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordOn();
                        PlayerPrefs.SetInt("FirstDeath", 3);
                    }
                    break;
                case false:
                    UIController.CanvasScoreZoneOn();
                    UIController.ResultOn();
                    break;
            }
        }
        if (i == 4)
        {
            switch (UIController.NewRecord)
            {
                case true:
                    if (GameScore.GetTotalScore() > DoHalyavnoyPizzaCount)
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordTopTenOn();
                    }
                    else
                    {
                        UIController.CanvasScoreZoneOn();
                        UIController.ResultRecordOn();
                    }
                    break;
                case false:
                    UIController.CanvasScoreZoneOn();
                    UIController.ResultOn();
                    //Notifications.CreateNotification();

                    break;
            }
        }
        if (i == 3)
        {
            UIController.InteractableCanvasOff();
            UIController.OcenkaOn();
            PlayerPrefs.SetInt("FirstDeath", 4);
        }

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
        //UIController.MainCameraOff();
        user.Score = PlayerPrefs.GetInt("BestScore");
        UIController.PositionCount(temp);
        float CountOfPlayers = await hubConnection.InvokeAsync<float>("CountUsers");
        UIController.SetRatingInMenu(temp, (int)CountOfPlayers);
        // }
    }
    public void NewRecordOrNot()
    {
        switch (GameScore.GetTotalScore() == bestRating && GameScore.GetTotalScore() > 0)
        {
            case true:
                UIController.NewRecord = true;
                break;
            case false:
                UIController.NewRecord = false;
                break;
        }
    }
    public void FirstOcenkaOff()
    {
        switch (UIController.NewRecord)
        {
            case true:
                UIController.CanvasScoreZoneOn();
                UIController.ResultRecordOn();
                break;
            case false:
                UIController.CanvasScoreZoneOn();
                UIController.ResultOn();
                break;
        }
        //Notifications.CreateNotification();
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
        hubConnection?.StopAsync();
    }
    public void SetRatingInGame()
    {
        UIController.SetCurrentRatingInGame(GameScore.GetTotalScore());
    }



}
