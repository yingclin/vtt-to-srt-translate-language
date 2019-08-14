using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VttToSrtTranslateLanguage.Models;

namespace VttToSrtTranslateLanguage.Services
{
    class Sentences
    {
        public List<Sentence> GetAll(string[] lines)
        {
            return VTTLinesToSentences(lines);
        }

        /// <summary>
        /// VTT 格式處理的實作
        /// <para>未來介面化或要支援其它格式,改這裡即可</para>
        /// </summary>
        /// <returns></returns>
        private List<Sentence> VTTLinesToSentences(string[] lines)
        {
            /* VTT 格式範例
            WEBVTT

            00:01.950 --> 00:03.720
            What is programming.

            00:03.930 --> 00:11.580
            What is programming, what is programming.
            */
            var sentences = new List<Sentence>();

            var lineCount = lines.Length;

            for(var i = 0; i < lineCount; i++)
            {
                var (time, isOk) = SubTime.TryParse(lines[i].Trim());

                if (isOk)
                {
                    if(i + 1 < lineCount && !string.IsNullOrEmpty(lines[i + 1]))
                    {
                        sentences.Add(new Sentence(time.ToSrtFormat(), lines[i + 1]));
                        i++;
                    }
                }
            }

            return sentences;
        }

        private bool IsTime(string text)
        {
            return Regex.IsMatch(text.Replace(" --> ", ""), @"^([0-9][0-9]\:[0-5][0-9]\.[0-9]{3}){2}");
        }
    }
}
