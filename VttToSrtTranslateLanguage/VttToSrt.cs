using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VttToSrtTranslateLanguage.Models;
using VttToSrtTranslateLanguage.Services;

namespace VttToSrtTranslateLanguage
{
    class VttToSrt
    {
        private const string GoogleAppCredentialsEnvName = "GOOGLE_APPLICATION_CREDENTIALS";
        private const string DefaultGoogleAppCredentials = @"D:\dev\gcp\private-key\SubtitleTranslation-1f7492d32cea.json";
        //
        private readonly SubFileReader _reader;
        private readonly SubFileWriter _writer;
        private readonly Translator _translator;

        public VttToSrt(SubFileReader reader, SubFileWriter writer, Translator translator)
        {
            _reader = reader;
            _writer = writer;
            _translator = translator;

            InitialCredentials();
        }

        private void InitialCredentials()
        {
            if (Environment.GetEnvironmentVariable(GoogleAppCredentialsEnvName) == null)
            {
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", DefaultGoogleAppCredentials);
            }
        }

        public void ConvertAll()
        {
            foreach (var subFile in _reader)
            {
                var translatedSubFile = _translator.TranslateSubFile(subFile);
                _writer.Write(translatedSubFile);
            }
        }
    }
}
