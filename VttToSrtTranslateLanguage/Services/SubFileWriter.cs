using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VttToSrtTranslateLanguage.Models;

namespace VttToSrtTranslateLanguage.Services
{
    class SubFileWriter
    {
        private const string Ext = "srt";

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

            for (int i = 1; i <= count; i++)
            {
                lines.AddRange(BuildSentence(i, subFile.Sentences[i]));
            }

            return lines.ToArray();
        }

        private IEnumerable<string> BuildSentence(int i, Sentence sentence)
        {
            throw new NotImplementedException();
        }

        private string GetOutFileName(string sourcePath)
        {
            return Path.GetFileNameWithoutExtension(sourcePath) + "." + Ext;
        }
    }
}
