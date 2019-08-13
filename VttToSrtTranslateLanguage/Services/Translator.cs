using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VttToSrtTranslateLanguage.Models;

namespace VttToSrtTranslateLanguage.Services
{
    class Translator
    {
        private readonly Context _context;

        public Translator(Context context)
        {
            _context = context;
        }

        public SubFile TranslateSubFile(SubFile subFile)
        {
            var sentences = GetSentencesText(subFile.Sentences);
            var translated = TranslateLines(sentences);

            subFile.Sentences = subFile.Sentences.Select((s, i) => 
            {
                s.TranslateText = translated[i];
                return s;
            }).ToList();

            return subFile;
        }

        private string[] GetSentencesText(List<Sentence> sentences)
        {
            return sentences.Select(s => s.Text).ToArray();
        }

        public string[] TranslateLines(string[] textLines)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            TranslationClient client = TranslationClient.Create();
            var response = client.TranslateText(textLines, _context.TargetLanguage, _context.SourceLanguage);
            return response.Select(r => r.TranslatedText).ToArray();
        }
    }
}
