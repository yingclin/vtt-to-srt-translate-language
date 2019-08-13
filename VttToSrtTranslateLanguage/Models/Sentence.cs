using System;
using System.Collections.Generic;
using System.Text;

namespace VttToSrtTranslateLanguage.Models
{
    class Sentence
    {
        public Sentence(string time, string text)
        {
            Time = time;
            Text = text;
        }

        public string Time { get; }
        public string Text { get; }
        public string TranslateText { get; set; }
    }
}
