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
    public class Quiz : MonoBehaviour
    {
        //private List<QuestionModel> questions;

        public Text QuestionText;
        public Text[] Answer;
        public Button[] AnswerButton;

        public string currentAnswer;
        public List<QuestionModel> questions;

        public int currentQuesion = -1;
        public int countQuestionFile;

        public Animator VictorineZone;
        public bool IsCorrectAnswer { get; private set; }
        public int CurrentQuestion
        {
            get
            {
                return currentQuesion;
            }
            set
            {
                if (value == 3)
                {
                    currentQuesion = 0;
                }
                else
                {
                    currentQuesion = value;
                }
            }
        }

        //UI Element


        private void SetQuestion()
        {
            //TextAsset JsonQuestion = Resources.Load<TextAsset>("Questions/Question");
            //List<QuestionModel> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(JsonQuestion.ToString());
            SetQuiz(questions[CurrentQuestion].TextQuestion, questions[CurrentQuestion].Answer);
            currentAnswer = questions[CurrentQuestion].Answer[0];
        }
        public void ReadQuestion()
        {
            var JsonQuestion = Resources.Load<TextAsset>("Questions/Question"); // прочитать файл
            questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(JsonQuestion.ToString()); // распознать его
            countQuestionFile = questions.Count;
            Debug.Log("Count Question: " + countQuestionFile);
            Shuffle(questions); // перемешать вопросы
        }
        public void NextQuestion()
        {
            AnimationOn();
            CurrentQuestion++;
            SetQuestion();
        }
        public void SetQuiz(string questionText, string[] answer)
        {
            QuestionText.text = questionText; // установить вопрос

            for (int i = 0; i < answer.Length; i++)
            {
                Answer[i].text = answer[i]; // установить ответы
            }
            Shuffle(Answer);
        }
        public void Shuffle(Text[] answer)
        {
            for (int i = 0; i < answer.Length; i++)
            {
                int r = Random.Range(0, answer.Length);
                var t = answer[i].text;
                answer[i].text = answer[r].text;
                answer[r].text = t;

            }
        }
        public void Shuffle(List<QuestionModel> model)
        {
            for (int i = 0; i < model.Count; i++)
            {
                int r = Random.Range(0, model.Count);
                var t = model[i];
                model[i] = model[r];
                model[r] = t;
            }
        }
        private void OnDisable()
        {
            foreach (var item in AnswerButton)
            {
                item.onClick.RemoveAllListeners(); // отписаться от всех событий
            }
        }
        public void AnimationOn()
        {
            VictorineZone.SetTrigger("on");
        }
        public void AnimationOff()
        {
            VictorineZone.SetTrigger("off");
        }

    }
}
