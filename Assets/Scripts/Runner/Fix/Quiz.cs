using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SupremumStudio
{
    public class Quiz : MonoBehaviour
    {
        private List<QuestionModel> questions;

        //UI Element


        private void SetQuestion()
        {
            TextAsset JsonQuestion = Resources.Load<TextAsset>("Questions/Question");
            List<QuestionModel> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionModel>>(JsonQuestion.ToString());
        }



    }
}
