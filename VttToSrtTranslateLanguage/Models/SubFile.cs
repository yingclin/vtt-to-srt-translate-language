using System;
using System.Collections.Generic;
using System.Text;

namespace VttToSrtTranslateLanguage.Models
{
    class SubFile
    {
        public string FullName { get; set; }
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();
    }
}
