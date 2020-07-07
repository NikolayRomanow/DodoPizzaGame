using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using DodoDataModel;


public class VictorineForRunner : MonoBehaviour
{
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

  
    public void Restart()
    {
        Application.LoadLevel(0);
        Statistic.Score = 0;
    }
    public void Menu()
    {
        Application.LoadLevel(1);
    }
    private void Start()
    {
        OnClickPlay();
    }

    public void Update()
    {


        if (Player.transform.position.y < 1.1f && Player.activeInHierarchy == true)
        {
            RUN.SetActive(true);
        }
        else
        {
            RUN.SetActive(false);
        }
        if (Statistic.OK == false)
        {
            victorinePanel.SetActive(true);
        }
        if (Statistic.OK == true)
        {
            victorinePanel.SetActive(false);
        }
        if (Stats.isReady == true)
        {
            time += Time.deltaTime;
        }        
        if(Player.activeInHierarchy==false)
        {
            victorinePanel.SetActive(false);
            ratingPanel.SetActive(true);
            Statistic.Speed = 3f;
        }
        ratingScore.text = Convert.ToString(Statistic.Score);
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
            Statistic.OK = true;
            Statistic.time = 0;
            Statistic.VoprosOtvet = true;
            Statistic.Score++;
            TRUE.PlayOneShot(Da);
            PRESSBUTTON.PlayOneShot(button);
            

        }
        else
        {
            Statistic.Speed = 9f;
            Statistic.OK = true;
            Statistic.VoprosOtvet = false;
            FALSE.PlayOneShot(Net);
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







