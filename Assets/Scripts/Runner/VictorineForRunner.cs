using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using DodoDataModel;


public class VictorineForRunner : MonoBehaviour
{
    public StateGame State = StateGame.menu;

    public AudioClip Da, Net,button;
    public AudioSource TRUE, FALSE,PRESSBUTTON;
    public GameObject GameManager;
    public GameObject RUN;

    public QuestionList[] Questions;
    public Text[] AnswersText;
    public Text qText;
    //public Transform endPosition;

    public GameObject victorinePanel;
    public GameObject ratingPanel;

    public GameObject AnswersTable;
    public GameObject Player;
    //public GameObject Bird2;

    private GameObject game;

    public float time;
    private float _ratingScore;
    private float _trueAnswers;
    private float _countAnswers;

    private Text timeCount;
    private Text trueAnswers;
    private Text countAnswers;
    public Text ratingScore;

    private AIMoveFirstBird speedBird1;

    private List<object> qList;
    private QuestionList crntQ;
    private int randQ;

    private int Rele = 0;
    private bool lampochka = false;
    private bool BuffOn = false;
    private bool QisOn = false;

    private float timeForNextQ;
    private float timeForBuff;

    private float timeTenSec = 10f;
    public Animator Animator, ScoreZone, StartBack;
    public GameObject NewVopros, PlayCanvas;



    public void Restart()
    {
        //Application.LoadLevel(0);
        NewVopros.SetActive(true);        
        
        //Statistic.Jiv = true;
        Statistic.Score = 0;
        Statistic.Speed = 3f;
        Animator.SetTrigger("Menu");
        ScoreZone.SetTrigger("off");
        //Statistic.OK =false;
        
        //Animator.SetTrigger("Start");
        
        //ratingPanel.SetActive(false);



    }
    public void Menu()
    {
        //Application.LoadLevel(1);
        //PlayCanvas.SetActive(true);
        //ratingPanel.SetActive(false);
        ScoreZone.SetTrigger("off");
        StartBack.SetTrigger("on");
        //Statistic.Jiv = true;
    }

    private void Start()
    {
        OnClickPlay(); 
       // Animator.SetTrigger("Menu");
    }

    public void ScoreZoneOn()
    {
        ScoreZone.SetTrigger("off");
    }

    public void Update()
    {
       
        if (Statistic.BOOL == true)
        {


            if (Player.transform.position.y < 1.1f && Player.activeInHierarchy == true)
            {
                RUN.SetActive(true);
            }
            else
            {
                RUN.SetActive(false);
            }
            if (Statistic.Speed == 0)
            {
                RUN.SetActive(false);
            }
            //if (Statistic.OK == false)
            //{
            //    victorinePanel.SetActive(true);
            //}
            //if (Statistic.OK == true)
            //{
            //    victorinePanel.SetActive(false);
            //}
            if (Stats.isReady == true)
            {
                time += Time.deltaTime;
            }
            //if (Player.activeInHierarchy == false || Statistic.Jiv == false)
            //{
            //    //victorinePanel.SetActive(false);
            //    //ratingPanel.SetActive(true);
            //    //ScoreZone.SetTrigger("on");

            //    //Statistic.Speed = 3f;
            //}
            //if (Statistic.Jiv == true)
            //{
            //    //ratingPanel.SetActive(false);
            //    //ScoreZoneOn();
            //}
            ratingScore.text = Statistic.Score.ToString();
        }
    }

    public void OnClickPlay()
    {
        if (Rele == 0)
        {
            Rele++;
            qList = new List<object>(Questions);
            _questionGenerate();
        }

    }

    void _questionGenerate()
    {
        randQ = UnityEngine.Random.Range(0, qList.Count);
        crntQ = qList[randQ] as QuestionList;
        qText.text = crntQ.Question;
        List<string> Answer = new List<string>(crntQ.Answer);
        for (int i = 0; i < crntQ.Answer.Length; i++)
        {
            int rand = UnityEngine.Random.Range(0, Answer.Count);
            AnswersText[i].text = Answer[rand];
            Answer.RemoveAt(rand);
        }
    }

    public async void AnswersButtons(int index)
    {
        print(AnswersText[index].ToString());
        QisOn = true;
        if (AnswersText[index].text.ToString() == crntQ.Answer[0])
        {
            //speedBird1.SpeedMove += 0.1f;     Заменил на использование статического класса.
            // _trueAnswers++;
            // BuffOn = true;
            // GameManager.SendMessage("setSpeed");
            //timeForNextQ = 0;
            Statistic.Speed = 9f;
            //Statistic.OK = true;
            Statistic.time = 0;
           // Statistic.VoprosOtvet = true;
            Statistic.Score++;
            TRUE.PlayOneShot(Da, 0.5F);
            PRESSBUTTON.PlayOneShot(button);
            

        }
        else
        {
            Statistic.Speed = 9f;
            //Statistic.OK = true;
            //Statistic.VoprosOtvet = false;
            FALSE.PlayOneShot(Net,0.5F);
            PRESSBUTTON.PlayOneShot(button);
        }
        _countAnswers++;
        StartCoroutine(KostylNomer2534());
    }

    public IEnumerator KostylNomer2534()
    {
        yield return new WaitForSeconds(0.5f);
        qList.RemoveAt(randQ);
        _questionGenerate();
    }

}

[System.Serializable]
public class QuestionLists
{
    public string Question;
    public string[] Answer = new string[3];
}


public enum StateGame
{
    menu,
    game,
    score
}




