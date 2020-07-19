using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

namespace SupremumStudio
{
    public class QuizView : MonoBehaviour
    {

        public TextMeshProUGUI QuestionText;
        public Text[] Answer;
        public Button[] AnswerButton;

        public Quiz Quiz;
        public Animator VictorineZone;
        public float TimeForQuestion, CurrentTime, DeltaTime;
        public bool QuestionIsOn = false;
        public bool? IsCorrectAnswer { get; private set; }
        public event Action<float> CorrectAnswer;
        public event Action InCorrectAnswer;

        public Slider Timer;

        public void SetSliderMaxValue()
        {
            Timer.maxValue = 1800;
            Timer.value = TimeForQuestion * 100;
        }
        public void SetSliderValue()
        {
            Timer.value = TimeForQuestion * 100;
        }
        public void ResetSliderValue()
        {
            Timer.value = 1800;
        }


        public Color newColor;


        public void AnimationOn()
        {
            VictorineZone.SetTrigger("on");
        }
        public void IsCorrectAnswerFalse()
        {
            IsCorrectAnswer = false;
        }
        //public void AnimationOff()
        //{
        //    Quiz.VictorineZone.SetTrigger("off");
        //}
        private void Start()
        {
            SetSliderMaxValue();
            Quiz.QuestionChanged += Quiz_QuestionChanged;
            TimeForQuestion = 18f;
            Quiz.ReadQuestions();
            //SetQuestion(); // Можно использовать из другого класса

            foreach (var item in AnswerButton) // TODO: not work in for
            {
                item.onClick.AddListener(() =>
                {
                    if (item.GetComponentInChildren<Text>().text == Quiz.currentAnswer) //TODO: переосмыслить
                    {
                        CorrectAnswer(SendScore());
                        //AnimationOff();
                        IsCorrectAnswer = true;
                        QuestinIsTrueOff();
                        ResetTime();
                        item.GetComponentInChildren<Image>().color = Color.green;
                    }
                    else
                    {
                        InCorrectAnswer();
                        // AnimationOff();
                        IsCorrectAnswer = false;
                        QuestinIsTrueOff();
                        ResetTime();
                        item.GetComponentInChildren<Image>().color = Color.red;
                    }
                });
            }
        }

        private void Quiz_QuestionChanged()
        {
            AnimationOn();
            var data = Quiz.GetQuestionData();
            SetQuiz(data.Item1, data.Item2);
            IsCorrectAnswer = null;
        }
        public void ResetColors()
        {
            for (int i = 0; i <=2; i++)
            {
                AnswerButton[i].GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.5f);
            }

        }

        public void ResetTime()
        {
            TimeForQuestion = 18f;
            CurrentTime = 0;
        }
        public void QuestinIsTrueOn()
        {
            QuestionIsOn = true;
        }
        public void QuestinIsTrueOff()
        {
            QuestionIsOn = false;
        }
        public void CalcTime()
        {
            TimeForQuestion -= Time.deltaTime;
            CurrentTime += Time.deltaTime;
            DeltaTime = TimeForQuestion - CurrentTime;
            if (DeltaTime < 0)
            {
                DeltaTime = 0;
            }
        }
        public float SendScore()
        {
            return DeltaTime;
        }
        private void Update()
        {

            //print(TimeForQuestion);
            //print(CurrentTime);
            //print(DeltaTime);

            if (QuestionIsOn == true)
            {
                CalcTime();
                SetSliderValue();
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
