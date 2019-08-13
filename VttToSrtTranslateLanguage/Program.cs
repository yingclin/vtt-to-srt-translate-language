using System;
using System.IO;
using VttToSrtTranslateLanguage.Models;
using VttToSrtTranslateLanguage.Services;

namespace VttToSrtTranslateLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ok VttToSrtTranslateLanguage");

            var context = Context.GetContext(args);

            new Program().Run(context);
            
            Console.Write("結束, 按任意鍵離開!");
            Console.ReadKey();
        }

        void Run(Context context)
        {
            var vttToSrt = new VttToSrt(new SubFileReader(context), new SubFileWriter(context), new Translator(context));
            vttToSrt.ConvertAll();
        }
    }
}
