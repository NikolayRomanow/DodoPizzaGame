using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SupremumStudio
{
    public class QuizView: MonoBehaviour
    {
        public Text QuestionText;
        public Text[] Answer;
        public Button[] AnswerButton;

        private void Start()
        {
            AnswerButton[0].onClick.AddListener(()=> { });
        }

        public void SetQuiz(string questionText, string[] answer)
        {
            QuestionText.text = questionText; // установить вопрос

            for (int i = 0; i < 3; i++)
            {
                Answer[i].text = answer[i]; // установить ответы
            }
        }



    }
}
