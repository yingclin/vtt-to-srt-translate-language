using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VttToSrtTranslateLanguage.Models;

namespace VttToSrtTranslateLanguage.Services
{
    class SubFileReader : IEnumerable<SubFile>
    {
        private const string FilePattern = "*.vtt";

        private readonly Context _context;
        private readonly Sentences _sentences;
        //
        private List<string> _names = new List<string>();
        //private int _currentIndex = 0;

        public SubFileReader(Context context)
        {
            _context = context;
            _sentences = new Sentences();

            LoadAll();
        }

        public void LoadAll()
        {
            Console.WriteLine("讀取目錄...");

            LoadFilesName(_context.SourceDirectory);
        }

        private void LoadFilesName(string path)
        {
            try
            {
                IEnumerable<string> fileNames = Directory.EnumerateFiles(path, FilePattern);

                _names.AddRange(fileNames.Select(n => Path.Combine(path, n)));

                foreach (var dirName in Directory.EnumerateDirectories(path))
                {
                    LoadFilesName(Path.Combine(path, dirName));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"讀取目錄出錯, 原因: {ex.Message}");
            }
        }

        public IEnumerator<SubFile> GetEnumerator()
        {
            for (int index = 0; index < _names.Count; index++)
            {
                yield return GetSubFile(_names[index]);
            }
        }

        private SubFile GetSubFile(string fullName)
        {
            var ret = new SubFile
            {
                FullName = fullName
            };

            try
            {
                var lines = File.ReadAllLines(fullName);
                ret.Sentences = _sentences.GetAll(lines);
            }
            catch(Exception ex)
            {
                ret.Message = ex.Message;
            }

            return ret;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
