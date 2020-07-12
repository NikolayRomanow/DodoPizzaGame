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
        Run.QuizView = QuizView;
        QuizView.Quiz = Quiz;
        GameScore = new Score();
    }
    //public static Action myStart = () => { Debug.Log("myStart"); };
    //public static Action myUpdate = () => { Debug.Log("myUpdate"); };

    //private void Start()
    //{
    //    myStart();
    //}
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
    }

    private void UIController_StartGame()
    { 
        Statistic.isGameStart = true;
        SetSpeed(3);     
    }

    private void QuizView_InCorrectAnswer()
    {
        SetSpeed(9);        
    }

    private void QuizView_CorrectAnswer()
    {
        SetSpeed(9);       
    }

    private void Run_NewVopros()
    {
        Quiz.NextQuestion();        
    }

    private void Run_Death()
    {
        SetSpeed(0);
        UIController.VictorineZoneOff();
        UIController.ScoreZoneOn();
        UIController.MainCameraOff();
    }

    private void Run_SoundOfDeath()
    {
        
    }
        
}
