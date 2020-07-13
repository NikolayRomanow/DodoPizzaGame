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
        private List<QuestionModel> questions;
        public string currentAnswer;
        public int currentWeight;
        // Простые вопросы
        public List<QuestionModel> simpleQuestions;
        // Средней сложности вопросы
        public List<QuestionModel> averageQuestions;
        // Сложные вопросы
        public List<QuestionModel> hardQuestions;
        public int currentQuesion = -1;
        public int countQuestionFile;
        public bool IsCorrectAnswer { get; private set; }
        public event Action QuestionChanged;
        public int CurrentQuestion
        {
            get
            {
                return currentQuesion;
            }
            set
            {  if (value == questions.Count)
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
        //private void SetQuestion()
        //{
        //    //TextAsset JsonQuestion = Resources.Load<TextAsset>("Questions/Question");
        //    //List<QuestionModel> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(JsonQuestion.ToString());
        //    //SetQuiz(questions[CurrentQuestion].TextQuestion, questions[CurrentQuestion].Answer);
        //    currentAnswer = questions[CurrentQuestion].Answer[0];
        //    currentWeight = questions[CurrentQuestion].Weight;
        //}
        public (string, string[]) GetQuestionData()
        {
            List<QuestionModel> questions = GetCurrentQuestionsCollection();
            //Debug.Log(CurrentQuestion);
            currentAnswer = questions[CurrentQuestion].Answer[0];
            
            currentWeight = questions[CurrentQuestion].Weight;
            return (questions[CurrentQuestion].TextQuestion, questions[CurrentQuestion].Answer);
            
        }
        public void ResetQuiz()
        {
            CurrentQuestion = -1;
        }
        private List<QuestionModel> GetCurrentQuestionsCollection()
        {
            //if (currentQuesion < 10)
            //{
            //    return simpleQuestions;
            //}
            //else if (currentQuesion < 20)
            //{
            //    return averageQuestions;
            //}
            //else
            //{
            //    return hardQuestions;
            //}
            return questions;
        }
        public void ReadQuestions()
        {
            //var JsonQuestion = Resources.Load<TextAsset>("Questions/Question"); // прочитать файл
            var JsonQuestion = Resources.Load<TextAsset>("Questions/test"); // прочитать файл
            List<QuestionModel> allQuestions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(JsonQuestion.ToString()); // распознать его
            OrderByWeight(allQuestions);            
            //questions = allQuestions;
            //countQuestionFile = questions.Count;

            //Shuffle(simpleQuestions); // перемешать вопросы
            //Shuffle(averageQuestions); // перемешать вопросы
            //Shuffle(hardQuestions); // перемешать вопросы
            //                          //SendWeight(questions);

            //Shuffle(allQuestions);
        }

        private void OrderByWeight(List<QuestionModel> allQuestions)
        {
            //foreach (var item in allQuestions)
            //{
            //    Debug.Log(item.Weight);
            //}
            //foreach (var item in allQuestions.Where(x => x.Weight == 0))
            //{
            //    Debug.Log(item.Weight);
            //}
            //Debug.Log(simpleQuestions);
            simpleQuestions = new List<QuestionModel>(allQuestions.Where(x => x.Weight == 0));
            averageQuestions = new List<QuestionModel>(allQuestions.Where(x => x.Weight == 1));
            hardQuestions = new List<QuestionModel>(allQuestions.Where(x => x.Weight == 2));
            Shuffle(simpleQuestions); // перемешать вопросы
            Shuffle(averageQuestions); // перемешать вопросы
            Shuffle(hardQuestions);
            questions = new List<QuestionModel>(simpleQuestions);
            questions.AddRange(averageQuestions);
            questions.AddRange(hardQuestions);

        }
      public void NextQuestion()
        {   
            CurrentQuestion++;
            QuestionChanged();
            
            //SetQuestion();
        }
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
        //public void SendWeight(List<QuestionModel> model)
        //{
        //    for (int i = 0; i < model.Count; i++)
        //    {
        //        Statistic.Scores += model[i].Weight;                
        //    }
        //}
    }
}