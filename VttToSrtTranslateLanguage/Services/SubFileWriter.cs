using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VttToSrtTranslateLanguage.Models;

namespace VttToSrtTranslateLanguage.Services
{
/*
1
00:00:26,901 --> 00:00:29,571
製作地圖的時候，在作為基準的場所埋下的標石被稱作三角點

2
00:00:29,821 --> 00:00:35,702
記錄這些工作的日記就是所謂點之記
*/

    /// <summary>
    /// 輸出 SRT 檔案的實作
    /// </summary>
    class SubFileWriter
    {
        private const string Ext = "srt";
        private const int MaxCharSize = 22;

        private readonly Context _context;

        public SubFileWriter(Context context)
        {
            _context = context;
        }

        public void Write(SubFile subFile)
        {
            var lines = BuildTextLines(subFile);
            File.WriteAllLines(GetOutFileName(subFile.FullName), lines);
        }

        private string[] BuildTextLines(SubFile subFile)
        {
            var lines = new List<string>();
            var count = subFile.Sentences.Count;

            for (int i = 0; i < count; i++)
            {
                lines.AddRange(BuildSentence(i + 1, subFile.Sentences[i]));
            }

            return lines.ToArray();
        }

        private IEnumerable<string> BuildSentence(int i, Sentence sentence)
        {
            var lines = new List<string>();

            if (i > 1)
            {
                lines.Add(string.Empty);
            }

            lines.Add(i.ToString());
            lines.Add(sentence.Time);
            lines.AddRange(SplitLine(sentence.TranslateText));

            return lines.ToArray();
        }

        private List<string> SplitLine(string text)
        {
            var ret = new List<string>();

            if(text.Length > MaxCharSize)
            {
                ret.Add(text.Substring(0, MaxCharSize));
                ret.AddRange(SplitLine(text.Substring(MaxCharSize)));
            }
            else
            {
                ret.Add(text);
            }

            return ret;
        }

        public string GetOutFileName(string sourcePath)
        {
            return Path.GetDirectoryName(sourcePath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(sourcePath) + "." + Ext;
        }
    }
}
