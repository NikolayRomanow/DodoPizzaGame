using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SupremumStudio;
using System;

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
    private void Awake()
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

    private void Start()
    {
        //myStart();
        SoundController.SoundInMenuOn();
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
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();
    }
    private void UIController_BackToMenu()
    {
        SoundController.SoundOfPressedButton();
        SoundController.SoundInMenuOn();
    }

    private void UIController_StartGame()
    {
        Statistic.isGameStart = true;
        SetSpeed(3);
        SoundController.SoundOfPressedButton();
        SoundController.SoundInGameOn();
        SoundController.SoundInMenuOff();
    }

    private void QuizView_InCorrectAnswer()
    {
        SoundController.SoundOfInCorrectAnswer();
        UIController.VictorineZoneOff();
        SetSpeed(9);
    }

    private void QuizView_CorrectAnswer()
    {
        SoundController.SoundOfCorrectAnswer();
        UIController.VictorineZoneOff();
        SetSpeed(9);
    }

    private void Run_NewVopros()
    {
        Quiz.NextQuestion();
    }

    private void Run_Death()
    {
        SetSpeed(0);
        SoundController.SoundInGameOff();        
        UIController.VictorineZoneOff();
        UIController.ScoreZoneOn();
        UIController.MainCameraOff();
    }

    private void Run_SoundOfDeath()
    {
        SoundController.SoundOfCrash();
    }

}
