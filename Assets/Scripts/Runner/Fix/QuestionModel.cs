﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;

namespace SupremumStudio
{
    public class QuestionModel
    {
        public string TextQuestion = String.Empty;
        public string[] Answer = new string[3];
        public int Weight { get; set; }
    }
}
