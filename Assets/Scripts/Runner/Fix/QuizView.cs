using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace SupremumStudio
{
    public class QuizView : MonoBehaviour
    {

        Quiz Quiz;
        public Animator VictorineZone;
        public bool IsCorrectAnswer { get; private set; }       
        public event Action CorrectAnswer;
        public event Action InCorrectAnswer;

        
        public void AnimationOn()
        {
            Quiz.VictorineZone.SetTrigger("on");
        }
        public void AnimationOff()
        {
            Quiz.VictorineZone.SetTrigger("off");
        }
        private void Start()
        {
            Quiz.ReadQuestion();
            //SetQuestion(); // Можно использовать из другого класса

            foreach (var item in Quiz.AnswerButton)  // TODO: not work in for
            {
                item.onClick.AddListener(() =>
                {
                    if (item.GetComponentInChildren<Text>().text == Quiz.currentAnswer) //TODO: переосмыслить
                    {   
                        CorrectAnswer();
                        AnimationOff();
                        IsCorrectAnswer = true;
                    }
                    else
                    {   
                        InCorrectAnswer();
                        AnimationOff();
                        IsCorrectAnswer = false;
                    }
                });
            }
        }

        //private void ReadQuestion()
        //{
        //    var JsonQuestion = Resources.Load<TextAsset>("Questions/Question"); // прочитать файл
        //    questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(JsonQuestion.ToString()); // распознать его
        //    countQuestionFile = questions.Count;
        //    Debug.Log("Count Question: " + countQuestionFile);
        //    Shuffle(questions); // перемешать вопросы
        //}

        //private void SetQuestion()
        //{
        //    SetQuiz(questions[CurrentQuestion].TextQuestion, questions[CurrentQuestion].Answer);
        //    currentAnswer = questions[CurrentQuestion].Answer[0];
        //}

        //public void NextQuestion()
        //{
        //    AnimationOn();
        //    CurrentQuestion++;
        //    SetQuestion();
        //}


        //public void SetQuiz(string questionText, string[] answer)
        //{
        //    QuestionText.text = questionText; // установить вопрос

        //    for (int i = 0; i < answer.Length; i++)
        //    {
        //        Answer[i].text = answer[i]; // установить ответы
        //    }
        //    Shuffle(Answer);
        //}

        //public void Shuffle(Text[] answer)
        //{
        //    for (int i = 0; i < answer.Length; i++)
        //    {
        //        int r = Random.Range(0, answer.Length);
        //        var t = answer[i].text;
        //        answer[i].text = answer[r].text;
        //        answer[r].text = t;

        //    }
        //}

        //public void Shuffle(List<QuestionModel> model)
        //{
        //    for (int i = 0; i < model.Count; i++)
        //    {
        //        int r = Random.Range(0, model.Count);
        //        var t = model[i];
        //        model[i] = model[r];
        //        model[r] = t;
        //    }
        //}



        //private void OnDisable()
        //{
        //    foreach (var item in AnswerButton)
        //    {
        //        item.onClick.RemoveAllListeners(); // отписаться от всех событий
        //    }
        //}


    }
}
