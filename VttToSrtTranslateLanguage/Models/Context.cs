using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VttToSrtTranslateLanguage.Models
{
    class Context
    {
        private const string DefaultSourceLanguage = "en";
        private const string DefaultTargetLanguage = "zh-tw";

        public static Context GetContext(string[] args)
        {
            var context = new Context
            {
                SourceDirectory = Environment.CurrentDirectory,
                SourceLanguage = DefaultSourceLanguage,
                TargetLanguage = DefaultTargetLanguage
            };

            context.SourceDirectory = GetArgumentPath(context.SourceDirectory, args);

            return context;
        }

        private static string GetArgumentPath(string path, string[] args)
        {
            if (args.Length > 0)
            {
                path = args[0];

                if (path.EndsWith(Path.DirectorySeparatorChar) || path.EndsWith(Path.AltDirectorySeparatorChar))
                {
                    path = path.Substring(0, path.Length - 1);
                }
            }

            return path;
        }

        public string SourceDirectory { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
    }
}
