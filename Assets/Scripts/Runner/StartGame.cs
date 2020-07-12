using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;

using SupremumStudio;

public class StartGame : MonoBehaviour
{
    public Animator Animator,StartBack;
    public GameObject CanvasPlay;

    // Start is called before the first frame update
    void Start()
    {
        var textFile = Resources.Load<TextAsset>("Questions/Question");
        //Debug.Log(textFile);
        List<QuestionModel> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(textFile.ToString());
        foreach (var item in questions)
        {
            Debug.Log(item.TextQuestion);
        }

    }
    public void StartGamee()
    {
       // Application.LoadLevel(0);

        Statistic.Speed = 3f;
        Animator.SetTrigger("Menu");
        StartBack.SetTrigger("off");
        //Statistic.Jiv = true;
        //Statistic.BOOL = true;       
        //CanvasPlay.SetActive(false);


    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
