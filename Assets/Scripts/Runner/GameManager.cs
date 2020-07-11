using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SupremumStudio;

public class GameManager : MonoBehaviour
{
    public UIController UIController;
    public Run Run;
    public QuizView QuizView;
    public Quiz Quiz;
    private void Awake()
    {
        Run.JumpBird += Run_JumpBird;
        Run.SoundOfDeath += Run_SoundOfDeath;
        Run.Death += Run_Death;
        Run.NewVopros += Run_NewVopros;
        QuizView.CorrectAnswer += QuizView_CorrectAnswer;
        QuizView.InCorrectAnswer += QuizView_InCorrectAnswer;
        Run.QuizView = QuizView;
        
    }

    private void QuizView_InCorrectAnswer()
    {
        
    }

    private void QuizView_CorrectAnswer()
    {
        
    }

    private void Run_NewVopros()
    {
        //QuizView.NextQuestion();        
    }

    private void Run_Death()
    {
        UIController.VictorineZoneOff();
        UIController.ScoreZoneOn();
        UIController.MainCameraOff();
    }

    private void Run_SoundOfDeath()
    {
        
    }

    private void Run_JumpBird()
    {

    }
}
